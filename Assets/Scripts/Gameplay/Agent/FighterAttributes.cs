using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Basic.Events;
namespace Basic.Agents
{
    public class FighterAttributes : MonoBehaviour
    {
        public int currentLevel = 1;
        public int currentExp = 0;
        public int expToNextLevel = 100;

        public float maxHealth = 100f;
        private float currentHealth;
        
        public float maxStamina = 100f;
        private float currentStamina;
        public bool isSprinting;
        public int expReward = 10;
        public GameObject ragdollPrefab;
        public bool canSprint { get; private set; } = true;
        public float MaxHealth
        {
            get { return maxHealth; }
            set
            {
                maxHealth = value;
                currentHealth = value;
            }
        }
        public float MaxStamina
        {
            get { return maxStamina; }
            set
            {
                maxStamina = value;
                currentStamina = value;
            }
        }
        public float CurrentHealth
        {
            get { return currentHealth; }
            set
            {
                if (value >= maxHealth) { currentHealth = maxHealth; }
                else if (value <= 0) 
                {
                    currentHealth = 0;
                    Die();
                }
                else { currentHealth = value; }
            }
        }
        public float CurrentStamina
        {
            get { return currentStamina; }
            set
            {
                if (value >= maxStamina)
                { 
                    currentStamina = maxStamina;
                    canSprint = true;
                }
                else if (value <= 0) 
                { 
                    currentStamina = 0;
                    canSprint = false;
                }
                else { currentStamina = value; }
            }
        }
        protected virtual void Start()
        {
            CurrentHealth = maxHealth;
            CurrentStamina = maxStamina;
        }
        protected virtual void Update()
        {
            if (isSprinting) CurrentStamina -= Time.deltaTime * 10f;
            else CurrentStamina += Time.deltaTime * 5f;
        }
        private int CalculateExpToNextLevel(int currentLevel)
        {
            return (int)(100 * Mathf.Pow(1.15f, currentLevel - 1));
        }
        virtual public void LevelUp()
        {
            ++currentLevel;
            currentExp -= expToNextLevel;
            expToNextLevel = CalculateExpToNextLevel(currentLevel);
            Debug.Log("Now level " + currentLevel);
        }
        public void AddExperience(int amount)
        {
            currentExp += amount;
            while (currentExp >= expToNextLevel) LevelUp();
        }
        public void AddCurrentStamina(float amount)
        {
            CurrentStamina += amount;
        }
        public void AddMaxHealth(int amount)
        {
            MaxHealth += amount;
        }
        public void AddMaxStamina(int amount)
        {
            MaxStamina += amount;
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
                    playerFighter.AddExperience(expReward);
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
