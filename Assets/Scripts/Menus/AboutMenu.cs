using Assets.Scripts;
using UnityEngine;

public class AboutMenu : MonoBehaviour
{
    public void Resume()
    {
        GameManager.Get().ShowStartMenu();
    }
}
