using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Player.UI
{
    public class PauseController : MonoBehaviour
    {
        public KeyCode key = KeyCode.Escape;
        public GameObject pausePanel;

        public bool InMenu
        {
            get => PlayerGamePrefs.InMenu;
            set
            {
                PlayerGamePrefs.InMenu = value;

                pausePanel.SetActive(value);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(key))
            {
                InMenu = !InMenu;
            }
        }

        public void Resume_ButtonClick()
        {
            InMenu = !InMenu;
        }
        public void Setting_ButtonClick()
        {

        }
        public void Player_Dead()
        {
            PlayerGamePrefs.InDialog = true;
            Invoke(nameof(Restart_ButtonClick), 2);
        }
        public void Restart_ButtonClick()
        {
            PlayerGamePrefs.InDialog = false;
            InMenu = false;
            SceneManager.LoadScene(this.gameObject.scene.name);
        }
        public void Exit_ButtonClick()
        {
            Application.Quit();
        }
    }
}
