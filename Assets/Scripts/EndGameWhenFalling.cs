
using UnityEngine;

public class EndGameWhenFalling : MonoBehaviour
{
    public PlayerMovement player;
    public float height = -2f;
    public GameObject explosionEffect;

    void FixedUpdate()
    {
        if (GameManager.Get().GameHasEnded || player.rigidBody.position.y >= height)
            return;

        Debug.Log("Fell off the platform");
        DestroyPlayer();
        GameManager.Get().EndGame();
    }

    private void DestroyPlayer()
    {
        player.StopPlayer();

        FindObjectOfType<FollowPlayer>().enabled = false;

        AudioManager.Instance.Play("ExplosionSound");
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
