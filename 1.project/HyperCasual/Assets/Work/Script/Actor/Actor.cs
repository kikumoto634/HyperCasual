using UniRx;
using UnityEngine;

public class ActorParam
{
}

public class Actor : MonoBehaviour
{
    public BoolReactiveProperty IsActive { get; set; } = new BoolReactiveProperty(true);

    //ä÷êî
    public virtual void InitializeStart(LevelSetting levelSetting = default) { }
    public virtual void SubscribeStart() { }
    public virtual void UpdateProcess() { }
    public virtual void FinalizeStart() { }
}

public class ActorSpawner
{
    public virtual Actor Spawn(Actor prefab, ActorParam param)
    {
        return null;
    }
}