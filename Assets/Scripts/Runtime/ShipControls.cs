using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Movement))]
public class ShipControls : DamagableObject, IPlayerTag
{
    [SerializeField] private Transform bulletMozzle;
    [SerializeField] float bulletSpeed;
    [SerializeField] private BulletType bulletType;


    private float invinsibiliylength = 3f;
    private bool isPlayerInvincible;

    Vector2 playerInput;
    private IMove imove;


    #region Unity Calls
    private void Start()
    {
        imove = GetComponent<IMove>();

        if (imove == null) Debug.LogError("Movement System Not Assigend");

        RegisterOnDeathAction(OnDeath);
        GameManager.Instance.RegisterPlayerAndOnSpawnAction(Respawn, gameObject);
    }

    private void Respawn()
    {
        IsDead = false;
        AudioManager.Instance.PlayOnce(SoundFX.Respawn);
        StartCoroutine(CR_InvinsibilityTime());
        // TODO:: BLINK
    }

    private void Update()
    {
        if (IsDead || GameManager.Instance.IsPaused) return;

        playerInput.x = Input.GetAxis(Constants.HORIZONTAL);
        playerInput.y = Input.GetAxis(Constants.VERTICAL);

        bool isInMotion = (playerInput.x != 0 || playerInput.y != 0) ? true : false;
        AudioManager.Instance.PlayThrustSoundFX(isInMotion);

        imove.MoveVelocity(playerInput);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown(Constants.SHOOT_KEY))
        {
            BulletPoolingSystem.Instance.Shoot(bulletMozzle, bulletSpeed, bulletType);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {

        }
        else
        {
            transform.rotation = AimMouse();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlayerInvincible)
        {
            IAsteroidTag asestroidTag = other.GetComponent<IAsteroidTag>();

            if (asestroidTag == null) return;
            else
            {
                KillImmedediatly();
            }
        }
    }

    private void OnDeath() => GameManager.Instance.PlayerDied();

    private IEnumerator CR_InvinsibilityTime()
    {
        isPlayerInvincible = true;
        yield return new WaitForSecondsRealtime(invinsibiliylength);
        isPlayerInvincible = false;
    }

    private Quaternion AimMouse()
    {
        Vector2 positionOnScreen = GameManager.Instance.MainCamera.WorldToViewportPoint(transform.position);

        Vector2 mouseOnScreen = (Vector2)GameManager.Instance.MainCamera.ScreenToViewportPoint(Input.mousePosition);

        float angle = Helpers.AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        return Quaternion.Euler(new Vector3(0f, -angle, 0f));
    }
    #endregion
}
