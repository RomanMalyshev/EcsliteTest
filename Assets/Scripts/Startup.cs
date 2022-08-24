using System;
using Config;
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

    [Inject]
    private void Construct(PlayerConfig playerConfig,PlayerViewConfig playerViewConfig)
    {
        _playerConfig = playerConfig;
        _playerViewConfig = playerViewConfig;
    }

    public void Initialize()
    {
        _world = new EcsWorld();
        _initSystems =  new EcsSystems(_world);
        
        var playerInit = new PlayerInitSystem(_playerConfig);
        var playerViewInit = new PlayerViewInitSystem(_playerViewConfig);
        
        _initSystems.Add(playerInit).Add(playerViewInit).Init();
    }

    public void Tick()
    {
        _runSystems?.Run();
    }

    public void Dispose()
    {
        _runSystems?.Destroy();
        _runSystems?.GetWorld()?.Destroy();
        _runSystems = null;
    }
}