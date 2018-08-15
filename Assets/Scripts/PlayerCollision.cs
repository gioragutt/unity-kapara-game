using Assets.Scripts;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject explosionEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag(Constants.Tags.OBSTACLE))
            return;

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
