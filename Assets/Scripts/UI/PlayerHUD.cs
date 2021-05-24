using UnityEngine;
using UnityEngine.UI;
using Unity.TPS.Game;
using Unity.TPS.Gameplay;
namespace Unity.TPS.UI {
    public class PlayerHUD : MonoBehaviour {
        public Image HealthFillImage;
        public Health health;
        public PlayerWeaponController playerWeaponController;
        public Ammos ammos;
        private void Start() {
                
        }
        private void Update() {
            HealthFillImage.fillAmount = health.GetCurrentHealth() / health.maxHealth;
            ammos.SetText(playerWeaponController.activeWeapon.CurrentAmmo.ToString(), playerWeaponController.activeWeapon.maxAmmo.ToString());
        }
    }
}