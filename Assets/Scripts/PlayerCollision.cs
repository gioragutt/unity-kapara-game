using Assets.Scripts;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement movement;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag(Constants.Tags.OBSTACLE))
            return;

        GameManager.Get().EndGame();
        movement.StopPlayer();
    }
}
