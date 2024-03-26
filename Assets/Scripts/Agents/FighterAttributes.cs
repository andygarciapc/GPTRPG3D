using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Basic
{
    public class FighterAttributes : MonoBehaviour
    {
        public float maxHealth = 100f;
        private float currHealth;
        public float maxStamina = 100f;
        private float currStamina;
        public GameObject ragdollPrefab;

        private void Start()
        {
            currHealth = maxHealth;
        }

        public void TakeDamage(float damageAmount)
        {
            currHealth -= damageAmount;

            if (currHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log(this.name + " died.");
            GameObject ragdoll;
            if (ragdollPrefab != null) ragdoll = Instantiate(ragdollPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
