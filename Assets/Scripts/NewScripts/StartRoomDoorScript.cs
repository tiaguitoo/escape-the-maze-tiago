using UnityEngine;

public class StartRoomDoorScript : MonoBehaviour
{
    private int hitCount = 0;
    public DoorAnimationController DoorAnimController;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Inside Collisoon");
        // Check if the collider has the tag 'Axe'
        if (collision.collider.tag == "Axe")
        {
            hitCount++; // Increment hit count
            Debug.Log("Door hit count: " + hitCount);

            // Check if the door has been hit 4 times
            if (hitCount == 1)
            {
                DoorAnimController.ToggleDoorAnimation();
            }
        }
    }
}
