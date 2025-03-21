using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LightFlicker : MonoBehaviour
{
    //public CameraFootsteps cameraFootStepsScript;
    public Light[] lightsToFlicker;
    public float minIntensity = 0f;
    public float maxIntensity = 1f;
    // public float flickerSpeed = 0.1f;
    public float flickerSpeed = 0.1f;
    public string triggerTag = "Player"; // Tag of the GameObject that triggers the flicker
    

    private bool isFlickering = false;

    public float flickerDuration = 2f;
    private float timer;
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag(triggerTag) && !isFlickering)
    //     {
    //         //cameraFootStepsScript.isScared = true;
    //         StartFlickering();
    //     }
    // }
    // private void OnSelectEntered(SelectEnterEventArgs args)
    // {
    //     StartFlickering();
    // }

    void Update() {
        StartFlickering();
    }

    private void StartFlickering()
    {
        isFlickering = true;

        // InvokeRepeating("Flicker", 0f, flickerSpeed);
        // Invoke("StopFlickering", flickerDuration);
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Flicker();
            timer = flickerSpeed;
        }
        
    }

    private void StopFlickering()
    {
        isFlickering = false;
        CancelInvoke("Flicker");
        foreach (Light light in lightsToFlicker)
        {
            light.intensity = maxIntensity; // Reset to original intensity
        }
            Destroy(gameObject);

    }
    

    private void Flicker()
    {
        if (!isFlickering) return;

        foreach (Light light in lightsToFlicker)
        {
            light.intensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}
