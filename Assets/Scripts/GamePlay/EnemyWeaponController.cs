using UnityEngine;
using Unity.TPS.Game;
namespace Unity.TPS.Gameplay
{
    public class EnemyWeaponController : MonoBehaviour {
        public Transform Hand;
        WeaponController activeWeapon;
        private void Start() {
            for(int i = 0; i < Hand.childCount; i ++) {
                var child = Hand.GetChild(i).gameObject;
                if (i > 0 && i != 3) {
                    child.SetActive(false);
                }
                if (i == 3) {
                    activeWeapon = child.GetComponent<WeaponController>();
                }
            }
        }

        public void Attack() {
            activeWeapon.fire();
        }
    }   
}