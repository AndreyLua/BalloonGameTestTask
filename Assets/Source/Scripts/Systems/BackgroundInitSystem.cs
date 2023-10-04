using Leopotam.Ecs;
using UnityEngine;

public class BackgroundInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private BackgroundConfig _backgroundConfig;

    public void Init()
    {
        EcsEntity backgroundEntity = _ecsWorld.NewEntity();

        Background background = Object.Instantiate(_backgroundConfig.BackgroundPrefab, _backgroundConfig.StartBackgroundPosition, Quaternion.identity);

        ModelComponent model = new ModelComponent(background.transform);
        RenderedComponent rendered = new RenderedComponent(background.SpriteRenderer);
        ColorChangeCommand colorCommand = new ColorChangeCommand(_backgroundConfig.FinalBackgroundColor, 50);

        backgroundEntity.Get<BackgroundTag>();
        backgroundEntity.Replace(model).Replace(colorCommand).Replace(rendered);
    }
}
