using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class translateZombie : MonoBehaviour
{
    public GameObject zombie;
    public float crawlSpeed = 1f;
    public Animator animator;

    public AudioSource jumpscareSound;
    public GameObject ppcam;

    public void Update()
    {
          //Debug.Log("Inside update");
        // Check if the crawl animation is playing
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("crawl")) // Replace with your actual crawl animation name
        {
            //Debug.Log("Inside Crawl");
            // Translate the character forward
            zombie.transform.Translate(Vector3.forward * crawlSpeed * Time.deltaTime);
        }
    }

    public void StartAnimate(){
        Invoke("Start",2f);
    }

    private void Start(){
        animator.SetTrigger("crawl");
        jumpscareSound.Play();
        //ppcam.SetActive(true);
    }

    public void Destroy(){
        Invoke("DestroyObjects", 5f);
    }
    private void DestroyObjects(){
        ppcam.SetActive(false);
        zombie.SetActive(false);
    }

}
