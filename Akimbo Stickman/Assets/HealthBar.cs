using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;

    public void SetHealth(int health, int devider = 100)
    {
        healthBar.fillAmount = health / (float)devider;
    }
}
