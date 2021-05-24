using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Unity.TPS.Game{
    public class Projectile : MonoBehaviour
    {
        public float speed = 25;
        public float damage = 15;
        public GameObject HitParticlePrefab;
        UnityAction<float, GameObject> onHit;
        private void FixedUpdate() {
            Vector3 start = transform.position;
            transform.Translate(speed * Time.fixedDeltaTime * Vector3.up);
            Vector3 direction = transform.position - start;
            Debug.DrawRay(transform.position, direction, Color.red, 0);
            if (Physics.Raycast(transform.position, direction,  out RaycastHit hit, 1f, -1, QueryTriggerInteraction.Ignore)) {
                if (hit.collider.tag == "Enemy" || hit.collider.tag == "Player") {
                    if (!hit.collider.gameObject.GetComponent<Health>().TakeDamage(damage, transform.tag)) {
                        return;
                    }
                }
                DestroyProjectile();
            }
        }
        private void OnCollisionEnter(Collision other) {
            Vector3 hitNormal = other.contacts[0].normal;
            // Instantiate(HitParticlePrefab, transform.position, Quaternion.Euler(hitNormal.x, hitNormal.y, hitNormal.z));
            print(other.collider.name);
            if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player" ) {
                if(!other.gameObject.GetComponent<Health>().TakeDamage(damage, transform.tag)) return;
            }
            DestroyProjectile();
        }
        private void Start() {
            transform.Rotate(new Vector3(transform.rotation.x + 90, transform.rotation.y, transform.rotation.z), Space.Self);
            Invoke("DestroyProjectile", 3.0f);
        }
        
        void DestroyProjectile() {
            Destroy(this.gameObject);
        }
    }
}