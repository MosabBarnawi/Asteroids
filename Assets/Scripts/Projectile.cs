using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask;
    private float Speed;
    private float damage = 1;
    private float lifeTime = 3f;
    private float cachedLifeTim;
    public float offsetRecycle = 5f;
    IMove imove;

    #region Uniyu Calls
    private void Start()
    {
        cachedLifeTim = lifeTime;
        imove = GetComponent<IMove>();
    }
    private void Update()
    {
        float moveDistance = Speed * Time.deltaTime;

        transform.Translate(Vector3.forward * moveDistance);

        imove.MoveVelocity(Vector3.zero);
        RecycleBulletAfterTime();
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageableObject = other.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeDamage(damage);
        }
        RecycleImmediately();
    }
    #endregion

    public void SetSpeed(float newSpeed) => Speed = newSpeed;

    private void RecycleBulletAfterTime()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            gameObject.SetActive(false);
            lifeTime = cachedLifeTim;
        }
    }

    private void RecycleImmediately()
    {
        gameObject.SetActive(false);
    }
}
