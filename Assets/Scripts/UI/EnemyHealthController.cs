using UnityEngine;
using Unity.TPS.Game;
using System;
using UnityEngine.UI;
namespace Unity.TPS.UI {
    public class EnemyHealthController : MonoBehaviour {
        public Health health;
        public Image HealthBarImage;
        public Transform HealthBarPivot;
        float width;
        private void Update() {
            HealthBarImage.fillAmount = health.GetCurrentHealth() / health.maxHealth;
            if (Camera.main != null)
                HealthBarPivot.LookAt(Camera.main.transform.position);
        }
    }
}