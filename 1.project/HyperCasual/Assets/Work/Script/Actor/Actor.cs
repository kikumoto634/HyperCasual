using UnityEngine;

public class Actor : MonoBehaviour
{
    public bool IsActive { get; set; }

    //�֐�
    public virtual void InitializeStart() { }
    public virtual void SubscribeStart() { }
    public virtual void UpdateProcess() { }
    public virtual void FinalizeStart() { }
}

