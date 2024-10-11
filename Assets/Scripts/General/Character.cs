using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    [Header("受伤无敌时间")]
    public int invincibleTime;
    private float invincibleCounter;
    private bool invincible = false;
    public UnityEvent<Transform> onTakeDamage;
    public UnityEvent onDead;

    public void TakeDamage(Attack attack)
    {
        if (invincible)
        {
            return;
        }
        if (currentHealth > attack.damage)
        {
            currentHealth -= attack.damage;
            TriggerInvincible();
            onTakeDamage?.Invoke(attack.transform);
        } else
        {
            currentHealth = 0;
            onDead?.Invoke();
        }
    }

    private void TriggerInvincible()
    {
        if (!invincible)
        {
            invincible = true;
            invincibleCounter = invincibleTime;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        invincible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincible)
        {
            invincibleCounter -= Time.deltaTime;
            if (invincibleCounter <= 0)
            {
                invincible = false;
            }
        }
    }
}
