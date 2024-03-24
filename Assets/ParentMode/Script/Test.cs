using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Get all child renderers of the GameObject
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();

        // Iterate through each child renderer
        foreach (Renderer childRenderer in childRenderers)
        {
            // Get the material of the child renderer
            Material material = childRenderer.material;

            // Set the material's color with the target alpha value
            Color color = material.color;
            color.a = 0.05f;
            material.color = color;            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
