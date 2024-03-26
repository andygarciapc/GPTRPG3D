using UnityEngine;
using UnityEngine.UI;
using Basic;

namespace BasicUI {

    public class PlayerUIController : BasicUIController
    {
        [SerializeField] private Slider staminaSlider;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            staminaSlider.maxValue = fighter.maxStamina;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}
