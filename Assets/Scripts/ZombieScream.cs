using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScream : MonoBehaviour
{
    public GameObject zombie;
    private Animator animator;
    public Collider trigger;
    private AudioSource scream;
    public GameObject ppCam;

    void Start()
    {
        animator = zombie.GetComponent<Animator>();
        scream = zombie.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TriggerSequence());
        }
    }
    private IEnumerator TriggerSequence()
    {
        trigger.enabled = false;
        animator.SetTrigger("isScreaming");
        ppCam.SetActive(true);
        scream.Play();

        yield return new WaitForSeconds(2f);
        ppCam.SetActive(false);

        // Wait for 5 seconds
        yield return new WaitForSeconds(10f);

        // Reactivate the trigger
        trigger.enabled = true;
    }
}
