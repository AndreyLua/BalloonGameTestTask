using Leopotam.Ecs;

public class RestartGroundSystem : IEcsRunSystem
{
    private LevelConfig _levelConfig;
    private EcsFilter<RestartEvent, GroundTag, ModelComponent> _filter;

    public void Run()
    {
        foreach (int i in _filter)
        {
            ref EcsEntity entity = ref _filter.GetEntity(i);
            ref ModelComponent model = ref _filter.Get3(i);

            entity.Del<MoveTaboo>();
            model.Transform.position = _levelConfig.StartGroundPosition;
        }
    }
}
