using UnityEngine;
using UnityEngine.UI;
using Basic;

namespace BasicUI {

    public class PlayerUIController : BasicUIController
    {
        [SerializeField] private Slider staminaSlider;
        [SerializeField] protected Slider easeStaminaSlider;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            staminaSlider.maxValue = fighter.maxStamina;
            easeStaminaSlider.maxValue = fighter.maxStamina;
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
        }
    }
}
