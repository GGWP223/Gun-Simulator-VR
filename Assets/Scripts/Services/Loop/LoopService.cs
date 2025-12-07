using System;
using UniRx;
using Zenject;

namespace Services.Loop
{
    public class LoopService : ILoopService, IInitializable, IDisposable
    {
        private readonly IEveryUpdate[] _updates;
        private readonly IEveryFixedUpdate[] _fixedUpdates;
        private readonly IEveryLateUpdate[] _lateUpdates;
        
        private readonly CompositeDisposable _disposables = new();

        public LoopService
        (
            IEveryUpdate[] updates,
            IEveryFixedUpdate[] fixedUpdates,
            IEveryLateUpdate[] lateUpdates
        )
        {
            _updates = updates;
            _fixedUpdates = fixedUpdates;
            _lateUpdates = lateUpdates;
        }

        public void Initialize()
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    foreach (var tick in _updates)
                        tick.Update();
                })
                .AddTo(_disposables);
            
            Observable
                .EveryFixedUpdate()
                .Subscribe(_ =>
                {
                    foreach (var tick in _fixedUpdates)
                        tick.FixedUpdate();
                })
                .AddTo(_disposables);
            
            Observable
                .EveryLateUpdate()
                .Subscribe(_ =>
                {
                    foreach (var tick in _lateUpdates)
                        tick.LateUpdate();
                })
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}