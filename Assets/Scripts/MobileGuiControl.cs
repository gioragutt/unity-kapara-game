using UnityEngine;

namespace Assets.Scripts
{
    public class MobileGuiControl : MobileGameControl
    {
        protected MobileGuiControl()
        : base(GameOptions.MobileControlStyles.Gui)
        {
        }

        public void StartMovingLeft()
        {
            Debug.Log("StartMovingLeft");
        }

        public void StartMovingRight()
        {
            Debug.Log("StartMovingRight");
        }

        public void StopMovingLeft()
        {
            Debug.Log("StopMovingLeft");
        }

        public void StopMovingRight()
        {
            Debug.Log("StopMovingRight");
        }

        public void PauseGame()
        {
            GameManager.Get().ShowPauseMenu();
        }
    }
}