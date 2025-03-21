using System.Collections;
using UnityEngine;

public class ClownFlash : MonoBehaviour
{
    public GameObject clown; 
    public float turnSpeed = 100f;
    public float runSpeed = 5f;
    public float runDuration = 1f; // The duration for which the zombie runs

    private Animator animator;
    private bool isTurning = false;
    private bool isRunning = false;
    private float runTimer;
    private float turnTimer;
    private float turnDuration = 0.895f;

    public GameObject ppCam;

    public float delay = 1f;

    void Start()
    {
        animator = clown.GetComponent<Animator>();
    }

    void Update()
    {
        if (isTurning)
        {
            // Rotate the zombie to the right
            clown.transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);

            // Check if the turn is complete and start running
            if (HasFinishedTurning())
            {
                isTurning = false;
                isRunning = true;
                runTimer = 0f;
                animator.SetTrigger("startRunning");
            }
        }
        else if (isRunning)
        {
            // Move the zombie forward
            clown.transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);

            // Check if the running duration is over
            runTimer += Time.deltaTime;
            if (runTimer >= runDuration)
            {
                isRunning = false;
                animator.SetTrigger("stopRunning");
                // Additional code to handle post-run behavior
                clown.SetActive(false);
                ppCam.SetActive(false);
            }
        }
    }
    public void animateClown(){
            Invoke("startClownAnim",delay);
    }

    private void startClownAnim(){
        isTurning = true;
            turnTimer = 0f;
            animator.SetTrigger("turnLeft");
            ppCam.SetActive(true);
            GetComponent<AudioSource>().Play();
    }

    private bool HasFinishedTurning()
    {
        turnTimer += Time.deltaTime;
        if (turnTimer >= turnDuration)
        {
            turnTimer = 0f; // Reset the timer for the next turn
            return true;
        }
        return false;
    }
}
