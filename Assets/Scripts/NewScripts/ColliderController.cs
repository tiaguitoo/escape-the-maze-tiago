using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public BoxCollider targetCollider; // Assign this in the Unity Editor

    public void enableCollider()
    {
        // Disable the Box Collider
        if (targetCollider != null)
        {
            targetCollider.enabled = true;
        }
    }
}
