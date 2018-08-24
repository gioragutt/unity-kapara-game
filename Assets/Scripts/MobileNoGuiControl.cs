using UnityEngine;

namespace Assets.Scripts
{
    public class MobileNoGuiControl : MobileGameControl
    {
        private struct ScreenSidesTouched
        {
            public bool left;
            public bool right;

            public ScreenSidesTouched(bool left, bool right)
            {
                this.left = left;
                this.right = right;
            }

            public static readonly ScreenSidesTouched None =
                new ScreenSidesTouched(false, false);
        }

        protected MobileNoGuiControl()
        : base(GameOptions.MobileControlStyles.NoGui)
        {
        }

        private void Update()
        {
            var sidesTouched = DetectTouches();
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
