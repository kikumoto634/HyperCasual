using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;

public class LevelStatelManager : MonoBehaviour
{
    private bool _gameLoop = false;

    public InputManager _inputManager = default;

    //アクター
    public CanonActor _canonActor = default;

    public void LevelStart()
    {
        _gameLoop = true;

        _inputManager = new InputManager();
        _inputManager.InitializeStart();

        //アクター
        _canonActor.InitializeStart();
        _inputManager._mousePositionCommand.Add(_canonActor._mouse);
    }

    public void LevelRun()
    {
        _inputManager.SubscribeStart();

        //アクター
        _canonActor.SubscribeStart();

        this.UpdateAsObservable()
            .Where(_ => _gameLoop)
            .Subscribe(_ =>
            {
                //アクター
                _inputManager.UpdateProcess();

            })
            .AddTo(this);
    }

    public void LevelFinalize()
    {
        //アクター

    }

    //ブチ切り用
    private void OnDisable()
    {
        _gameLoop = false;
    }
}
