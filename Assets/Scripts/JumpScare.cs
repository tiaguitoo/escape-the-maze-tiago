using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject jumpCam;

    public Collider trigger;

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            trigger.enabled = false;
            jumpCam.SetActive(true);
            // playerCam.SetActive(false);
            StartCoroutine(EndJump());
        }
    }

    IEnumerator EndJump() {
        yield return new WaitForSeconds(2.03f);
        // playerCam.SetActive(true);
        jumpCam.SetActive(false);
        yield return new WaitForSeconds(25f);

        // Reactivate the trigger
        trigger.enabled = true;
    }
}
