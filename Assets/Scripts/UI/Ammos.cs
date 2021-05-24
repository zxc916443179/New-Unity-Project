using UnityEngine;
using UnityEngine.UI;
using Unity.TPS.Gameplay;
namespace Unity.TPS.UI {
    public class Ammos : MonoBehaviour {
        public Text text_vol;
        public Text text_max;

        public void SetText(string vol, string max) {
            text_max.text = max;
            text_vol.text = vol;
        }
    }
}