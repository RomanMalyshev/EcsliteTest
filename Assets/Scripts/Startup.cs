using System;
using Components;
using Config;
using Interfaces;
using Leopotam.EcsLite;
using Systems;
using Zenject;

public class Startup: IInitializable, ITickable, IDisposable
{
    private EcsSystems _initSystems;
    private EcsSystems _runSystems;
    
    private EcsWorld _world;
    
    private PlayerConfig _playerConfig;
    private PlayerViewConfig _playerViewConfig;
    private GameConfig _gameConfig;
    private DoorsConfig _doorsConfig;
    private ButtonsConfig _buttonsConfig;
    
    private IGameTime _gameTime;
    
    [Inject]
    private void Construct(
        PlayerConfig playerConfig,
        PlayerViewConfig playerViewConfig,
        GameConfig gameConfig,
        IGameTime gameTime,
        DoorsConfig doorsConfig,
        ButtonsConfig buttonsConfig)
    {
        _playerConfig = playerConfig;
        _playerViewConfig = playerViewConfig;
        _gameConfig = gameConfig;
        _gameTime = gameTime;
        _doorsConfig = doorsConfig;
        _buttonsConfig = buttonsConfig;
    }

    public void Initialize()
    {
        _world = new EcsWorld();
        _initSystems =  new EcsSystems(_world);
        
        var playerInit = new PlayerInitSystem(_playerConfig);
        var playerViewInit = new PlayerViewInitSystem(_playerViewConfig);

        var doorInit = new DoorInitSystem(_doorsConfig);
        var buttonInit = new ButtonInitSystem(_buttonsConfig);
        
        _initSystems.
            Add(playerInit).
            Add(playerViewInit).
            Add(doorInit).
            Add(buttonInit).
            Init();
        
        _runSystems =  new EcsSystems(_world);
        
        var filterInput = _world.Filter<PlayerTagComponent>().Inc<TargetPositionComponent>().End();
        var inputSystem = new InputSystem(filterInput);
        
        var filterPositionCalculation =_world.
            Filter<MovableComponent>().
            Inc<TargetPositionComponent>().
            Inc<PositionComponent>().
            Inc<SpeedComponent>().
            End();
        var positionCalculationSystem = new PositionCalculationSystem(_gameConfig,_gameTime,filterPositionCalculation);
        
        
        var filterMove = _world.
            Filter<TransformComponent>().
            Inc<PositionComponent>().
            End();

        var moveSystem = new MoveSystem(filterMove);
        
        var filterMoveAnimation = _world.Filter<AnimatorComponent>().Inc<MovableComponent>().End();
        var moveAnimationSystem = new MoveAnimationSystem(filterMoveAnimation);
        
        var directionFilter = _world.Filter<MovableComponent>().
            Inc<DirectionComponent>().
            Inc<TransformComponent>().
            Inc<TargetPositionComponent>().
            End();

        var directionSystem = new DirectionSystem(directionFilter);


        var buttonActivateSystem = new ButtonActivateSystem(_gameConfig);
        var doorActivateSystem = new DoorActivateSystem();
        _runSystems.
            Add(inputSystem).
            Add(positionCalculationSystem).
            Add(moveSystem).
            Add(moveAnimationSystem).
            Add(directionSystem).
            Add(buttonActivateSystem).
            Add(doorActivateSystem).
            Init();
    }

    public void Tick()
    {
        _runSystems?.Run();
    }

    public void Dispose()
    {
        _initSystems?.Destroy();
        _initSystems?.GetWorld()?.Destroy();
        _initSystems = null;
        
        _runSystems?.Destroy();
        _runSystems?.GetWorld()?.Destroy();
        _runSystems = null;
    }
}