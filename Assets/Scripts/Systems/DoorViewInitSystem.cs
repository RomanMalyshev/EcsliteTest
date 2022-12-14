using Components;
using Leopotam.EcsLite;
using Level_Data;

namespace Systems
{
    public class DoorViewInitSystem: IEcsInitSystem
    {
        private readonly DoorViewData[] _doorsView;
        
        public DoorViewInitSystem(DoorViewData[] doorsView)
        {
            _doorsView = doorsView;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<DoorTagComponent>().End();
            
            var transformPool = world.GetPool<TransformComponent>();
            var idPool = world.GetPool<IdComponent>();
            var positionPool =world.GetPool<PositionComponent>();

            foreach (var entity in filter)
            {
                foreach (var doorView in _doorsView)
                {
                    if (idPool.Get(entity).Id != doorView.Id) continue;
                    doorView.Transform.position = positionPool.Get(entity).Position;
                    transformPool.Add(entity).Transform = doorView.Transform;
                }
            }
        }
    }
}