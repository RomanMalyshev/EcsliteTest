using Interfaces;

namespace Zenject
{
    public class GameStartupInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameTime>().To<TimeData>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Startup>().AsSingle().NonLazy();
        }
    }
}