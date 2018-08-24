using Assets.Scripts.Audio;
using UnityEngine;

namespace Assets.Scripts
{
    public class EndGameTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != Constants.Tags.PLAYER)
            {
                return;
            }

            AudioManager.Instance.StopAllPlayingSounds();
            AudioManager.Instance.Play("LevelCompleteSound");

            other.GetComponent<PlayerMovement>().StopPlayer();
            GameManager.Get().CompleteLevel();
        }
    }
}
