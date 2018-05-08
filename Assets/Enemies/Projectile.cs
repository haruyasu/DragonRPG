using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float damageCaused;
    public float projectileSpeed;

    void OnTriggerEnter(Collider collider) {
        Component damageableComponet = collider.gameObject.GetComponent(typeof(IDamageable));

        if (damageableComponet) {
            (damageableComponet as IDamageable).TakeDamage(damageCaused);
        }
    }
}
