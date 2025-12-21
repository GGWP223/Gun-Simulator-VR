using DI.View;
using UnityEngine;
using Zenject;

namespace DI.Installers
{
    public class ViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Bind<PlayerView>();
            Bind<PlayerHandView>();
            Bind<PlayerHeadView>();
            Bind<PlayerRiggingView>();
        }
        
        private void Bind<T>() where T : MonoBehaviour
        {
            Container
                .Bind<T>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}