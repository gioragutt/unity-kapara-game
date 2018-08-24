using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerCollision : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.collider.CompareTag(Constants.Tags.OBSTACLE))
                return;

            GetComponent<PlayerDeath>().DestroyPlayer();
            GameManager.Get().EndGame();
        }
    }
}