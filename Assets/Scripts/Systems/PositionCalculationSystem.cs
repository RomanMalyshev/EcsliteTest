using Components;
using Interfaces;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class PositionCalculationSystem:IEcsRunSystem
    {
        private readonly GameConfig _gameConfig;
        private readonly IGameTime _gameTime;
        
        private EcsPool<MovableComponent> _movablePool;
        private EcsPool<TargetPositionComponent> _targetPositionPool;
        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<SpeedComponent> _speedPool;
        
        private readonly EcsFilter _filterPositionCalculation;
        
        public PositionCalculationSystem(GameConfig gameConfig, IGameTime gameTime, EcsFilter filterPositionCalculation)
        {
            _gameConfig = gameConfig;
            _gameTime = gameTime;
            _filterPositionCalculation = filterPositionCalculation;
        }

        public void Run(IEcsSystems systems)
        {
            _movablePool = systems.GetWorld().GetPool<MovableComponent>();
            _targetPositionPool = systems.GetWorld().GetPool<TargetPositionComponent>();
            _positionPool = systems.GetWorld().GetPool<PositionComponent>();
            _speedPool = systems.GetWorld().GetPool<SpeedComponent>();
            
            foreach (var entity in _filterPositionCalculation)
            {
                ref var targetPositionComponent = ref _targetPositionPool.Get(entity);
                ref var positionComponent = ref _positionPool.Get(entity);
                ref var movableComponent = ref _movablePool.Get(entity);
                
                var distanceToTargetPosition = Vector3.Distance(positionComponent.Position, targetPositionComponent.TargetPosition);
                
                if (distanceToTargetPosition > _gameConfig.DestinationDistance)
                {
                    ref var speedComponent = ref _speedPool.Get(entity);
                    
                    var moveDirection =
                        Vector3.Normalize(targetPositionComponent.TargetPosition - positionComponent.Position);

                    positionComponent.Position += moveDirection * speedComponent.Speed * _gameTime.DeltaTime;
                    movableComponent.IsMove = true;
                }
                else
                {
                    movableComponent.IsMove = false;
                }
            }
        }  
    }
}