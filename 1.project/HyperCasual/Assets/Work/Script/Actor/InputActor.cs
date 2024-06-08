using UniRx;
using UnityEngine;

/// <summary>
/// //アクター使用例
//_tempActor.InitializeStart();
//_inputManager._inputCommands.Add(_tempActor._input);
/// </summary>

public class InputActor : Actor
{
    public InputCommand _input = default;

    public override void InitializeStart(LevelSetting levelSetting)
    {
        base.InitializeStart();

        _input = (new InputCommand(KeyCode.Space));
    }

    public override void SubscribeStart()
    {
        base.SubscribeStart();

        _input.IsPush
            .Where(flag => flag)
            .Subscribe(_ =>
            {
                Debug.Log("入力");
            })
            .AddTo(this);
    }
}