using UnityEngine;

namespace Basic
{
    public class BasicPlayerFighter : BasicFighter
    {

        //public
        public Transform characterBack;
        public float rollCost = 20f;

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

        public override void DoRoll(Vector3 rollDirection) {
            base.DoRoll(rollDirection);
            if (controller == null) return;
            controller.center = new Vector3(0f, 0.4f, 0f);
            controller.height = 0.01f;
            Debug.Log(rollDirection);
            controller.Move(rollDirection * rollSpeed * Time.deltaTime);
            AddStamina(-rollCost);
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
