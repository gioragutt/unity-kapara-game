using Assets.Scripts;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement movement;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag(Constants.Tags.OBSTACLE))
            return;

        movement.enabled = false;
        var rigidbody = movement.GetComponent<Rigidbody>();
        rigidbody.AddForce(rigidbody.velocity * -0.5f);
    }
}
