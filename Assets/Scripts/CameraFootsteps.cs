using UnityEngine;

public class CameraFootsteps : MonoBehaviour
{
    public AudioSource footstepAudioSource; // Assign this in the inspector
    public AudioClip footstepClip;          // Assign the footstep sound clip in the inspector
    public AudioClip scaredClip;            // Assign the scared sound clip in the inspector
    public float minimumMovementThreshold = 0.1f; // Minimum distance the camera must move to play sound
    public bool isScared = false; // Variable to control scared state

    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
        // If the AudioSource is not assigned, try to get it from the current gameObject
        if (footstepAudioSource == null)
        {
            footstepAudioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (isScared)
        {
            // Play the scared sound for 5 seconds if not already playing
                footstepAudioSource.clip = scaredClip;
                footstepAudioSource.Play();
                Invoke("StopScaredSound", 7f); // Stop the sound after 7 seconds
        }
        else
        {
            // Check if the camera has moved significantly
            if (Vector3.Distance(transform.position, lastPosition) > minimumMovementThreshold)
            {
                // Play footstep sound if not already playing
                if (!footstepAudioSource.isPlaying)
                {
                    footstepAudioSource.clip = footstepClip;
                    footstepAudioSource.Play();
                }
            }
        }

        // Update the last position for the next frame's comparison
        lastPosition = transform.position;
    }

    void StopScaredSound()
    {
        if (footstepAudioSource.clip == scaredClip)
        {
            footstepAudioSource.Stop();
            footstepAudioSource.clip = footstepClip; // Reset the clip to footstep after scared sound stops
        }
    }

    public void TriggerScared(){
        isScared = true;
    }
}
