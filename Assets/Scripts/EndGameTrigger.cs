using Assets.Scripts;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != Constants.Tags.PLAYER)
            return;

        other.GetComponent<PlayerMovement>().StopPlayer();
        GameManager.Get().CompleteLevel();
    }
}
