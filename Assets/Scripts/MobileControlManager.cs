using UnityEngine;

namespace Assets.Scripts
{
    public class MobileControlManager : MonoBehaviour
    {
        private void Start()
        {
            SetControlActiveOnAll(GameOptions.MobileControlStyle, true);
            GameOptions.MobileControlStyleChanged += OnMobileControlStyleChanged;
        }

        private void OnMobileControlStyleChanged(object sender, GameOptions.MobileControlStyleChangedEventArgs e)
        {
            SetControlActiveOnAll(e.MobileControlStyle, false);
        }

        private static void SetControlActiveOnAll(GameOptions.MobileControlStyles controlStyle, bool isStartup)
        {
            Debug.LogFormat("<MobileControlManager> Called with style={0}, isStartup={1}", controlStyle, isStartup);
            Utilities.FindObjectsOfType<MobileGameControl>().ForEach(
                control => SetControlActive(control, controlStyle, isStartup));
        }

        private static void SetControlActive(
            MobileGameControl control,
            GameOptions.MobileControlStyles controlStyle,
            bool isStartup)
        {
            var isActive = control.mobileControlStyle == controlStyle;
            Debug.LogFormat("<MobileControlManager> Setting {0} Active = {1}", control.GetType().Name, isActive);
            control.ForceDisabled = !isActive;
            control.gameObject.SetActive(isActive);
            control.enabled = true;
            DisableIfChangedInOptions(control, isStartup, isActive);
        }

        /// <summary>
        /// Due to how controls are disabled and enabled during the menu scene flow,
        /// The control needs to be disabled in order to be re-enabled when coming
        /// Back to the game
        /// </summary>
        /// <param name="control">The control that is handled</param>
        /// <param name="isStartup">Wether the action is being done during the level start up</param>
        /// <param name="isActive">Wether the component has been just actived or deactived</param>
        private static void DisableIfChangedInOptions(MobileGameControl control, bool isStartup, bool isActive)
        {
            if (!isStartup && isActive)
            {
                Debug.LogFormat("<MobileControlManager> {0} Disabled During Menu", control.GetType().Name, false);
                control.enabled = false;
            }
        }
    }
}
