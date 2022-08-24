﻿using Components;
using Config;
using Leopotam.EcsLite;

namespace Systems
{
    public class PlayerInitSystem: IEcsInitSystem
    {
        
        private PlayerConfig _playerConfig;
        
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
            var speedPool = world.GetPool<SpeedComponent>();

            playerPool.Add(playerEntity);
            positionPool.Add(playerEntity);
            speedPool.Add(playerEntity).Speed = _playerConfig.Speed;
        }
    }
}