using UnityEngine;
using Unity.TPS.Game;
namespace Unity.TPS.Gameplay {
    public class PlayerWeaponController : MonoBehaviour {
        PlayerCharacterController m_PlayerCharacterController;
        public WeaponController activeWeapon;
        InputHandler m_inputHandler;
        PlayerAnimatorController playerAnimatorController;
        bool isReloading = false;
        public bool IsAutoReload = true;
        void Awake() {
            m_inputHandler = GetComponent<InputHandler>();
            playerAnimatorController = GetComponent<PlayerAnimatorController>();
            activeWeapon.NeedReload += ForceReload;
        }
        void Update() {
            if (playerAnimatorController.GetStateInfo().IsName("heavy_combat_reload")) {
                isReloading = true;
            } else {
                isReloading = false;
            }
            if (!isReloading) {
                if(activeWeapon.fire(m_inputHandler.GetFireDown(), m_inputHandler.GetFireHeld(), m_inputHandler.GetFireRelease()))
                    playerAnimatorController.setShoot(activeWeapon.GetFireMode() == "Burst");
                if (m_inputHandler.GetSwitchFireMode()) {
                    activeWeapon.switchFireMode();
                }
                if (m_inputHandler.GetReload()) {
                    if(activeWeapon.reload()){
                        playerAnimatorController.setReload();
                    }
                }
            }
        }
        void ForceReload() {
            playerAnimatorController.setReload();
        }
    }
}