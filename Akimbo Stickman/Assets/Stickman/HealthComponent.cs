using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : NetworkBehaviour
{
    public int maxHealth;
    public int health;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetHealth(health, maxHealth);
    }

    public override void OnNetworkSpawn()
    {
        health = maxHealth;
        healthBar.SetHealth(health, maxHealth);
        if (!IsOwner)
        {
            enabled = false;
        }
    }

    /// <summary>
    /// Subtracts health by certain amount
    /// </summary>
    /// <param name="dmg">Amount of damage</param>
    /// <returns>True if health is greater than 0, otherwise returns false.</returns>
    public bool DealDamage(int dmg)
    {
        health -= dmg;
        healthBar.SetHealth(health, maxHealth);

        Debug.Log("HP: " + health);

        if (health <= 0)
        {
            return false;
        }

        return true;
    }
}
