using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class Asteroids : DamagableObject
{
#if UNITY_EDITOR
    public AsteroidsScriptable AstroidScriptable => astroidScriptable;

    private void OnValidate()
    {
        if (astroidScriptable != null)
        {
            float health = astroidScriptable.StartingHealth;
            startingHealth = health;
        }
    }
#endif

    [SerializeField] AsteroidsScriptable astroidScriptable;
    private Vector3 movementDirection;
    private int offscreenOffSet;
    private IMove imove;

    #region Unity Calls
    protected override void Awake()
    {
        base.Awake();

        RegisterOnDeathAction(OnDeathAction);
        imove = GetComponent<IMove>();
    }

    private void OnEnable()
    {
        movementDirection = astroidScriptable.GetRandomDirection();
        startingHealth = astroidScriptable.StartingHealth;
        imove.SetVelocitySpeed(astroidScriptable.MoveSpeed);
    }

    private void FixedUpdate() => imove.MoveVelocity(movementDirection, offscreenOffSet);

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.isPlayerInvincible)
        {
            IPlayerTag playerTag = other.GetComponent<IPlayerTag>();
            if (playerTag == null) return;
            else
            {
                playerTag.KillImmedediatly();
            }
        }
    }
    #endregion

    #region IDamagable 
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (Helpers.GetRandomChance())
            AudioManager.Instance.PlaySoundFX(SoundFX.AstroidHit1, true);
        else
            AudioManager.Instance.PlaySoundFX(SoundFX.AstroidHit2, true);
    }
    #endregion

    /// <summary>
    /// Use to Create An offset when Spawning So it Happens off Screen
    /// </summary>
    /// <param name="offset"></param>
    public void SetOffScreenOffSet(int offset) => offscreenOffSet = offset;

    private void OnDeathAction()
    {
        gameObject.SetActive(false);
        AudioManager.Instance.PlaySoundFX(SoundFX.AstroidDie, true);
        astroidScriptable.SpawnChildrenAsteroid(transform.position);
        GameManager.Instance.AddPoints(astroidScriptable.Points);
    }
}
