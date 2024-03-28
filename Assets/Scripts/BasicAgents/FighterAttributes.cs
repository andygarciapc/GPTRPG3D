using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Basic
{
    public class FighterAttributes : MonoBehaviour
    {
        public float maxHealth = 100f;
        private float currHealth;
        public float maxStamina = 100f;
        public bool isSprinting;
        private float currStamina;
        public GameObject ragdollPrefab;
        public bool canSprint { get; private set; } = true;

        public float MaxHealth
        {
            get { return maxHealth; }
            set
            {
                maxHealth = value;
                currHealth = value;
            }
        }

        public float CurrentHealth
        {
            get { return currHealth; }
        }
        public float CurrentStamina
        {
            get { return currStamina; }
        }

        protected virtual void Start()
        {
            currHealth = maxHealth;
            currStamina = maxStamina;
        }
        protected virtual void Update()
        {
            if (isSprinting) AddStamina(-(Time.deltaTime * 10f));
            else AddStamina(Time.deltaTime * 5f);
        }

        public void AddHealth(float damageAmount)
        {
            currHealth += damageAmount;

            if (currHealth <= 0)
            {
                Die();
            }
        }
        public void AddStamina(float value)
        {
            currStamina += value;
            if(currStamina < 0)
            {
                canSprint = false;
                currStamina = 0;
            }
            if (currStamina == maxStamina) canSprint = true;
        }

        private void Die()
        {
            Debug.Log(this.name + " died.");
            GameObject ragdoll;
            if (ragdollPrefab != null) ragdoll = Instantiate(ragdollPrefab, transform.position, transform.rotation);
            bool returnMenu = gameObject.tag == "Player" ? true : false;
            Destroy(gameObject);
            if (returnMenu)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

}
