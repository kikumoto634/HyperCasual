using System.Collections.Generic;
using UnityEngine;

public class WeaponUIManager : BaseMonoManager
{
    [SerializeField] private PlayerManager _playerManager = default;
    [SerializeField] private GameObject _weaponUILocations = default;
    [SerializeField] private GameObject _weaponUICollect = default;

    //武器アイコンの位置
    private List<RectTransform> _iconLocations = new List<RectTransform>();

    //武器アイコン
    private List<Actor> _weaponUIs = new List<Actor>();

    private WeaponUIParam _param = new WeaponUIParam();
    private WeaponUISpawner _spawner = new WeaponUISpawner();

    public override void InitializeStart(LevelSetting levelSetting)
    {
        base.InitializeStart(levelSetting);

        //場所の確保
        foreach (RectTransform child in _weaponUILocations.transform)
        {
            _iconLocations.Add(child);
        }

        //初期武器分の生成
        for(int i = 0; i < _levelSetting._playersInfo._initWeaponNum; i++) 
        {
            Create();
        }
        RePosition();
    }
    public override void SubscribeStart()
    {
        base.SubscribeStart();
    }
    public override void UpdateProcess()
    {
        base.UpdateProcess();
    }
    public override void FinalizeStart()
    {
        base.FinalizeStart();
    }


    //生成
    public void Create()
    {
        WeaponUI weaponActor = (WeaponUI)_spawner.Spawn(_levelSetting._playersInfo._weaponUIPrefab, _param);
        weaponActor.transform.parent = _weaponUICollect.transform;

        _weaponUIs.Add(weaponActor);
    }

    //場所の位置設定
    public void RePosition()
    {
        int index = 0;
        foreach(var ui in _weaponUIs)
        {
            ui.transform.position = _iconLocations[index].transform.position;
            index++;
        }
    }
}
