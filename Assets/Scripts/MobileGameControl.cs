using Assets.Scripts.Menus;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Assets.Scripts
{
    public class MobileGameControl : MenuShortcuts
    {
        public readonly GameOptions.MobileControlStyles mobileControlStyle;

        protected MobileGameControl(GameOptions.MobileControlStyles mobileControlStyle)
        {
            this.mobileControlStyle = mobileControlStyle;
        }
    }
}