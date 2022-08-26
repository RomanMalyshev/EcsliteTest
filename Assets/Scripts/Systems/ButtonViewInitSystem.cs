using Components;
using Leopotam.EcsLite;

namespace Systems
{
    public class ButtonViewInitSystem: IEcsInitSystem
    {
        private readonly ButtonViewData[] _buttonsView;
        
        public ButtonViewInitSystem(ButtonViewData[] buttonsView)
        {
            _buttonsView = buttonsView;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<ButtonTagComponent>().End();
            
            var transformPool = world.GetPool<TransformComponent>();
            var positionPool = world.GetPool<PositionComponent>();
            var idPool = world.GetPool<IdComponent>();

            foreach (var entity in filter)
            {
                foreach (var buttonView in _buttonsView)
                {
                    if (idPool.Get(entity).Id != buttonView.Id) continue;
                    
                    buttonView.Transform.position = positionPool.Get(entity).Position;
                    transformPool.Add(entity).Transform = buttonView.Transform;
                }
            }
        }
    }
}