using Components;
using Leopotam.EcsLite;

namespace Systems
{
    public class MoveSystem:IEcsRunSystem
    {
        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<TransformComponent> _transformPool;
        
        private readonly EcsFilter _filterMove;

        public MoveSystem(EcsFilter filterMove)
        {
            _filterMove = filterMove;
        }

        public void Run(IEcsSystems systems)
        {
            _positionPool = systems.GetWorld().GetPool<PositionComponent>();
            _transformPool = systems.GetWorld().GetPool<TransformComponent>();

            foreach (var entity in _filterMove)
            {
                ref var positionComponent = ref _positionPool.Get(entity);
                ref var transformComponent = ref _transformPool.Get(entity);

                transformComponent.Transform.position = positionComponent.Position;
            }
        }
    }
}