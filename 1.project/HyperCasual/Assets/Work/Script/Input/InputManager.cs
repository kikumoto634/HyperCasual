using System.Collections.Generic;
using UniRx;
using UnityEngine;


public class InputManager : BaseManager
{
    //入力
    public List<InputCommand> _inputCommands = new List<InputCommand>();

    //マウス
    public List<MousePositionCommand> _mousePositionCommand = new List<MousePositionCommand>();
    public List<MouseCommand> _mouseCommand = new List<MouseCommand>();

    public override void InitializeStart(LevelSetting levelSetting)
    {
        base.InitializeStart(levelSetting);
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
        foreach (var command in _mouseCommand)
        {
            command.Push();
        }
    }

    public override void FinalizeStart()
    {
        base.FinalizeStart();
        _inputCommands.Clear();
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

//入力座標
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

//マウスコマンド
public class MouseCommand
{
    private int _code = default;
    public BoolReactiveProperty IsPush { get; set; }

    public MouseCommand(int code)
    {
        _code = code;
        IsPush = new BoolReactiveProperty(false);
    }

    public void Push()
    {
        IsPush.Value = Input.GetMouseButton(_code);
    }
}