using Components;
using Config;
using Leopotam.EcsLite;

namespace Systems
{
    public class ButtonInitSystem: IEcsInitSystem
    {
        private readonly ButtonsConfig _buttonsConfig;
        
        public ButtonInitSystem(ButtonsConfig buttonsConfig)
        {
            _buttonsConfig = buttonsConfig;
        }

        public void Init(IEcsSystems systems)
        {
            var world =systems.GetWorld();
            
            foreach (var button in _buttonsConfig.Buttons)
            {
                var entity = world.NewEntity();
                
                var buttonPool = world.GetPool<ButtonTagComponent>();
                var positionPool = world.GetPool<PositionComponent>();
                var idPool = world.GetPool<IdComponent>();

                idPool.Add(entity).Id = button.Id;
                buttonPool.Add(entity);
                positionPool.Add(entity).Position = button.Position;
            }
        }
    }
}