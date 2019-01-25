using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    //Maybe déplacé ailleur
    public enum Team { Players, Enemies}

    public Team currentTeam;
    public bool IsInvincible { get; set; }

    [SerializeField] int maxHealth = 10;

    public HealthEvent OnDamageEvent { get; private set; }
    public UnityEvent OnDeathEvent { get; private set; }
    public UnityEvent OnNegatedDamageEvent { get; private set; }

    int currentHealth;

    Coroutine invincibleCoroutine;


    void Awake()
    {
        OnDamageEvent = new HealthEvent();
        OnDeathEvent = new UnityEvent();
        OnNegatedDamageEvent = new UnityEvent();
        currentHealth = maxHealth;
    }

    public void SetHealth(int health)
    {
        maxHealth = health;
        currentHealth = maxHealth;
    }

    public bool IsFriendlyFire(DamageData damageData)
    {
        return currentTeam == damageData.team;
    }

    public void TakeDamage(DamageData damageData)
    {
        if (IsInvincible)
        {
            OnNegatedDamageEvent.Invoke();
            return;
        }

        currentHealth -= damageData.damage;

        if(currentHealth <= 0)
        {
            OnDeathEvent.Invoke();
        }
        else
        {
            OnDamageEvent.Invoke(currentHealth, maxHealth);
        }
    }

    public void SetInvincible(float secInvincible, System.Action endInvincibleCallback = null)
    {
        if (invincibleCoroutine != null)
            StopCoroutine(invincibleCoroutine);

        invincibleCoroutine = StartCoroutine(DelayInvincible(secInvincible, endInvincibleCallback));
    }
    IEnumerator DelayInvincible(float secInvincible, System.Action endInvincibleCallback = null)
    {
        IsInvincible = true;
        yield return new WaitForSeconds(secInvincible);
        IsInvincible = false;
        if (endInvincibleCallback != null)
            endInvincibleCallback.Invoke();
    }

    public class HealthEvent : UnityEvent<int, int>{}
}
