using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashLight : MonoBehaviour
{
    private XRGrabInteractable interactable;
    private bool isFlashlightOn = false;
    private Light spotlight;
    private AudioSource switchSound;
    private Material lens;
    private Material bulb;

    public Light pointLight;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        lens = renderer.materials[1];
        bulb = renderer.materials[3];
        pointLight.enabled = false;
        spotlight = GetComponentInChildren<Light>();

        interactable  = GetComponent<XRGrabInteractable>();
        interactable.activated.AddListener(OnGrab);
        switchSound = GetComponentInChildren<AudioSource>();

        // spotlight.enabled = false;
        // lens.DisableKeyword("_EMISSION");
        // bulb.DisableKeyword("_EMISSION");
        // isFlashlightOn = false;
    }

    public void OnGrab(ActivateEventArgs args)
    {
        pointLight.enabled = false;
        Debug.Log("Entered Ongrab");
        if (!isFlashlightOn)
        {
            TurnOnFlashlight();
        }
        else
        {
            TurnOffFlashlight();
        }
    }
    private void TurnOnFlashlight()
    {
        switchSound.Play();
        spotlight.enabled = true;
        lens.EnableKeyword("_EMISSION");
        bulb.EnableKeyword("_EMISSION");
        isFlashlightOn = true;
    }

    private void TurnOffFlashlight()
    {
        switchSound.Play();
        spotlight.enabled = false;
        lens.DisableKeyword("_EMISSION");
        bulb.DisableKeyword("_EMISSION");
        isFlashlightOn = false;
 
    }

    public void DropFlashlight()
    {
        // Enable the point light
        pointLight.enabled = true;

        // Add more effects if needed
    }
    public void TurnOffFlashIndicator()
    {
        // Enable the point light
        pointLight.enabled = false;

        // Add more effects if needed
    }
}
