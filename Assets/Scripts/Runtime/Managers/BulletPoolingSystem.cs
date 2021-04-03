using System.Collections.Generic;
using UnityEngine;

public class BulletPoolingSystem : PoolingClass<BulletPoolingSystem>
{
    [SerializeField] List<PoolSettings> ItemsToPool = new List<PoolSettings>();

    void Start()
    {
        CreatePool(ItemsToPool, true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="shotOrigin"></param>
    /// <param name="bulletSpeed"></param>
    /// <param name="bulletType"></param>
    public void Shoot(Transform shotOrigin, float bulletSpeed, BulletType bulletType)
    {
        GameObject bullet = GetItemFromPool(ItemsToPool[(int)bulletType]);

        if (bullet != null)
        {
            bullet.transform.position = shotOrigin.position;
            bullet.transform.rotation = shotOrigin.rotation;

            bullet.SetActive(true);


            Projectile projectile = bullet.GetComponent<Projectile>();
            projectile.SetSpeed(bulletSpeed);

            AudioManager.Instance.PlaySoundFX(SoundFX.Shooting, true);
        }
    }
}
