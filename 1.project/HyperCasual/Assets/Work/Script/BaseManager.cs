using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager{

    public virtual void InitializeStart() { }
    public virtual void SubscribeStart() { }
    public virtual void UpdateProcess() { }
    public virtual void FinalizeStart() { }

}
