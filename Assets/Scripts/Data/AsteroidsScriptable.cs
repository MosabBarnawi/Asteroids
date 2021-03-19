using UnityEngine;

[CreateAssetMenu(fileName = "Astroid Asset", menuName = "Asset/Astroid Asset")]
public class AsteroidsScriptable : ScriptableObject
{
    [SerializeField] AsteroidsSize astroidSize;
    [SerializeField] float moveSpeed;
    float startingHealth = 10f;

    [Space(20)]
    [SerializeField] int valueInPoints;
    [SerializeField] int numberOfChildrenToSpawn = Constants.NUMBER_TO_SPAWN;

    #region Properties
    public float StartingHealth => startingHealth;
    public float MoveSpeed => moveSpeed;
    public int Points => valueInPoints;

    #endregion


    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"> postion to Spawn from</param>
    public void SpawnChildrenAsteroid(Vector3 position)
    {
        if (astroidSize == AsteroidsSize.Large)
        {
            for (int i = 0; i < numberOfChildrenToSpawn; i++)
            {
                AsteroidsPoolingSystem.Instance.SpawnAstroid(AsteroidsSize.Medium, position);
            }
        }
        else if (astroidSize == AsteroidsSize.Medium)
        {
            for (int i = 0; i < numberOfChildrenToSpawn; i++)
            {
                AsteroidsPoolingSystem.Instance.SpawnAstroid(AsteroidsSize.Small, position);
            }
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// Return a random Vector3 Direction wih Randomasation
    /// </summary>
    /// <returns></returns>
    public Vector3 GetRandomDirection()
    {
        Vector3 randomDirection = new Vector3(
            Helpers.GetRandomChance() ? -1 : 1,
            Helpers.GetRandomChance() ? -1 : 1,
            Helpers.GetRandomChance() ? -1 : 1);

        return randomDirection;
    }
}
