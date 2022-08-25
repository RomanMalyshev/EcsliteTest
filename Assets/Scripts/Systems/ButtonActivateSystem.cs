using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class ButtonActivateSystem : IEcsRunSystem
    {
        private readonly GameConfig _gameConfig;
        
        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<ActiveButtonComponent> _activateButtonPool;
        private EcsPool<IdComponent> _idPool;
        
        public ButtonActivateSystem(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
        }

        public void Run(IEcsSystems systems)
        {
            var playerFilter = systems.GetWorld().Filter<PlayerTagComponent>().Inc<PositionComponent>().End();
            var buttonFiler = systems.GetWorld().Filter<ButtonTagComponent>().Inc<PositionComponent>().End();
            
            _positionPool = systems.GetWorld().GetPool<PositionComponent>();
            _activateButtonPool = systems.GetWorld().GetPool<ActiveButtonComponent>();
            _idPool = systems.GetWorld().GetPool<IdComponent>();
            
            foreach (var player in playerFilter)
            {
                foreach (var button in buttonFiler)
                {
                    var distanceToButton = Vector3.Distance(
                        _positionPool.Get(player).Position,
                        _positionPool.Get(button).Position);
                    
                    if (distanceToButton < _gameConfig.DestinationDistance)
                    {
                        if (!_activateButtonPool.Has(button))
                        {
                            _activateButtonPool.Add(button).Id = _idPool.Get(button).Id;
                        }
                    }
                    else
                    {
                        if (_activateButtonPool.Has(button))
                        {
                            _activateButtonPool.Del(button);
                        }
                    }
                }
            }
        }
    }
}