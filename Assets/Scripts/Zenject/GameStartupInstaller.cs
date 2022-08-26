using Interfaces;
using Level_Data;

namespace Zenject
{
    public class GameStartupInstaller: MonoInstaller
    {
        public DoorViewData[] DoorsViewData;
        public ButtonViewData[] ButtonsViewData;

        public override void InstallBindings()
        {
            Container.Bind<IGameTime>().To<TimeData>().FromNew().AsSingle().NonLazy();
            Container.Bind<DoorViewData[]>().FromInstance(DoorsViewData).AsSingle().NonLazy();
            Container.Bind<ButtonViewData[]>().FromInstance(ButtonsViewData).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Startup>().AsSingle().NonLazy();
        }
    }
}