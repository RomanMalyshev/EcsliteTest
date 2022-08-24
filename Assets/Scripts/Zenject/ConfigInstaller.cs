using Config;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ConfigInstaller", menuName = "Installers/ConfigInstaller")]
public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
{
    public PlayerViewConfig PlayerViewConfig;
    public PlayerConfig PlayerConfig;
    
    public override void InstallBindings()
    {
        Container.Bind<PlayerViewConfig>().FromInstance(PlayerViewConfig).AsSingle().NonLazy();
        Container.Bind<PlayerConfig>().FromInstance(PlayerConfig).AsSingle().NonLazy();
    }
}