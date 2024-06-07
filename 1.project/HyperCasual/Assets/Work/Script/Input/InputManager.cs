
using Mono.Cecil.Cil;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering;

public class InputManager : BaseManager
{
    //入力
    public List<InputCommand> _inputCommands = new List<InputCommand>();

    //マウス
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

        //入力
        foreach(var command in _inputCommands)
        {
            command.Push();
        }

        //マウス
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


//入力コマンド
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

//入力コマンド
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