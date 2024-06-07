
using Mono.Cecil.Cil;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering;

public class InputManager : BaseManager
{
    //����
    public List<InputCommand> _inputCommands = new List<InputCommand>();

    //�}�E�X
    public List<MousePositionCommand> _mousePositionCommand = new List<MousePositionCommand>();

    public override void InitializeStart()
    {
        base.InitializeStart();
    }

    public override void SubscribeStart()
    {
        base.SubscribeStart();
    }

    public override void UpdateProcess()
    {
        base.UpdateProcess();

        //����
        foreach(var command in _inputCommands)
        {
            command.Push();
        }

        //�}�E�X
        foreach (var command in _mousePositionCommand)
        {
            command.MouawPointer();
        }
    }

    public override void FinalizeStart()
    {
        base.FinalizeStart();
    }
}


//���̓R�}���h
public class InputCommand
{
    private KeyCode _code = default;
    public BoolReactiveProperty IsPush { get; set; }

    public InputCommand(KeyCode code)
    {
        _code = code;
        IsPush = new BoolReactiveProperty(false);
    }

    public void Push()
    {
        IsPush.Value = Input.GetKey(_code);
    }
}

//���̓R�}���h
public class MousePositionCommand
{
    public ReactiveProperty<Vector2> _mousePosition = new ReactiveProperty<Vector2>();

    public MousePositionCommand()
    {

    }

    public void MouawPointer()
    {
        _mousePosition.Value = Input.mousePosition;
    }
}