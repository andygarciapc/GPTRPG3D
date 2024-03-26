using UnityEngine;
using UnityEngine.UI;
using Basic;

namespace BasicUI
{

    public class BasicUIController : MonoBehaviour
    {
        [SerializeField] protected Slider healthSlider;
        [SerializeField] protected Slider easeHealthSlider;
        [SerializeField] protected BasicFighter fighter;
        private float lerpspeed = 0.05f;
        // Start is called before the first frame update
        protected virtual void Start()
        {
            healthSlider.maxValue = fighter.maxHealth;
            easeHealthSlider.maxValue = fighter.maxHealth;
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (healthSlider.value != fighter.CurrentHealth)
            {
                healthSlider.value = fighter.CurrentHealth;
            }
            if (healthSlider.value != easeHealthSlider.value)
            {
                easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, fighter.CurrentHealth, lerpspeed);
            }
        }
    }
}