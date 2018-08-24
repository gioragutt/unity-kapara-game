using UnityEngine;

namespace Assets.Scripts
{
    public class MobileNoGuiControl : MobileGameControl
    {
        private struct ScreenSidesTouched
        {
            public readonly bool left;
            public readonly bool right;

            public ScreenSidesTouched(bool left, bool right)
            {
                this.left = left;
                this.right = right;
            }

            public static readonly ScreenSidesTouched None =
                new ScreenSidesTouched(false, false);
        }

        public PlayerMovement player;

        protected MobileNoGuiControl()
        : base(GameOptions.MobileControlStyles.NoGui)
        {
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Get().ShowPauseMenu();
            }

            var sidesTouched = DetectTouches();
            player.IsMovingLeft = sidesTouched.left;
            player.IsMovingRight = sidesTouched.right;
        }

        ScreenSidesTouched DetectTouches()
        {
            if (Input.touchCount == 0)
                return ScreenSidesTouched.None;

            var screenWidth = Screen.width;
            var leftTouched = false;
            var rightTouched = false;
            foreach (Touch touch in Input.touches)
            {
                var direction = TouchDirection(touch, screenWidth);
                if (direction > 0)
                {
                    rightTouched = true;
                }
                if (direction < 0)
                {
                    leftTouched = true;
                }
            }
            return new ScreenSidesTouched(leftTouched, rightTouched);
        }

        private static float TouchDirection(Touch touch, int screenWidth)
        {
            return touch.position.x <= screenWidth / 2 ? -1 : 1;
        }
    }
}
