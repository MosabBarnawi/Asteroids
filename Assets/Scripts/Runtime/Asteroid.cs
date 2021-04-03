using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

[DisallowMultipleComponent]
[RequireComponent(typeof(Movement))]
public class Asteroid : DamagableObject, IAsteroidTag
{
#if UNITY_EDITOR
    public AsteroidsScriptable AstroidScriptable => astoroidScriptable;

    private void OnValidate()
    {
        if (astoroidScriptable != null)
        {
            float health = astoroidScriptable.StartingHealth;
            startingHealth = health;
        }
    }
#endif

    [SerializeField] AsteroidsScriptable astoroidScriptable;
    private Vector3 movementDirection;
    private int offscreenOffSet;
    private IMove imove;

    #region Unity Calls
    protected override void Awake()
    {
        base.Awake();

        RegisterOnDeathAction(OnDeathAction);

        imove = GetComponent<IMove>();
        if (imove == null)
        {
            Debug.LogWarning("No IMove Found adding New One");
            gameObject.AddComponent<Movement>();
        }
    }

    private void OnEnable()
    {
        movementDirection = astoroidScriptable.GetRandomDirection();
        startingHealth = astoroidScriptable.StartingHealth;
        imove.SetVelocitySpeed(astoroidScriptable.MoveSpeed);
    }

    private void FixedUpdate() => imove.MoveVelocity(movementDirection, offscreenOffSet);

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
        astoroidScriptable.SpawnChildrenAsteroid(transform.position);
        GameManager.Instance.AddPoints(astoroidScriptable.Points);
    }
}
