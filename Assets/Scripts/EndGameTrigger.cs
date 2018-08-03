using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != Constants.Tags.PLAYER)
            return;

        other.GetComponent<PlayerMovement>().StopPlayer();
        GameManager.GetGameManager().CompleteLevel();
    }
}
