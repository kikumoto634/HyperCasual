using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class WeaponManager : BaseMonoManager
{
    public ReactiveCollection<Weapon> Weapons => _weaponActors;
    public MouseCommand MouseCommand => _mouseCommand;
    public bool IsFire
    {
        set
        {
            _isFire.Value = value;
        }
    }


    [SerializeField] private Transform _shotTransform = default;
    [SerializeField] private GameObject _weaponCollect = default;


    private ReactiveCollection<Weapon> _weaponActors = new ReactiveCollection<Weapon>();

    private WeaponParam _param = new WeaponParam();
    private WeaponSpawner _spawner = new WeaponSpawner();

    //É}ÉEÉXì¸óÕ(UIèàóùÇ©ÇÁì¸ÇÈ)
    private MouseCommand _mouseCommand = default;
    private BoolReactiveProperty _isFire = new BoolReactiveProperty(false);

    public override void InitializeStart(LevelSetting levelSetting)
    {
        base.InitializeStart(levelSetting);

        _mouseCommand = new MouseCommand(0);

        //èâä˙å¬êî
        for (int i = 0; i < levelSetting._playersInfo._initWeaponNum; i++) 
        {
            Create();
        }
    }
    public override void SubscribeStart()
    {
        base.SubscribeStart();

        //î≠éÀ
        _isFire
            .Subscribe(flag =>
            {
                if (flag)
                {
                    Fire();
                }
            })
            .AddTo(this);
    }
    public override void UpdateProcess()
    {
        base.UpdateProcess();
    }
    public override void FinalizeStart()
    {
        base.FinalizeStart();
    }


    public void Create()
    {
        _param.Velocity = _levelSetting._playersInfo._weaponVelocity;
        Weapon weaponActor = (Weapon)_spawner.Spawn(_levelSetting._playersInfo._weaponPrefab, _param);
        weaponActor.transform.parent = _weaponCollect.transform;

        _weaponActors.Add(weaponActor);
    }
    public async void Fire()
    {
        foreach (var weapon in _weaponActors)
        {
            weapon.Fire(_shotTransform);
            await UniTask.Delay(TimeSpan.FromSeconds(_levelSetting._playersInfo._weaponIntervalSecond));
        }
        _isFire.Value = false;
    }
}
