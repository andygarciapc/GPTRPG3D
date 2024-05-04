using UnityEngine;
using System.Collections;
using Basic.Events;
namespace Basic.Agents
{
    public class PlayerFighter : BasicFighter
    {

        //public
        protected float rollCost = 20f;
        private Vector3 rollDirection;

        [SerializeField] private GameObject levelupMenu;

        //private
        private CharacterController controller;

        protected override void Start()
        {
            base.Start();
            controller = (tag == "Player") ? GetComponent<CharacterController>() : null;
            GameEventsManager.instance.playerEvents.PlayerLevelChange(currentLevel);
            GameEventsManager.instance.playerEvents.PlayerExperienceChange(currentExp);
        }
        protected override void Update()
        {
            base.Update();
        }

        private void OnEnable()
        {
            GameEventsManager.instance.playerEvents.onAddExperience += AddExperience;
        }
        private void OnDisable()
        {
            GameEventsManager.instance.playerEvents.onAddExperience -= AddExperience;
        }
        public override void DoRoll(Vector3 _rollDirection) {
            base.DoRoll(_rollDirection);
            if (controller == null) return;
            controller.center = new Vector3(0f, 0.4f, 0f);
            controller.height = 0.01f;
            //Debug.Log(_rollDirection);
            rollDirection = _rollDirection;
            //CurrentStamina -= rollCost;
        }
        public override void LevelUp()
        {
            base.LevelUp();
            Time.timeScale = 0;
            levelupMenu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        public void DoDash()
        {
            StartCoroutine(Dash(rollDirection));
        }
        IEnumerator Dash(Vector3 rollDirection)
        {
            float startTime = Time.time;
            while(Time.time < startTime + dashTime)
            {
                controller.Move(rollDirection * dashSpeed * Time.deltaTime);
                yield return null;
            }
        }
        public void ToggleCollider()
        {
            if (controller != null)
            {
                controller.center = new Vector3(0f, 0.93f, 0f);
                controller.height = 1.8f;
            }
        }
    }
}
