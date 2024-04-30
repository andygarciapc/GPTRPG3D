using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Basic.Events;
namespace Basic.Agents
{
    public class FighterAttributes : MonoBehaviour
    {
        public int level = 1;
        public int currentExp = 0;
        public int expToNextLevel = 100;
        public float maxHealth = 100f;
        private float currHealth;
        public float maxStamina = 100f;
        public bool isSprinting;
        private float currStamina;
        public int expValue = 10;
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
        public float MaxStamina
        {
            get { return maxStamina; }
            set
            {
                maxStamina = value;
                currStamina = value;
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

        private int CalculateExpToNextLevel(int currentLevel)
        {
            return (int)(100 * Mathf.Pow(1.15f, currentLevel - 1));
        }

        virtual public void LevelUp()
        {
            ++level;
            currentExp -= expToNextLevel;
            expToNextLevel = CalculateExpToNextLevel(level);
            //maxHealth += 10;
            //maxStamina += 5;
            //currHealth = maxHealth;
            //currStamina = maxStamina;
            Debug.Log("Now level " + level);
        }
        public void AddMaxHealth(int amount)
        {
            MaxHealth += amount;
            Debug.Log("AddMaxHealth(" + amount + ")\n" + "MaxHealth = " + MaxHealth);
        }
        public void AddMaxStamina(int amount)
        {
            MaxStamina += amount;
            Debug.Log("AddMaxStamina(" + amount + ")\n" + "MaxStamina = " + MaxStamina);
        }
        public void AddExperience(int expAmount)
        {
            currentExp += expAmount;
            while (currentExp >= expToNextLevel) LevelUp();
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
            bool isPlayer = gameObject.tag == "Player" ? true : false;
            bool isEnemy = gameObject.tag == "Enemy" ? true : false;
            if (isEnemy)
            {
                FighterAttributes playerFighter = GameObject.Find("BasicPlayer").GetComponent<FighterAttributes>();
                if (playerFighter != null)
                {
                    GameEventsManager.instance.combatEvents.EnemyKilled();
                    playerFighter.AddExperience(expValue);
                }
                else Debug.Log("NULL PLAYERFIGHTER");
            }
            Destroy(gameObject);
            if (isPlayer)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

}
