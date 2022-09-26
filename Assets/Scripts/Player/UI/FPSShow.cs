using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player.UI
{
    public class FPSShow : MonoBehaviour
    {
        public Text text;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void LateUpdate()
        {
            var fps = 1 / Time.deltaTime;

            if (fps > 30)
                text.color = Color.green;
            else if (fps > 20)
                text.color = Color.yellow;
            else
                text.color = Color.red;

            text.text = string.Format("FPS: {0:0}({1:N2} ms)", fps, Time.deltaTime * 1000);

        }
    }
}