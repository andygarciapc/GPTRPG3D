using UnityEngine;
using UnityEngine.UI;
using Basic;

namespace BasicUI {

    public class PlayerUIController : BasicUIController
    {
        [SerializeField] private Slider staminaSlider;
        [SerializeField] protected Slider easeStaminaSlider;
        [SerializeField] private Slider expSlider;
        [SerializeField] protected Slider easeExpSlider;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            staminaSlider.maxValue = fighter.maxStamina;
            easeStaminaSlider.maxValue = fighter.maxStamina;
            expSlider.maxValue = fighter.expToNextLevel;
            easeExpSlider.maxValue = fighter.expToNextLevel;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            if (staminaSlider.value != fighter.CurrentStamina)
            {
                staminaSlider.value = fighter.CurrentStamina;
            }
            if (staminaSlider.value != easeStaminaSlider.value)
            {
                easeStaminaSlider.value = Mathf.Lerp(easeStaminaSlider.value, fighter.CurrentStamina, lerpspeed);
            }
            if(expSlider.value != fighter.currentExp)
            {
                expSlider.value = fighter.currentExp;
            }
            if(expSlider.value != easeExpSlider.value)
            {
                easeExpSlider.value = Mathf.Lerp(easeExpSlider.value, fighter.currentExp, lerpspeed);
            }
            if(expSlider.maxValue != fighter.expToNextLevel)
            {
                expSlider.maxValue = fighter.expToNextLevel;
                easeExpSlider.maxValue = fighter.expToNextLevel;
            }
        }
    }
}
