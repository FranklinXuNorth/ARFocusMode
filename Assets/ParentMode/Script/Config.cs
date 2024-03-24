using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config: MonoBehaviour
{
    [Header("Zoom In Out")]
    [SerializeField] public float positionOffset;
    [SerializeField] public float scalingFactor;
    [SerializeField] public float opacityChangingFactor;
    [SerializeField] public float duration;

    [Header("Icon When Selected")] 
    [SerializeField] public float iconShrinkTime;
    [SerializeField] public float iconShrinkScale;
    [SerializeField] public float iconShrinkDist;

    // Parenting Mode and Cat Mode
    [Header("Icon When Escaping")]
    [SerializeField] public float escapeSpeed;
    [SerializeField] public float returnDuration;
    [SerializeField] public float returnSpeedTime;

    // Dog Mode
    [Header("Icon When Attracting")]
    [SerializeField] public float attractedSpeed;

}
