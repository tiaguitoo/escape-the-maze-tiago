using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    public bool disableObject = true;
    public GameObject ppcam;
    public string targetTag;
    public UnityEvent<GameObject> onEnterEvent;
    public UnityEvent<GameObject> onExitEvent;


    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == targetTag){
            onEnterEvent.Invoke(other.gameObject);
            if(disableObject){
                if(ppcam){
                ppcam.SetActive(false);
                }
                gameObject.SetActive(false);
            }
            
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == targetTag){
            onExitEvent.Invoke(other.gameObject);
            if(disableObject){
                if(ppcam){
                ppcam.SetActive(false);
                }
                gameObject.SetActive(false);
            }
        }
    }
}
