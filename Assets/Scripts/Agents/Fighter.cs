using UnityEngine;
using System.Collections;


namespace Basic
{
    public class Fighter : MonoBehaviour
    {
        private Animator animator;
        private bool hasAnimator;
        private bool hasCollider;
        private int animIDroll, animIDsheathe, animIDattackstance;
        //private Collider collider;
        private CharacterController controller;
        //private Rigidbody rb;

        public Transform characterHand;
        public Transform characterBack;
        public Transform sword;
        public SwordCollisionHandler swordCollisionHandler;
        private bool attackStance;

        public float cooldownTime = 1.5f;
        public static int noOfClicks = 0;
        public FighterAttributes fighterAttributes;
        //private float nextFireTime = 0f;
        //float lastClickedTime = 0;
        // maxComboDelay = 1;
        //float lastAttackTime;

        public GameObject ragdollPrefab;

        public float rollSpeed = 10f;

        enum ComboState
        {
            None,
            Attack1,
            Attack2,
            Attack3
        }
        private ComboState currentComboState = ComboState.None;

        public bool AttackStance
        {
            get { return attackStance; }
        }

        private void Start()
        {
            attackStance = false;
            hasCollider = true;
            hasAnimator = TryGetComponent(out animator);
            //hasCollider = TryGetComponent(out collider);
            controller = (tag == "Player") ? GetComponent<CharacterController>() : null;
            AssignAnimationIDs();
            fighterAttributes = GetComponent<FighterAttributes>();
        }

        private void AssignAnimationIDs()
        {
            animIDroll = Animator.StringToHash("Roll");
            animIDsheathe = Animator.StringToHash("Sheathe");
            animIDattackstance = Animator.StringToHash("AttackStance");
        }

        private void Update()
        {
            if (!hasAnimator) return;


            if (!(tag == "Player")) return;
            UpdateSwordPosition();
            if (IsPlayingAttackAnimation()) swordCollisionHandler.EnableCollider();
            else swordCollisionHandler.DisableCollider();

        }

        private void UpdateSwordPosition()
        {
            if (!attackStance)
            {
                sword.position = characterBack.position;
                sword.rotation = characterBack.rotation;
            }
            else
            {
                sword.position = characterHand.position;
                sword.rotation = characterHand.rotation;
            }
        }

        float attackStartTime = 0f;
        float inputWindowStart = 0.5f;
        float inputWindowEnd = 1.0f;
        public void DoAttack()
        {
            // Check if an attack animation is already playing
            if (IsPlayingAttackAnimation())
            {
                swordCollisionHandler.EnableCollider();
                // If an attack animation is playing, check if it's within the input window for triggering the next attack
                if (Time.time - attackStartTime > inputWindowStart && Time.time - attackStartTime < inputWindowEnd)
                {
                    // Trigger the next attack if player inputs within the window
                    TriggerNextAttack();
                }
                else
                {
                    // Reset combo state if player misses the input window
                    ResetCombo();
                }
            }
            else
            {
                // Start the first attack animation and record its start time
                StartAttackAnimation(ComboState.Attack1);
            }
        }

        private void TriggerNextAttack()
        {
            switch (currentComboState)
            {
                case ComboState.Attack1:
                    StartAttackAnimation(ComboState.Attack2);
                    break;
                case ComboState.Attack2:
                    StartAttackAnimation(ComboState.Attack3);
                    break;
                case ComboState.Attack3:
                    // Reset back to the first attack for looping combos
                    ResetCombo();
                    break;
            }
        }

        private void StartAttackAnimation(ComboState nextState)
        {
            // Trigger the appropriate attack animation and record its start time
            animator.SetTrigger("Attack" + (int)nextState);
            currentComboState = nextState;
            attackStartTime = Time.time;
        }

        private void ResetCombo()
        {
            // Reset the combo state and clear attack start time
            currentComboState = ComboState.None;
            attackStartTime = 0f;
        }

        private bool IsPlayingAttackAnimation()
        {
            // Check if any of the attack animation states are currently playing
            return animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
        }


        public void DoRoll(Vector3 rollDirection)
        {
            if (!hasAnimator) return;
            animator.SetTrigger(animIDroll);

            if (!hasCollider) return;
            if (controller == null) return;
            controller.center = new Vector3(0f, 0.4f, 0f);
            controller.height = 0.01f;
            Debug.Log(rollDirection);
            controller.Move(rollDirection * rollSpeed * Time.deltaTime);
        }

        public void ToggleCollider()
        {
            if (!hasCollider) return;
            if (controller != null)
            {
                controller.center = new Vector3(0f, 0.93f, 0f);
                controller.height = 1.8f;
            }
        }
        public void ToggleAttackStance()
        {
            attackStance = !attackStance;
            animator.SetBool(animIDattackstance, attackStance);
        }
        public void Sheathe()
        {
            animator.SetTrigger(animIDsheathe);
        }
    }
}