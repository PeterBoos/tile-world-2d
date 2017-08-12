using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character {
    
    string name;
    float maxHealth;
    float currentHealth;
    bool isDead;

    #region Properties
    public string Name
    {
        get { return name; }
        private set { name = value; }
    }

    public float Health
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }


    #endregion

    public Character(string name, float maxHealth)
    {
        this.name = name;
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        IsDead = false;
    }

    public void DoDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            IsDead = true;
        }
    }

    public void DoHeal(float amount)
    {
        currentHealth += amount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
