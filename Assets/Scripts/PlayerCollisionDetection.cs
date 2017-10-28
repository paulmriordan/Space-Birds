using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
    public event System.Action OnPlayerCollision;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (OnPlayerCollision != null)
        {
            OnPlayerCollision();
        }
    }
}