using Leopotam.Ecs;

public class EnemyMoveTabooSystem : IEcsRunSystem
{
    private EcsFilter<MoveTaboo, EnemyTag> _filter;

    public void Run()
    {
        foreach (int i in _filter)
        {
            ref EnemyTag tag = ref _filter.Get2(i);
            ref EcsEntity entity = ref _filter.GetEntity(i);

            entity.Get<InPool>();
            tag.EnemyBase.ReturnEnemyInPool();
        }
    }
}

