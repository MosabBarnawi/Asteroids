using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public abstract class DamagableObject : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    [SerializeField, Min(0f)] protected float startingHealth;
    public bool IsDead { get; protected set; }

    private float health;
    private Action OnDeath;

    #region Unity Calls
    protected virtual void Awake()
    {
        if (startingHealth == 0) startingHealth = 10f;
        health = startingHealth;
    }
    #endregion

    #region IDamagble
    public virtual void TakeDamage(float damage)
    {
        if (health <= 0) Death();
        else
            health -= damage;
    }

    public void KillImmedediatly() => Death();

    #endregion

    /// <summary>
    /// Action That Will Run When Death Method is Invoked in Damagble Object Class
    /// </summary>
    /// <param name="OnDeath">Action</param>
    protected void RegisterOnDeathAction(Action OnDeath) => this.OnDeath += OnDeath;

    #region Private Methods
    private void Death()
    {
        IsDead = true;
        OnDeath?.Invoke();
        //gameObject.SetActive(false);
    }
    #endregion
}
