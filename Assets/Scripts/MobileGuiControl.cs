namespace Assets.Scripts
{
    public class MobileGuiControl : MobileGameControl
    {
        public PlayerMovement player;

        protected MobileGuiControl()
        : base(GameOptions.MobileControlStyles.Gui)
        {
        }

        public void StartMovingLeft()
        {
            player.IsMovingLeft = true;
        }

        public void StartMovingRight()
        {
            player.IsMovingRight = true;
        }

        public void StopMovingLeft()
        {
            player.IsMovingLeft = false;
        }

        public void StopMovingRight()
        {
            player.IsMovingRight = false;
        }

        public void PauseGame()
        {
            GameManager.Get().ShowPauseMenu();
        }
    }
}