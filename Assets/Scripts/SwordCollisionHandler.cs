using UnityEngine;

namespace Basic
{

    public class SwordCollisionHandler : MonoBehaviour
    {
        public float damageAmount = 10f;
        private Collider swordCollider;

        private void Start()
        {
            swordCollider = GetComponent<Collider>();
            swordCollider.enabled = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if ((this.tag == "Player" && other.tag == "Player") || other.tag == "Untagged") return;
            Fighter enemyFighter = other.GetComponent<Fighter>();
            if (enemyFighter != null)
            {
                enemyFighter.fighterAttributes.TakeDamage(damageAmount);
                Debug.Log(other.name + " has been hit for " + damageAmount);
            }
        }

        public void EnableCollider()
        {
            swordCollider.enabled = true;
        }
        public void DisableCollider()
        {
            swordCollider.enabled = false;
        }
    }

}
