using UnityEngine;
using Unity.TPS.Gameplay;

namespace Unity.TPS.Game {
    public class WeaponController : MonoBehaviour {
        public Transform weaponMuzzle;
        public GameObject bulletPrefab;
        public int maxAmmo = 30;
        int m_currentAmmo;
        enum FireMode {
            Single,
            Burst,
            Auto
        }
        FireMode fireMode;
        float m_LastShootTime;
        public float ShootDelay = 0.5f;
        public float BurstingDelay = 0.1f;
        int BurstingBullets = 0;
        private void Start() {
            m_currentAmmo = maxAmmo;
            fireMode = 0;
            m_LastShootTime = Time.time;
        }
        private void Update() {
        }
        public bool fire(bool inputDown, bool inputHeld, bool inputUp) {
            
            if (BurstingBullets > 0) {
                return handleBurst();
            }
            if (m_currentAmmo < 0) {
                return reload();
            }
            bool shoot = inputDown || inputHeld;
            switch(fireMode) {
                case FireMode.Single:
                    if (inputDown) {
                        return handleFire();
                    }
                    return false;
                case FireMode.Burst:
                    if (inputDown) {
                        if (m_LastShootTime + ShootDelay < Time.time) {
                            BurstingBullets = 3;
                            return handleBurst();
                        }
                    }
                    return false;
                case FireMode.Auto:
                    if (inputHeld) {
                        return handleFire();
                    }
                    return false;
                default:
                    return false;
            }
        }
        public bool reload() {
            if (m_currentAmmo < maxAmmo) {
                return handleReload();
            }
            return false;
        }
        public bool handleReload() {
            try {
                m_currentAmmo = maxAmmo;
                return true;
            } catch (System.Exception e) {
                print(e.ToString());
            }
            return false;
        }
        public void switchFireMode() {
            if (fireMode == FireMode.Auto)fireMode = FireMode.Single;
            else fireMode += 1;
        }
        private bool handleFire() {
            if (m_currentAmmo > 0 && m_LastShootTime + ShootDelay < Time.time) {
                m_LastShootTime = Time.time;
                m_currentAmmo --;
                Instantiate(bulletPrefab, weaponMuzzle.position, weaponMuzzle.rotation);
                return true;
            }
            return false;
        }
        public bool handleBurst() {
            if (m_currentAmmo <= 0) BurstingBullets = 0;
            else if (m_LastShootTime + BurstingDelay < Time.time) {
                BurstingBullets --;
                m_currentAmmo --;
                m_LastShootTime = Time.time;
                Instantiate(bulletPrefab, weaponMuzzle.position, weaponMuzzle.rotation);
                return true;
            }
            return false;
        }
        public string GetFireMode() {
            return fireMode.ToString();
        }
    }
}