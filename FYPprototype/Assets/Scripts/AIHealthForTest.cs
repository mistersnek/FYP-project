using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealthForTest : MonoBehaviour
{

        [HideInInspector]
    public float currentHealth;

    public float maxHealth;
    public bool gotAttacked;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Current health:" + currentHealth);
        gotAttacked = true;

        //if (currentHealth <= 0.0f) Ragdoll();

    }
    public void IncreaseHealth(float amount)
    {
        currentHealth += amount;
    }
}
