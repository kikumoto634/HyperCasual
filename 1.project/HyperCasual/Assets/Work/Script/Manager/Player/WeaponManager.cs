using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class WeaponManager : BaseMonoManager
{
    public ReactiveCollection<Weapon> Weapons => _weaponActors;
    public BoolReactiveProperty IsAttack
    {
        get
        {
            return _isAttack;
        }
        set
        {
            _isAttack = value;
        }
    }
    public BoolReactiveProperty IsMoveComp => _isMoveComp;
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
    //í«â¡ó\íËÉAÉCÉRÉì
    private Queue<Weapon> _weaponsCreateQueue = new Queue<Weapon>();

    private WeaponParam _param = new WeaponParam();
    private WeaponSpawner _spawner = new WeaponSpawner();

 
    //çsìÆ
    private BoolReactiveProperty _isAttack = new BoolReactiveProperty(false);
    private BoolReactiveProperty _isFire = new BoolReactiveProperty(false);
    private BoolReactiveProperty _isMoveComp = new BoolReactiveProperty(false);

    public override void InitializeStart(LevelSetting levelSetting)
    {
        base.InitializeStart(levelSetting);

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
                    Debug.Log("î≠éÀ");
                    Fire();
                }
            })
            .AddTo(this);
    }
    public override void UpdateProcess()
    {
        base.UpdateProcess();


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_weaponActors.Count < 10)
            {
                 CreateQueue();
             }
        }

        if (!_isFire.Value)
        {
            foreach(var actor in _weaponsCreateQueue)
            {
                _weaponActors.Add(actor);
            }
            _weaponsCreateQueue.Clear();
        }

        //çUåÇíÜ
        if (_isFire.Value && _weaponActors.All(actor => !actor.IsActive.Value))
        {
            Debug.Log("çUåÇèIóπ");
            _isMoveComp.Value = true;
        }
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
    public void CreateQueue()
    {
        _param.Velocity = _levelSetting._playersInfo._weaponVelocity;
        Weapon weaponActor = (Weapon)_spawner.Spawn(_levelSetting._playersInfo._weaponPrefab, _param);
        weaponActor.transform.parent = _weaponCollect.transform;

        _weaponsCreateQueue.Enqueue(weaponActor);
        weaponActor.InitializeStart(_levelSetting);
        weaponActor.SubscribeStart();
    }
    public async void Fire()
    {
        foreach (var weapon in _weaponActors)
        {
            weapon.Fire(_shotTransform);
            await UniTask.Delay(TimeSpan.FromSeconds(_levelSetting._playersInfo._weaponIntervalSecond));
        }
    }

    public void MoveEnd()
    {
        _isAttack.Value = false;
        _isFire.Value = false;
        _isMoveComp.Value = false;
    }
}
