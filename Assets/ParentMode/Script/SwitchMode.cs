using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.XR.CoreUtils;
using UnityEngine;

public class SwitchMode : MonoBehaviour
{
    [SerializeField] List<GameObject> modes;
    [SerializeField] List<GameObject> panels;
    // Start is called before the first frame update


    public void ToParent()
    {
        foreach(GameObject mode in modes)
        {
            if (mode.CompareTag("ParentalMode"))
            {
                mode.SetActive(true);
            }
            else
            {
                mode.SetActive(false);
            }
        }

        foreach (GameObject panel in panels)
        {
            if (panel.CompareTag("OpenParental"))
            {
                panel.SetActive(true);
            }
            else
            {
                panel.SetActive(false);
            }
        }
    }

    public void ToCat()
    {
        foreach (GameObject mode in modes)
        {
            if (mode.CompareTag("CatMode"))
            {
                mode.SetActive(true);
            }
            else
            {
                mode.SetActive(false);
            }
        }

        foreach (GameObject panel in panels)
        {
            if (panel.CompareTag("OpenCat"))
            {
                panel.SetActive(true);
            }
            else
            {
                panel.SetActive(false);
            }
        }
    }

    public void ToDog()
    {
        foreach (GameObject mode in modes)
        {
            if (mode.CompareTag("DogMode"))
            {
                mode.SetActive(true);
            }
            else
            {
                mode.SetActive(false);
            }
        }

        foreach (GameObject panel in panels)
        {
            if (panel.CompareTag("OpenDog"))
            {
                panel.SetActive(true);
            }
            else
            {
                panel.SetActive(false);
            }
        }
    }
}


