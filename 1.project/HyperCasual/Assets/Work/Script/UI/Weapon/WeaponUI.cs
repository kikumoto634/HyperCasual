
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIParam : ActorParam
{
}


public class WeaponUI : Actor
{

    internal WeaponUIParam _param;

    public override void InitializeStart(LevelSetting levelSetting)
    {
        base.InitializeStart(levelSetting);
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
}

public class WeaponUISpawner : ActorSpawner
{
    public override Actor Spawn(Actor prefab, ActorParam param)
    {
        ActorParam _param = param as ActorParam; ;
        WeaponUI _actor = (WeaponUI)Object.Instantiate(prefab);

        _actor._param = (WeaponUIParam)_param;

        return _actor;
    }
}
