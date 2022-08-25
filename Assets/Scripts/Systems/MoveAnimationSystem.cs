using Components;
using Leopotam.EcsLite;

public class MoveAnimationSystem : IEcsRunSystem
{
    private EcsPool<AnimatorComponent> _animatorPool;
    private EcsPool<MovableComponent> _movablePool;
    
    private readonly EcsFilter _filterMoveAnimation;

    public MoveAnimationSystem(EcsFilter filterMoveAnimation)
    {
        _filterMoveAnimation = filterMoveAnimation;
    }
    
    public void Run(IEcsSystems systems)
    {
        _animatorPool = systems.GetWorld().GetPool<AnimatorComponent>();
        _movablePool =  systems.GetWorld().GetPool<MovableComponent>();
            
        foreach (var entity in _filterMoveAnimation)
        {
            ref var animatorComponent = ref _animatorPool.Get(entity);
            ref var movableComponent = ref _movablePool.Get(entity);

            animatorComponent.Animator.SetBool("Run",movableComponent.IsMove);
        }
    }
}
