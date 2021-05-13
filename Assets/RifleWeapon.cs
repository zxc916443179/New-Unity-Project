using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleWeapon : MonoBehaviour
{
    
    public GameObject BulletPrefab;
    public Transform ShootPoint;
    public void Fire() {
        Instantiate(BulletPrefab, ShootPoint.position, ShootPoint.rotation);
    }
}
