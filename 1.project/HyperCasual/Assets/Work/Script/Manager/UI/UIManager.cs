using UnityEngine;

public class UIManager : BaseMonoManager
{
    [SerializeField] private WeaponUIManager _weaponUIManager = default;

    public override void InitializeStart(LevelSetting levelSetting)
    {
        base.InitializeStart(levelSetting);

        _weaponUIManager.InitializeStart(levelSetting);
    }
    public override void SubscribeStart()
    {
        base.SubscribeStart();

        _weaponUIManager.SubscribeStart();
    }
    public override void UpdateProcess()
    {
        base.UpdateProcess();

        _weaponUIManager.UpdateProcess();
    }
    public override void FinalizeStart()
    {
        base.FinalizeStart();

        _weaponUIManager.FinalizeStart();
    }
}
