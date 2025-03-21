using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationTrigger : MonoBehaviour
{
    public Collider trigger;

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Monster")){
            StartCoroutine(ReEnable());
            trigger.enabled = false;
        }
    }

    IEnumerator ReEnable() {
        yield return new WaitForSeconds(5f);
        trigger.enabled = true;
    }
}
