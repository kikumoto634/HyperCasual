using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

//SOLID 参考 https://annulusgames.com/blog/solid-principles/
//Collision 参考　https://naoyu.dev/%E3%80%90unity%E3%80%91%E3%82%AF%E3%83%A9%E3%82%B9%E8%A8%AD%E8%A8%88%E3%81%AB%E3%81%8A%E3%81%91%E3%82%8B%E3%82%A4%E3%83%B3%E3%82%BF%E3%83%BC%E3%83%95%E3%82%A7%E3%82%A4%E3%82%B9%E3%81%AE%E5%BD%B9/

//Pool 参考 https://qiita.com/KeichiMizutani/items/ca46a40de02e87b3d8a8

public enum StageState
{
    CononFire,
    EnemyMove
}


public class LevelStatelManager : MonoBehaviour
{
    [SerializeField] private LevelSetting _levelSetting = default;

    private bool _gameLoop = false;

    //Manager
    private InputManager _inputManager = default;
    
    //MonoManager
    [SerializeField] private PlayerManager _playerManager = default;
    [SerializeField] private UIManager _uiManager = default;

    public void LevelStart()
    {
        _gameLoop = true;

        //Manager
        _inputManager = new InputManager();
        _inputManager.InitializeStart(_levelSetting);

        //MonoManager
        _playerManager.InitializeStart(_levelSetting);
        _inputManager._mousePositionCommand.Add(_playerManager.CanonActor.MousePosition);
        _inputManager._mouseCommand.Add(_playerManager.WeaponManager.MouseCommand);

        _uiManager.InitializeStart(_levelSetting);
    }

    public void LevelRun()
    {
        _playerManager.SubscribeStart();
        _uiManager.SubscribeStart();


        //更新処理
        this.UpdateAsObservable()
            .Where(_ => _gameLoop)
            .Subscribe(_ =>
            {
                //アクター
                _inputManager.UpdateProcess();
                _playerManager.UpdateProcess();
                _uiManager.UpdateProcess();

            })
            .AddTo(this);
    }

    //ブチ切り用
    private void OnDisable()
    {
        //アクター
        _inputManager.FinalizeStart();
        _playerManager.FinalizeStart();
        _uiManager.FinalizeStart();

        _gameLoop = false;
    }
}
