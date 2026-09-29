using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PokeStatus
{
    public class HealthBar : MonoBehaviour
    {

        public Slider slider;
        public Gradient gradient;
        public Image fill;

        public void SetMaxHealth (int health)
        {
            slider.maxValue = health;
            slider.value = health;
            fill.color = gradient.Evaluate(1f);
        }
        public void SetHealth(float health)
        {
            slider.value = health;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

    }

}