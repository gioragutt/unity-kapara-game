using UnityEngine;

namespace Assets.Scripts
{
    public class EndGameWhenFalling : MonoBehaviour
    {
        public PlayerMovement player;
        public float height = -2f;

        void FixedUpdate()
        {
            if (GameManager.Get().GameHasEnded || !player.enabled || player.rigidBody.position.y >= height)
            {
                return;
            }

            Debug.Log("Fell off the platform");
            GetComponent<PlayerDeath>().DestroyPlayer();
            GameManager.Get().EndGame();
        }
    }
}