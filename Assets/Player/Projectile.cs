using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] float damageCaused = 10f;

    void OnTriggerEnter(Collider collider) {
        Component damageableComponet = collider.gameObject.GetComponent(typeof(IDamageable));

        if (damageableComponet) {
            (damageableComponet as IDamageable).TakeDamage(damageCaused);
        }
    }
}
