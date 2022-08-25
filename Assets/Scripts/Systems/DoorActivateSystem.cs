using Components;
using Leopotam.EcsLite;

namespace Systems
{
    public class DoorActivateSystem : IEcsRunSystem
    {
        private EcsPool<ActiveButtonComponent> _activeButtonsPool;
        private EcsPool<MovableComponent> _movablePool;
        
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var doorFilter = world.Filter<DoorTagComponent>().End();
            var activeButtonFiler = world.Filter<ActiveButtonComponent>().End();

            _activeButtonsPool = world.GetPool<ActiveButtonComponent>();
            _movablePool = world.GetPool<MovableComponent>();

            var idPool = world.GetPool<IdComponent>();

            foreach (var activeButton in activeButtonFiler)
            {
                foreach (var door in doorFilter)
                {
                    if (_activeButtonsPool.Get(activeButton).Id == idPool.Get(door).Id)
                    {
                        if (!_movablePool.Has(door))
                        {
                            _movablePool.Add(door);
                        }
                    }
                    else
                    {
                        if (_movablePool.Has(door))
                        {
                            _movablePool.Del(door);
                        }
                    }
                }
            }
        }
    }
}