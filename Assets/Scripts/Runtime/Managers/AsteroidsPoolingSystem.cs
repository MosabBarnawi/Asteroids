using System.Collections.Generic;
using UnityEngine;

public class AsteroidsPoolingSystem : PoolingClass<AsteroidsPoolingSystem>
{
    [SerializeField] List<PoolSettings> ItemsToPool = new List<PoolSettings>();

    private void Start() => CreatePool(ItemsToPool, true);

    // TODO:: COMBINE METHODS

    /// <summary>
    /// Used for Spawning Large Astroids
    /// </summary>
    /// <param name="asteroidSize"></param>
    public void SpawnAstroid(AsteroidsSize asteroidSize)
    {
        GameObject obj = GetItemFromPool(ItemsToPool[(int)asteroidSize]);

        if(obj == null)
        {
            Debug.LogWarning("Can't Get Asteroid");
            return;
        }

        obj.transform.position = GameManager.Instance.GetRandomPositionOffScreen();
        obj.SetActive(true);

        obj.GetComponent<Asteroid>().SetOffScreenOffSet(GameManager.Instance.OffScreenOffset);
    }

    /// <summary>
    /// Spawn Astroid
    /// </summary>
    /// <param name="asteroidSize">Enum</param>
    /// <param name="position"></param>
    public void SpawnAstroid(AsteroidsSize asteroidSize, Vector3 position)
    {
        GameObject obj = GetItemFromPool(ItemsToPool[(int)asteroidSize]);

        obj.transform.position = position;
        obj.SetActive(true);
    }
}
