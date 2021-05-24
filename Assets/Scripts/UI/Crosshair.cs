using UnityEngine;
using UnityEngine.UI;

namespace Unity.TPS.UI {
    public class Crosshair : MonoBehaviour {
        public Texture2D source;
        public Image V;
        private void Start() {
            Sprite sprite = Sprite.Create(source, new Rect(0, 0, source.width, source.height), new Vector2(0.5f, 0.5f));
            V.sprite = sprite;
        }
    }
}