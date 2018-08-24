using Assets.Scripts.Audio;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerDeath : MonoBehaviour
    {
        public PlayerMovement player;
        public GameObject explosionEffect;

        public void DestroyPlayer()
        {
            player.StopPlayer();

            FindObjectOfType<FollowPlayer>().enabled = false;

            AudioManager.Instance.Play("ExplosionSound");
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
