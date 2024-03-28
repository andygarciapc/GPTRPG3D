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
        protected float lerpspeed = 0.005f; // original : 0.05f
        // Start is called before the first frame update
        protected virtual void Start()
        {
            healthSlider.maxValue = fighter.MaxHealth;
            easeHealthSlider.maxValue = fighter.MaxHealth;
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