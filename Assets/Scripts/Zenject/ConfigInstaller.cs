using Config;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ConfigInstaller", menuName = "Installers/ConfigInstaller")]
public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
{
    public DoorsConfig DoorsConfig;
    public ButtonsConfig ButtonsConfig;
    public PlayerViewConfig PlayerViewConfig;
    public PlayerConfig PlayerConfig;
    public GameConfig GameConfig;
    
    public override void InstallBindings()
    {
        Container.Bind<DoorsConfig>().FromInstance(DoorsConfig).AsSingle().NonLazy();
        Container.Bind<ButtonsConfig>().FromInstance(ButtonsConfig).AsSingle().NonLazy();
        Container.Bind<PlayerViewConfig>().FromInstance(PlayerViewConfig).AsSingle().NonLazy();
        Container.Bind<PlayerConfig>().FromInstance(PlayerConfig).AsSingle().NonLazy();
        Container.Bind<GameConfig>().FromInstance(GameConfig).AsSingle().NonLazy();
    }
}