using System.Collections.Generic;
using UnityEngine;
using Unity.TPS.Game;

namespace Unity.TPS.Gameplay {
    [RequireComponent(typeof(InputHandler))]
    public class WeaponManager {
        public enum WeaponState {
            Up,
            Down
        }
        public WeaponController StartingWeapon;
        
    }
}