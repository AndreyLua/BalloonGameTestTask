using Leopotam.Ecs;
using UnityEngine;

public class CommonEnemy : EnemyBase
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private EcsEntity _ecsEntity;

    public override EcsEntity Entity => _ecsEntity;

    public override EnemyType Identifier => EnemyType.Common;
    public override SpriteRenderer SpriteRenderer => _spriteRenderer;

    public override void Init(EcsEntity entity)
    {
        _ecsEntity = entity;
    }
}
