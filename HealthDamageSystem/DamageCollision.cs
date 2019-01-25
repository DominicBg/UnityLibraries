using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class DamageCollision : MonoBehaviour
{
    protected DamageData damageData;
    [HideInInspector] public HealthComponentEvent OnDealDamageEvent = new HealthComponentEvent();

    public void SetDamageData(DamageData damageData)
    {
        this.damageData = damageData;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Maybe Dictionary<collider,HealthComponent>
        HealthComponent hpComponent = other.GetComponent<HealthComponent>();
        if (hpComponent && !hpComponent.IsFriendlyFire(damageData))
        {
            hpComponent.TakeDamage(damageData);
            OnDamageCollision();
            OnDealDamageEvent.Invoke(hpComponent);
        }
    }

    protected abstract void OnDamageCollision();

    public class HealthComponentEvent : UnityEvent<HealthComponent>{};
}
