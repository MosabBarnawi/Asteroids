using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[DisallowMultipleComponent][RequireComponent(typeof(Movement))]
public class ShipControls : DamagableObject, IPlayerTag
{
    [SerializeField] private Transform bulletMozzle;
    [SerializeField] float bulletSpeed;
    [SerializeField] private BulletType bulletType;

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

    private void OnDeath() => GameManager.Instance.PlayerDied();

    private Quaternion AimMouse()
    {
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        float angle = Helpers.AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        return Quaternion.Euler(new Vector3(0f, -angle, 0f));
    }
    #endregion
}
