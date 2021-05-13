﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 25;
    public GameObject HitParticlePrefab;
    private void FixedUpdate() {
        transform.Translate(speed * Time.fixedDeltaTime * Vector3.up);
    }
    private void OnCollisionEnter(Collision other) {
        Vector3 hitNormal = other.contacts[0].normal;
        Instantiate(HitParticlePrefab, transform.position, Quaternion.Euler(hitNormal.x, hitNormal.y, hitNormal.z));


        DestroyProjectile();
    }
    private void Start() {
        Invoke("DestroyProjectile", 3.0f);
    }
    
    void DestroyProjectile() {
        Destroy(this.gameObject);
    }
}
