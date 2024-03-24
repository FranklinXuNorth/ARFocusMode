using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static MouseLook;
using static UnityEngine.GraphicsBuffer;

public class Escape : MonoBehaviour
{
    // Configuration
    [SerializeField] Config config;

    [SerializeField] GameObject rightHandle;
    [SerializeField] GameObject leftHandle;
    RaycastHit leftRayHit;
    RaycastHit rightRayHit;

    Rigidbody rgbd;

    Vector3 facingDirection;
    Vector3 forceDirection = Vector3.zero;

    [SerializeField] GameObject cameraObject;

    float moveSpeed;
    float returnDuration;
    float returnSpeedTime;
    Vector3 largestVelocity;

    bool isTouched = false;
    bool shouldRotate = false;
    RaycastHit inputHit;


    // about moving and getting back
    Timer timer;
    bool timerTrigger;
    Vector3 originalLoc;
    Quaternion originalRotation;


    // Start is called before the first frame update
    void Start()
    {
        // config
        moveSpeed = config.escapeSpeed;
        returnDuration = config.returnDuration;
        returnSpeedTime = config.returnSpeedTime;


        timer = this.gameObject.AddComponent<Timer>();
        timer.Duration = returnDuration;
        timerTrigger = false;

        originalLoc = transform.localPosition;
        originalRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, 
                                            transform.localRotation.eulerAngles.y, 
                                            transform.localRotation.eulerAngles.z);

        rgbd = GetComponent<Rigidbody>();
        facingDirection = cameraObject.transform.position - transform.position;
    }

    private void Update()
    {

        RaycastHit[] leftRayHits = Physics.SphereCastAll(leftHandle.transform.position, 0.1f, leftHandle.transform.forward, 20);
        RaycastHit[] rightRayHits = Physics.SphereCastAll(rightHandle.transform.position, 0.1f, rightHandle.transform.forward, 20);        

        // detect if either of controllers pointed to something
        if (leftRayHits.Length != 0 || rightRayHits.Length != 0)
        {
            isTouched = false;

            foreach (RaycastHit leftRayHit in leftRayHits)
            {
                if ((leftRayHit.collider.gameObject == this.gameObject))
                {
                    isTouched = true;
                    shouldRotate = true;
                    inputHit = leftRayHit;
                    break;
                }                
            }

            foreach (RaycastHit rightRayHit in rightRayHits)
            {
                if ((rightRayHit.collider.gameObject == this.gameObject))
                {
                    isTouched = true;
                    shouldRotate = true;
                    inputHit = rightRayHit;
                    break;
                }
            }
        }                                                              

        else if (leftRayHits.Length == 0 && rightRayHits.Length == 0)
        {
            isTouched = false;
        }

        // slow or fasten the object
        if (!isTouched) SlowDown();
        else SpeedUp(inputHit);


        // if the speed is 0:
        if (rgbd.velocity.magnitude == 0 && transform.position != originalLoc)
        {                        
            if (!timerTrigger)
            {
                timerTrigger = true;
                timer.Duration = returnDuration;
                timer.ResetTimer();
                timer.Tiktok();                              
            }            
        }

        else if (rgbd.velocity.magnitude != 0 || transform.position == originalLoc)
        {
            timer.Stop();
            timerTrigger = false;

            timer.Duration = returnDuration;
            timer.ResetTimer();
        }

        if (timer.Finished)
        {
            LeanTween.cancel(this.gameObject);
            LeanTween.moveLocal(this.gameObject, originalLoc, returnSpeedTime).setEase(LeanTweenType.easeOutCubic);            
            timer.Stop();
            timer.Duration = returnDuration;
            timer.ResetTimer();
        }

        if (transform.localPosition == originalLoc)
        {            
            shouldRotate = false;
        }

        if (shouldRotate)
        {
            // a really complex rotation
            facingDirection = cameraObject.transform.position - transform.position;

            // first face the object approximately towards the correct direction
            Quaternion rotation1 = Quaternion.LookRotation(facingDirection, Vector3.up);
            Vector3 newUp = rotation1 * Vector3.up;

            // then based on the existing Rotation, face it to the object

            Quaternion rotation2 = Quaternion.AngleAxis(90.0f, newUp);
            this.gameObject.transform.rotation = rotation1 * rotation2;
        }
        else
        {
            this.gameObject.transform.localRotation = originalRotation;
        }        

    }

    void SlowDown()
    {
        rgbd.velocity *= 0.95f;
        if (rgbd.velocity.magnitude < largestVelocity.magnitude * 0.05) rgbd.velocity = Vector3.zero;
    }

    void SpeedUp(RaycastHit rayHit)
    {
        LeanTween.cancel(this.gameObject);
        forceDirection = this.gameObject.transform.position - rayHit.point;
        forceDirection.Normalize();
        rgbd.AddForce(forceDirection * moveSpeed, ForceMode.Force);
        largestVelocity = rgbd.velocity;
    }
    
}
