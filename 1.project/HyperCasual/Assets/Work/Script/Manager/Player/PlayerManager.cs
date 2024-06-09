using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

public class PlayerManager : BaseMonoManager
{
    //入力オブジェクト
    public CanonActor CanonActor => _canonActor;
    public WeaponManager WeaponManager => _weaponManager;

    [SerializeField] private CanonActor _canonActor = default;

    //武器
    [SerializeField] private WeaponManager _weaponManager = default;

    //プレイヤーに関するすべてのオブジェクト
    private List<Actor> _playersActor = new List<Actor>();

    

    public override void InitializeStart(LevelSetting levelSetting)
    {
        base.InitializeStart(levelSetting);
        _weaponManager.InitializeStart(levelSetting);

        ActorAdd(_canonActor);
        ActorsAdd(_weaponManager.Weapons.ToList());

        foreach (var actor in _playersActor)
        {
            actor.InitializeStart(levelSetting);
        }
    }
    public override void SubscribeStart()
    {
        base.SubscribeStart();
        _weaponManager.SubscribeStart();

        _weaponManager.Weapons.ObserveAdd()
            .Subscribe(obj =>
            {
                ActorAdd(obj.Value);
            })
            .AddTo(this);

        foreach (var actor in _playersActor)
        {
            actor.SubscribeStart();
        }
    }
    public override void UpdateProcess()
    {
        base.UpdateProcess();
        _weaponManager.UpdateProcess();

        foreach (var actor in _playersActor)
        {
            actor.UpdateProcess();
        }
    }
    public override void FinalizeStart()
    {
        base.FinalizeStart();
        _weaponManager.FinalizeStart();

        foreach (var actor in _playersActor)
        {
            actor.FinalizeStart();
        }
        _playersActor.Clear();
    }



    public void ActorAdd(Actor actor)
    {
        _playersActor.Add(actor);
    }
    public void ActorsAdd(List<Weapon> actors)
    {
        foreach (var actor in actors)
        {
            _playersActor.Add(actor);
        }
    }
}
