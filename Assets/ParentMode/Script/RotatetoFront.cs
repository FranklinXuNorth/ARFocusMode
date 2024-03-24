using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatetoFront : MonoBehaviour
{
    [SerializeField] GameObject cameraObject;

    Vector3 cameraRotation;
    bool canRotate = true;

    // Start is called before the first frame update
    void Start()
    {
        cameraRotation = cameraObject.transform.localRotation.eulerAngles;
    }

    public void whenPressed()
    {
        if (canRotate)
        {
            cameraRotation = cameraObject.transform.localRotation.eulerAngles;
            float yRotation = cameraRotation.y;
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
            canRotate = false;
        }
        else
        {
            canRotate = true;
        }
        
    }
}
