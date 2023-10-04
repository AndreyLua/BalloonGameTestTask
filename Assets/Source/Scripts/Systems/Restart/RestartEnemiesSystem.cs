﻿using Leopotam.Ecs;

public class RestartEnemiesSystem : IEcsRunSystem
{
    private EcsFilter<RestartEvent, EnemyTag> _filter;

    public void Run()
    {
        foreach (int i in _filter)
        {
            ref EcsEntity entity = ref _filter.GetEntity(i);
            ref EnemyTag enemyTag = ref _filter.Get2(i);
            enemyTag.EnemyBase.ReturnEnemyInPool();
            entity.Get<InPool>();
            entity.Get<MoveTaboo>();
        }
    }
}
