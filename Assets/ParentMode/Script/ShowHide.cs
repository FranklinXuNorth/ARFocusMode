using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowHide : MonoBehaviour
{

    [SerializeField] GameObject scalingAsset;
    [SerializeField] Config config;

    float positionOffset;
    float scalingFactor;
    float duration;
    float targetAlpha = 0.05f;

    Vector3 originalLoc;
    Vector3 beforeLoc;
    Vector3 originalScale;

    // Get all child renderers of the GameObject
    Renderer[] childRenderers;

    bool canChangeColor = false;

    // define this is showing or hiding
    bool isShowing = false;

    private void Start()
    {
        positionOffset = config.positionOffset;
        scalingFactor = config.scalingFactor;
        duration = config.duration;

        originalLoc = scalingAsset.transform.localPosition;
        originalScale = scalingAsset.transform.localScale;

        beforeLoc = new Vector3(originalLoc.x, originalLoc.y, originalLoc.z - positionOffset);
        scalingAsset.transform.localPosition = beforeLoc;
        scalingAsset.transform.localScale *= scalingFactor;


        // color
        childRenderers = scalingAsset.GetComponentsInChildren<Renderer>();

        // Iterate through each child renderer
        foreach (Renderer childRenderer in childRenderers)
        {
            // Get the material of the child renderer
            Material material = childRenderer.material;

            // Set the material's color with the target alpha value
            Color color = material.color;
            color.a = targetAlpha;
            material.color = color;
        }

        scalingAsset.SetActive(false);
    }
    private void Update()
    {
        if (isShowing) changeColor(config.opacityChangingFactor, (targetAlpha < 1.0f));
        else changeColor(-config.opacityChangingFactor, (targetAlpha > 0.0f));        
    }

    public void whenPressed()
    {
        if (!isShowing) OnShown();
        else OnHide();
    }

    private void OnHide()
    {
        isShowing = false;           
        LeanTween.cancel(scalingAsset);
        LeanTween.moveLocal(scalingAsset, beforeLoc, duration).setEase(LeanTweenType.easeOutCubic);
        LeanTween.scale(scalingAsset, originalScale * scalingFactor, duration).setEase(LeanTweenType.easeOutCubic);

        canChangeColor = true;     
    }

    private void OnShown()
    {
        isShowing = true;
        scalingAsset.SetActive(true);
        LeanTween.cancel(scalingAsset);
        LeanTween.moveLocal(scalingAsset, originalLoc, duration).setEase(LeanTweenType.easeOutCubic);
        LeanTween.scale(scalingAsset, originalScale, duration).setEase(LeanTweenType.easeOutCubic);

        canChangeColor = true;
    }

    private void changeColor(float gradient, bool situation)
    {
        // change color
        if (canChangeColor)
        {
            if (situation)
            {
                targetAlpha += gradient;
            }
            else
            {
                canChangeColor = false;
                if (!isShowing) scalingAsset.SetActive(false);
            }

            // Iterate through each child renderer
            foreach (Renderer childRenderer in childRenderers)
            {
                // Get the material of the child renderer
                Material material = childRenderer.material;

                // Set the material's color with the target alpha value
                Color color = material.color;
                color.a = targetAlpha;
                material.color = color;
            }
        }

    }
}
