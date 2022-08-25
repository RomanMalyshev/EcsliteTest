using Components;
using Config;
using Leopotam.EcsLite;

namespace Systems
{
    public class PlayerInitSystem: IEcsInitSystem
    {
        private readonly PlayerConfig _playerConfig;
        
        public PlayerInitSystem(PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var playerEntity = systems.GetWorld().NewEntity();
            
            var playerPool = world.GetPool<PlayerTagComponent>();
            var positionPool = world.GetPool<PositionComponent>();
            var targetPositionPool = world.GetPool<TargetPositionComponent>();
            var speedPool = world.GetPool<SpeedComponent>();
            var movablePool = world.GetPool<MovableComponent>();
            var directionPool = world.GetPool<DirectionComponent>();

            playerPool.Add(playerEntity);
            positionPool.Add(playerEntity);
            targetPositionPool.Add(playerEntity);
            speedPool.Add(playerEntity).Speed = _playerConfig.Speed;
            movablePool.Add(playerEntity).IsMove = false;
            directionPool.Add(playerEntity);
        }
    }
}