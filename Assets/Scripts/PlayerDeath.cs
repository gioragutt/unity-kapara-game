using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
