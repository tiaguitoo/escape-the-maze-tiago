using UnityEngine;

public class PlayRandomAudioOnProximity : MonoBehaviour
{
    public AudioClip[] audioClips; // Array to store your audio clips
    private AudioSource audioSource;
    private int lastPlayedIndex = -1; // To store the last played clip's index
    private SphereCollider sphereCollider;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sphereCollider = GetComponent<SphereCollider>(); // Reference the SphereCollider component
    }

    // Play a random audio clip
    private void PlayRandomClip()
    {
        if (audioClips.Length == 0) return;

        int randomIndex = Random.Range(0, audioClips.Length);

        // Ensure the same clip doesn't play twice in a row if there are multiple clips
        while (randomIndex == lastPlayedIndex && audioClips.Length > 1)
        {
            randomIndex = Random.Range(0, audioClips.Length);
        }

        audioSource.clip = audioClips[randomIndex];
        audioSource.Play();

        lastPlayedIndex = randomIndex; // Store the index of the now playing clip

        // Disable the collider after playing
        sphereCollider.enabled = false;
    }

    // This function is called when another collider enters the trigger zone of this GameObject
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player (adjust this condition as needed)
        if (other.CompareTag("Player"))
        {
            PlayRandomClip();
        }
    }
}
