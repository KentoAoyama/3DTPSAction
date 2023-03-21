using VContainer;
using VContainer.Unity;
using UnityEngine;

public class InputTimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        //InputProvider�̃C���X�^���X��IInputProvider�̕���DI�R���e�i�ɓo�^
        builder.Register<IInputProvider, InputProvider>(Lifetime.Singleton);
    }
}
