using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Basic.Agents
{
    public class WeaponCollisionController : MonoBehaviour
    {
        public float damageAmount = 10f;
        private Collider weaponCollider;
        // Start is called before the first frame update
        protected virtual void Start()
        {
            weaponCollider = GetComponent<Collider>();
            weaponCollider.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        protected void OnTriggerEnter(Collider other)
        {
            if (this.tag == other.tag || other.tag == "Untagged") return;
            BasicFighter enemyFighter = other.GetComponent<BasicFighter>();
            if (enemyFighter != null)
            {
                enemyFighter.AddHealth(-damageAmount);
                Debug.Log(other.name + " has been hit for " + damageAmount);
            }
        }

        public void EnableCollider()
        {
            weaponCollider.enabled = true;
        }

        public void DisableCollider()
        {
            weaponCollider.enabled = false;
        }
    }
}
