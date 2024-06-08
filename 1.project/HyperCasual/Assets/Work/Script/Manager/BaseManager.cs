using UnityEngine;

public class BaseManager{

    public LevelSetting _levelSetting = default;

    public virtual void InitializeStart(LevelSetting levelSetting) 
    {
        _levelSetting = levelSetting;
    }
    public virtual void UpdateProcess() { }
    public virtual void FinalizeStart() { }

}

public class BaseMonoManager : MonoBehaviour
{

    public LevelSetting _levelSetting = default;

    public virtual void InitializeStart(LevelSetting levelSetting)
    {
        _levelSetting = levelSetting;
    }
    public virtual void SubscribeStart() { }
    public virtual void UpdateProcess() { }
    public virtual void FinalizeStart() { }

}