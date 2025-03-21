using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerAnimationAndSound : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public AudioSource audioSource; // Reference to the AudioSource component

    public string triggerName;
    public bool destroyAftertrigger = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggere Entered in collider");
        // Check if the collider belongs to the specific object you're interested in
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Tag is Player");
            // Play animation
            if(animator!=null){
                animator.SetTrigger(triggerName); // Replace 'StartAnimation' with your animation trigger name
            }       
                    else
        {
            Debug.Log("Animator Source is Null");
        }
    
        if(audioSource!=null){
            audioSource.Play();
        }
        else
        {
            Debug.Log("Audio Source is Null");
        }

        }
        if(destroyAftertrigger){
            gameObject.SetActive(false);
        }
    }

    void OnSelectEntered(SelectEnterEventArgs args)
    {
    // Check if the selected object is the specific object you're interested in
    // This depends on how you identify the player or the object in your specific context
        // Play animation
            if(animator!=null){
                animator.SetTrigger(triggerName); // Replace 'StartAnimation' with your animation trigger name
            }  
                    else
        {
            Debug.Log("Animator Source is Null");
        }

        // Play sound
        if(audioSource!=null){
            audioSource.Play();
        }
                else
        {
            Debug.Log("Audio Source is Null");
        }

    }
}
