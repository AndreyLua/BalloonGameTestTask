using DG.Tweening;
using Leopotam.Ecs;
using System;
using UnityEngine;

public class SpawnEnemiesSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private LevelConfig _levelConfig;
    private EnemyFactory _enemyFactory;

    private EcsFilter<Paused, PlayerTag> _pauseFilter;
    private EcsFilter<RestartEvent, PlayerTag> _restartFilter;

    private Pool<EnemyBase, EnemyType> _enemiesPool;
    private Sequence _spawnSequence;


    public void Init()
    {
        _enemiesPool = new Pool<EnemyBase, EnemyType>(_enemyFactory.SpawnEnemy<EnemyBase>);
        StartSpawnEnemies();
    }

    private void StartSpawnEnemies()
    {
        _spawnSequence = DOTween.Sequence().AppendInterval(_levelConfig.TimeToSpawnEnemy).AppendCallback(() =>
        {
            EnemyBase enemy = _enemiesPool.ExtractElement(EnemyType.Common);

            LineType lineType = GetRandomLineType();

            if (enemy.Entity.IsNull())
                SpawnEntity(enemy, lineType);
            else
                InitEntityInPool(enemy, lineType);
            
            StartSpawnEnemies();
        });
    }

    private void SpawnEntity(EnemyBase enemy, LineType lineType)
    {
        EcsEntity enemyEntity = _ecsWorld.NewEntity();
        InitNewEnemyEntity(ref enemyEntity, enemy, lineType);
        enemy.Init(enemyEntity);
    }

    private void InitEntityInPool(EnemyBase enemy, LineType lineType)
    {
        enemy.gameObject.SetActive(true);
        ref ModelComponent model = ref enemy.Entity.Get<ModelComponent>();

        model.Transform.position = GetStartPosition(lineType);

        enemy.Entity.Del<InPool>();
        enemy.Entity.Del<MoveTaboo>();
    }

    private void InitNewEnemyEntity(ref EcsEntity entity, EnemyBase enemy, LineType lineType)
    {
        ModelComponent model = new ModelComponent(enemy.transform);
        LineComponent line = new LineComponent(lineType);
        MoveableComponent moveable = new MoveableComponent(Vector2.down, _levelConfig.Speed);
        EnemyTag tag = new EnemyTag(enemy);

        model.Transform.position = GetStartPosition(line.LineType);

        entity.Replace(model)
               .Replace(moveable)
               .Replace(tag);
    }


    private void StopSpawnEnemies()
    {
        _spawnSequence.Kill();
    }

    private LineType GetRandomLineType()
    {
        Array values = Enum.GetValues(typeof(LineType));
        LineType randomLine = (LineType)values.GetValue(UnityEngine.Random.Range(0, values.Length));

        return randomLine;
    }

    private Vector2 GetStartPosition(LineType lineType)
    {
        return new Vector2(_levelConfig.LineXPositionInTypePair[lineType], 6);
    }

    public void Run()
    {
        if (_spawnSequence.active)
        {
            foreach (int i in _pauseFilter)
            {
                StopSpawnEnemies();
                break;
            }
        }
        else
        {
            foreach (int i in _restartFilter)
            {
                StartSpawnEnemies();
                break;
            }
        }
    }
}