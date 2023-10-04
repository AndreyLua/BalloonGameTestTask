using Leopotam.Ecs;

public class RestartBackgroundSystem : IEcsRunSystem
{
    private BackgroundConfig _backgroundConfig;
    private EcsFilter<RestartEvent, BackgroundTag, RenderedComponent> _filter;

    public void Run()
    {
        foreach (int i in _filter)
        {
            ref EcsEntity entity = ref _filter.GetEntity(i);
            ref RenderedComponent renderer = ref _filter.Get3(i);

            renderer.SpriteRenderer.color = _backgroundConfig.StartBackgroundColor;
            ColorChangeCommand colorCommand = new ColorChangeCommand(_backgroundConfig.FinalBackgroundColor, 50);

            entity.Replace(colorCommand);
        }
    }
}
