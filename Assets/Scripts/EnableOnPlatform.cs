using UnityEngine;

namespace Assets.Scripts
{
    public class EnableOnPlatform : MonoBehaviour
    {
        public Utilities.PlatformType platform;

        private void Awake()
        {
            if (Utilities.Platform != platform)
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}
