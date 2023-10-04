using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private EnemiesConfig _enemiesConfig;

    public EnemyT SpawnEnemy<EnemyT>(EnemyType type) where EnemyT : EnemyBase
    {
        EnemyBase enemyPrefab = _enemiesConfig.EnemyBaseInTypePairs[type];

        EnemyBase enemyBase = UnityEngine.Object.Instantiate(enemyPrefab,
            Vector2.zero, Quaternion.identity);
        return (EnemyT)enemyBase;
    }
}