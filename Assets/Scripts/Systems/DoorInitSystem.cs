using Components;
using Config;
using Leopotam.EcsLite;

namespace Systems
{
    public class DoorInitSystem: IEcsInitSystem
    {
        private readonly DoorsConfig _doorsConfig;
        
        public DoorInitSystem(DoorsConfig doorsConfig)
        {
            _doorsConfig = doorsConfig;
        }

        public void Init(IEcsSystems systems)
        {
            var world =systems.GetWorld();
            
            foreach (var door in _doorsConfig.Doors)
            {
                var entity = world.NewEntity();
                
                var doorPool = world.GetPool<DoorTagComponent>();
                var positionPool = world.GetPool<PositionComponent>();
                var targetPosition =  world.GetPool<TargetPositionComponent>();
                var speedPool =  world.GetPool<SpeedComponent>();
                
                doorPool.Add(entity);
                positionPool.Add(entity).Position = door.Position;
                targetPosition.Add(entity).TargetPosition = door.Position + door.OpenOffsetPosition;
                speedPool.Add(entity).Speed = door.MoveSpeed;
            }
        }
    }
}