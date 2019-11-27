using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PUNTutorial
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Image healthBarImage;
        
        public void SetHealthBarValue(float value)
        {
            healthBarImage.fillAmount = value;
            healthBarImage.color = Color.Lerp(Color.red, Color.green, value);
        }
    }
}
