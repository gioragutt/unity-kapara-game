using Assets.Scripts;
using UnityEngine;

public class LevelMobileShortcuts : MonoBehaviour {

	void Start ()
    {
        if (Utilities.Platform != Utilities.PlatformType.Mobile)
        {
            enabled = false;
        }
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Get().ShowPauseMenu();
        }
	}
}
