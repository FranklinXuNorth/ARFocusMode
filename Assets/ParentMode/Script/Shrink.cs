using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour
{
    // Configuration
    [SerializeField] Config config;


    float scalingFactor;
    float duration;

    private Vector3 originalScale;
    private Vector3 originalLoc;

    // Start is called before the first frame update
    void Awake()
    {
        scalingFactor = config.iconShrinkScale;
        duration = config.iconShrinkTime;

        originalScale = this.gameObject.transform.localScale;
        originalLoc = this.gameObject.transform.localPosition;
    }

    // if the icon is pointed
    public void WhenEnter()
    {
        LeanTween.cancel(this.gameObject);
        LeanTween.scale(this.gameObject, originalScale * scalingFactor, duration).setEase(LeanTweenType.easeOutQuad);

        LeanTween.moveLocal(this.gameObject, new Vector3(originalLoc.x + config.iconShrinkDist, originalLoc.y, originalLoc.z), duration)
            .setEase(LeanTweenType.easeOutCubic);
    }

    // if the icon is being released
    public void WhenOut()
    {
        LeanTween.cancel(this.gameObject);
        LeanTween.scale(this.gameObject, originalScale, duration).setEase(LeanTweenType.easeOutQuad);
        LeanTween.moveLocal(this.gameObject, new Vector3(originalLoc.x, originalLoc.y, originalLoc.z), duration)
            .setEase(LeanTweenType.easeOutCubic);
    }
}
