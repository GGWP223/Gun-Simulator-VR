using UnityEngine;
using View;
using Zenject;

namespace Installers
{
    public class ViewInstallers : MonoInstaller
    {
        public override void InstallBindings()
        {
            Bind<CameraView>();
            Bind<HandsView>();
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