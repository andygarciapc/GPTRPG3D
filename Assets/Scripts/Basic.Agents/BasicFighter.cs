using UnityEngine;


namespace Basic.Agents
{
    public class BasicFighter : FighterAttributes
    {
        //private
        private Animator animator;
        private int animIDroll, animIDsheathe, animIDattackstance;
        protected bool attackStance;
        private float attackStartTime = 0f;
        private float inputWindowStart = 0.5f;
        private float inputWindowEnd = 2.0f;

        //public
        public WeaponCollisionController weaponCollisionHandler;
        public float cooldownTime = 1.5f;
        public static int noOfClicks = 0;
        public float dashSpeed = 10f;
        public float dashTime = 1f;
        public Transform characterHand;
        public Transform weaponHolster;
        public Transform weapon;

        public float DashSpeed
        {
            set
            {
                dashSpeed = value;
            }
        }

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
            set { attackStance = value; }
        }

        protected override void Start()
        {
            base.Start();
            attackStance = false;
            animator = GetComponent<Animator>();
            AssignAnimationIDs();
        }

        private void AssignAnimationIDs()
        {
            animIDroll = Animator.StringToHash("Roll");
            animIDsheathe = Animator.StringToHash("Sheathe");
            animIDattackstance = Animator.StringToHash("AttackStance");
        }

        protected override void Update()
        {
            base.Update();
            UpdateWeaponPosition();
            if (IsPlayingAttackAnimation()) weaponCollisionHandler.EnableCollider();
            else weaponCollisionHandler.DisableCollider();

        }
        private void UpdateWeaponPosition()
        {
            if (attackStance)
            {
                weapon.position = characterHand.position;
                weapon.rotation = characterHand.rotation;
            }
            else
            {
                weapon.position = weaponHolster.position;
                weapon.rotation = weaponHolster.rotation;
            }
        }
        public void DoAttack()
        {
            // Check if an attack animation is already playing
            if (IsPlayingAttackAnimation())
            {
                weaponCollisionHandler.EnableCollider();
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

        protected bool IsPlayingAttackAnimation()
        {
            // Check if any of the attack animation states are currently playing
            return animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
        }


        virtual public void DoRoll(Vector3 rollDirection)
        {
            animator.SetTrigger(animIDroll);
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