using System.Collections;
using UnityEngine;

public class JumpscareTrigger : MonoBehaviour
{
    public GameObject ghost; 
    public AudioSource jumpscareAudio; 
    public float ghostSpeed = 5f; // Speed at which ghost moves towards player
    private Transform playerFace; // Player's Camera (face) Transform component
    private bool hasTriggered = false; // To ensure the jumpscare happens only once

    private void Start()
    {
        playerFace = Camera.main.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            // Activate ghost after 1.5 seconds
            StartCoroutine(ActivateGhostAfterDelay(1.5f));
        }
    }

    private IEnumerator ActivateGhostAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        ghost.SetActive(true);

        // Start audio playback
        if (jumpscareAudio != null && !jumpscareAudio.isPlaying)
        {
            jumpscareAudio.Play();
        }
        // Start moving the ghost towards the player's face
        StartCoroutine(MoveGhost());
    }

    private IEnumerator MoveGhost()
    {
        while (Vector3.Distance(ghost.transform.position, playerFace.position) > 0.5f) // Adjust the distance as needed
        {
            Vector3 moveDirection = (playerFace.position - ghost.transform.position).normalized;
            ghost.transform.position += moveDirection * ghostSpeed * Time.deltaTime;
            yield return null;
        }

      // Deactivate ghost after it reaches the player's face or after some time
        ghost.SetActive(false);
    }
}
