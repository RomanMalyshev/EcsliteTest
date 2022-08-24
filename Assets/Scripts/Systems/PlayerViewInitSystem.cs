using Components;
using Config;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class PlayerViewInitSystem: IEcsInitSystem
    {
        private PlayerViewConfig _playerViewConfig;
        
        public PlayerViewInitSystem(PlayerViewConfig playerViewConfig)
        {
            _playerViewConfig = playerViewConfig;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<PlayerTagComponent>().End();
            
            var transformPool = world.GetPool<TransformComponent>();
            var animationPool = world.GetPool<AnimatorComponent>();
            
            foreach (var entity in filter)
            {
                var player = Object.Instantiate(
                    _playerViewConfig.PlayerPrefab,
                    _playerViewConfig.PlayerPrefab.transform.position,
                    Quaternion.identity);

                transformPool.Add(entity).Transform = player.transform;
                animationPool.Add(entity).Animator = player.GetComponent<Animator>();
            }
        }
    }
}