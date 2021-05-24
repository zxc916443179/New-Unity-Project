using UnityEngine;
using UnityEngine.Events;
using Unity.TPS.Audio;
namespace Unity.TPS.Game {
    public class WeaponController : MonoBehaviour {
        public Transform weaponMuzzle;
        public GameObject bulletPrefab;
        public int maxAmmo = 30;
        int m_currentAmmo;
        public int CurrentAmmo {
            get {
                return m_currentAmmo;
            }
        }
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
        public AudioController audioShootController;
        public AudioController audioReloadController;
        public UnityAction NeedReload;
        private void Start() {
            m_currentAmmo = maxAmmo;
            fireMode = 0;
            m_LastShootTime = Time.time;
        }
        private void Update() {
        }
        public bool fire() {
            return fire(true, false, false);
        }
        public bool fire(bool inputDown, bool inputHeld, bool inputUp) {
            
            if (BurstingBullets > 0) {
                return handleBurst();
            }
            if (m_currentAmmo <= 0) {
                reload();
                return false;
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
                if (NeedReload != null) NeedReload.Invoke();
                return handleReload();
            }
            return false;
        }
        public bool handleReload() {
            try {
                m_currentAmmo = maxAmmo;
                audioReloadController.Play();
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
                audioShootController.Play();
                m_LastShootTime = Time.time;
                m_currentAmmo --;
                InstantiateBullet();
                return true;
            }
            return false;
        }
        public bool handleBurst() {
            if (m_currentAmmo <= 0) BurstingBullets = 0;
            else if (m_LastShootTime + BurstingDelay < Time.time) {
                audioShootController.Play();
                BurstingBullets --;
                m_currentAmmo --;
                m_LastShootTime = Time.time;
                InstantiateBullet();
                return true;
            }
            return false;
        }
        void InstantiateBullet() {
            GameObject go = Instantiate(bulletPrefab, weaponMuzzle.position, weaponMuzzle.rotation);
            go.transform.tag = transform.tag;
        }
        public string GetFireMode() {
            return fireMode.ToString();
        }
    }
}