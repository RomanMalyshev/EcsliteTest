using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public sealed class DirectionSystem : IEcsRunSystem
    {
        private EcsPool<MovableComponent> _movablePool;
        private EcsPool<TransformComponent> _transformPool;
        private EcsPool<TargetPositionComponent> _targetPositionPool;

        private EcsFilter _directionFilter;
        
        public DirectionSystem(EcsFilter directionFilter)
        {
            _directionFilter = directionFilter;
        }

        public void Run(IEcsSystems systems)
        {
            _movablePool = systems.GetWorld().GetPool<MovableComponent>();
            _transformPool = systems.GetWorld().GetPool<TransformComponent>();
            _targetPositionPool = systems.GetWorld().GetPool<TargetPositionComponent>();

            foreach (var entity in _directionFilter)
            {
                ref var movableComponent = ref _movablePool.Get(entity);
                
                if (movableComponent.IsMove)
                {
                    ref var transformComponent = ref _transformPool.Get(entity);
                    ref var targetPosition = ref _targetPositionPool.Get(entity);
                    
                    var moveDirection =
                        Vector3.Normalize(targetPosition.TargetPosition - transformComponent.Transform.position);
                    
                    transformComponent.Transform.forward = moveDirection;
                }
            }
        }
    }
}