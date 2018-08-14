
using UnityEngine;

public class EndGameWhenFalling : MonoBehaviour
{
    public PlayerMovement player;
    public float height = -2f;

    void FixedUpdate()
    {
        if (GameManager.Get().GameHasEnded || player.rigidBody.position.y >= height)
            return;

        Debug.Log("Fell off the platform");
        player.StopPlayer();
        Destroy(player.gameObject);
        GameManager.Get().EndGame();
    }
}
