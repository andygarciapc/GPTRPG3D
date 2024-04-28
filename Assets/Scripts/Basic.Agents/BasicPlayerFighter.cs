using UnityEngine;
using System;
using System.Collections;

namespace Basic
{
    public class BasicPlayerFighter : BasicFighter
    {

        //public
        public Transform characterBack;
        public float rollCost = 20f;
        private Vector3 rollDirection;

        [SerializeField] private GameObject levelupMenu;

        //private
        private CharacterController controller;

        protected override void Start()
        {
            base.Start();
            controller = (tag == "Player") ? GetComponent<CharacterController>() : null;
        }

        protected override void Update()
        {
            base.Update();
        }

        public override void DoRoll(Vector3 _rollDirection) {
            base.DoRoll(_rollDirection);
            if (controller == null) return;
            controller.center = new Vector3(0f, 0.4f, 0f);
            controller.height = 0.01f;
            Debug.Log(_rollDirection);
            rollDirection = _rollDirection;
            //controller.Move(rollDirection * dashSpeed * Time.deltaTime);
            //StartCoroutine(Dash(rollDirection));
            AddStamina(-rollCost);
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
