using System;
using System.Linq;
using Zenject;

public class StackFocusController : IInitializable, IDisposable
{
    private readonly SignalBus _signalBus;
    private readonly StacksRegistry _stacksRegistry;

    public Stack CurrentFocusedStack { get; private set; }

    public StackFocusController(
        SignalBus signalBus,
        StacksRegistry stacksRegistry)
    {
        _signalBus = signalBus;
        _stacksRegistry = stacksRegistry;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<AllStacksCreatedSignal>(OnAllStacksCreatedSignal);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<AllStacksCreatedSignal>(OnAllStacksCreatedSignal);
    }

    public void FocusOnStack(Stack stack)
    {
        CurrentFocusedStack = stack;
        _signalBus.Fire(new FocusStackSignal(stack));
    }

    private void OnAllStacksCreatedSignal(AllStacksCreatedSignal signal)
    {
        FocusOnDefaultStack();
    }

    private void FocusOnDefaultStack()
    {
        var stacks = _stacksRegistry.GetAllItems();

        if(stacks.Any())
        {
            FocusOnStack(stacks.First());
        }
    }
}