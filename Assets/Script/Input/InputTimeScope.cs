using VContainer;
using VContainer.Unity;
using UnityEngine;

public class InputTimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        //InputProviderのインスタンスをIInputProviderの方でDIコンテナに登録
        builder.Register<IInputProvider, InputProvider>(Lifetime.Singleton);
    }
}
