using VContainer;
using VContainer.Unity;
using UnityEngine;

public class InputManager : IStartable
{
    private readonly IInputProvider _input;
    public IInputProvider Input => _input;

    [Inject]
    public InputManager(IInputProvider input)
    {
        _input = input;
    }

    public void Start()
    {
        Debug.Log(_input.GetMoveDir());
    }
}
