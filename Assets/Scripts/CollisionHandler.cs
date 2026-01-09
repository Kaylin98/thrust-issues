using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        string ObjectTag = collision.gameObject.tag;
        
        switch (ObjectTag)
        {
            case "Friendly":
                Debug.Log("Collided with Friendly object.");
                break;
            case "Finish":
                Debug.Log("Collided with Finish object. Level Complete!");
                break;
            case "Fuel":
                Debug.Log("Collided with Fuel object. Refueling!");
                break;
            default:
                Debug.Log("Collided with an obstacle. Game Over!");
                break;
        }
    }
}
