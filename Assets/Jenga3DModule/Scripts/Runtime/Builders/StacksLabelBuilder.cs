using System;
using Zenject;

public class StacksLabelBuilder : IInitializable, IDisposable
{
    private readonly SignalBus _signalBus;
    private readonly StackLabelFactory _stackLabelFactory;
    private readonly StacksLabelBuilderInstaller.Settings _builderSettings;

    public StacksLabelBuilder(
        SignalBus signalBus,
        StackLabelFactory stackLabelFactory,
        StacksLabelBuilderInstaller.Settings builderSettings)
    {
        _signalBus = signalBus;
        _stackLabelFactory = stackLabelFactory;
        _builderSettings = builderSettings;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<StackCreatedSignal>(OnStackCreatedSignal);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<StackCreatedSignal>(OnStackCreatedSignal);
    }

    private void OnStackCreatedSignal(StackCreatedSignal signal)
    {
        BuildLabelForStack(signal.CreatedStack);
    }

    private void BuildLabelForStack(Stack stack)
    {

        StackLabelCreationConfig stackLabelCreationConfig = new()
        {
            Stack = stack,
            Position = stack.transform.position + _builderSettings.StackLabelOffset,
            ParentTransform = stack.transform
        };

        _stackLabelFactory.Create(stackLabelCreationConfig);
    }
}