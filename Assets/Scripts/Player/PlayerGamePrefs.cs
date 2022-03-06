using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public static class PlayerGamePrefs
    {
        private static bool inMenu = false;
        public static bool InDialog = false;

        public static bool InMenu
        {
            get => inMenu;
            set
            {
                inMenu = value;
                Time.timeScale = (value ? 0 : 1);
                Cursor.lockState = (value ? CursorLockMode.None : CursorLockMode.Locked);
            }
        }
    }
}
