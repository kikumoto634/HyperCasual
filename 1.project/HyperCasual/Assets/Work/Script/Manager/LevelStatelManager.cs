using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;

public class LevelStatelManager : MonoBehaviour
{
    private bool _gameLoop = false;

    public InputManager _inputManager = default;

    //�A�N�^�[
    public CanonActor _canonActor = default;

    public void LevelStart()
    {
        _gameLoop = true;

        _inputManager = new InputManager();
        _inputManager.InitializeStart();

        //�A�N�^�[
        _canonActor.InitializeStart();
        _inputManager._mousePositionCommand.Add(_canonActor._mouse);
    }

    public void LevelRun()
    {
        _inputManager.SubscribeStart();

        //�A�N�^�[
        _canonActor.SubscribeStart();

        this.UpdateAsObservable()
            .Where(_ => _gameLoop)
            .Subscribe(_ =>
            {
                //�A�N�^�[
                _inputManager.UpdateProcess();

            })
            .AddTo(this);
    }

    public void LevelFinalize()
    {
        //�A�N�^�[

    }

    //�u�`�؂�p
    private void OnDisable()
    {
        _gameLoop = false;
    }
}
