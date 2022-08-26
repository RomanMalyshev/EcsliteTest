using Components;
using Config;
using Leopotam.EcsLite;

namespace Systems
{
    public class DoorInitSystem : IEcsInitSystem
    {
        private readonly DoorsConfig _doorsConfig;

        public DoorInitSystem(DoorsConfig doorsConfig)
        {
            _doorsConfig = doorsConfig;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var doorPool = world.GetPool<DoorTagComponent>();
            var positionPool = world.GetPool<PositionComponent>();
            var targetPosition = world.GetPool<TargetPositionComponent>();
            var speedPool = world.GetPool<SpeedComponent>();
            var idPool = world.GetPool<IdComponent>();
            
            foreach (var door in _doorsConfig.Doors)
            {
                var entity = world.NewEntity();

                doorPool.Add(entity);
                positionPool.Add(entity).Position = door.Position;
                targetPosition.Add(entity).TargetPosition = door.Position + door.OpenOffsetPosition;
                speedPool.Add(entity).Speed = door.MoveSpeed;
                idPool.Add(entity).Id = door.Id;
            }
        }
    }
}