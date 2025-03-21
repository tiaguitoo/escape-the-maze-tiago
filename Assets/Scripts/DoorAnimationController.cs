using System;
using UnityEngine;

public class DoorAnimationController : MonoBehaviour
{
    private Animator doorAnimator;   // The Animator on the DoorHinge
    private AudioSource audioSource; // The AudioSource on the DoorHinge
    public AudioClip doorOpenClip;  // Drag your doorOpen sound clip here in the inspector
    public AudioClip doorCloseClip; // Drag your doorClose sound clip here in the inspector
    private bool isOpen = false;    // Track door state
    private GameObject flashLight;


    public void Awake(){
        flashLight = GameObject.FindGameObjectWithTag("Torchlight");
    }
    
    private bool flashActive = false;
    public String triggerVariable = "toggleDoor";

    public void Start(){
        doorAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        flashLight.SetActive(flashActive);
    }
    // This function will be called to toggle the door animation
    public void ToggleDoorAnimation()
    {
        Debug.Log("TriggeredAction" + triggerVariable);
        isOpen = !isOpen;
        doorAnimator.SetTrigger(triggerVariable);
        // Play the respective sound
        if (isOpen)
        {
            if(triggerVariable == "openDrawer" && !flashActive){
                 Invoke("ActivateFlashlight", 0.7f);
            }
            audioSource.clip = doorOpenClip;
        }
        else
        {
            if(triggerVariable == "openDrawer"){
                Invoke("ActivateFlashlight", 0.7f);
            }
            audioSource.clip = doorCloseClip;
        }
        audioSource.Play();
    }
    public void ActivateFlashlight()
    {
        flashActive = true;
        // Activate the flashlight object
        flashLight.SetActive(flashActive);
    }
}
