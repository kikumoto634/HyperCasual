using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class WeaponParam : ActorParam
{
    public float Velocity { get; set; }
}

public class Weapon : Actor
{
    internal WeaponParam _param;
    private Rigidbody2D _rb = default;

    public override void InitializeStart(LevelSetting levelSetting)
    {
        base.InitializeStart();
        _rb = GetComponent<Rigidbody2D>();
        IsActive.Value = false;
    }
    public override void SubscribeStart()
    {
        base.SubscribeStart();

        //�A�N�e�B�u
        IsActive
            .Subscribe(flag =>
            {
                gameObject.SetActive(flag);
            });

        //����(�C���Ώ�)
        this.OnCollisionEnter2DAsObservable()
            .Subscribe(collider =>
            {
                if(collider.gameObject.layer == 6)
                {
                    Debug.Log("�폜�G���A");
                    IsActive.Value = false;
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


    public void Fire(Transform location)
    {
        IsActive.Value = true;
        transform.position = location.position;
        _rb.velocity = Vector2.zero;
        _rb.AddForce(location.up * _param.Velocity, ForceMode2D.Impulse);
    }
}

public class WeaponSpawner : ActorSpawner
{
    public override Actor Spawn(Actor prefab, ActorParam param)
    {
        ActorParam _param = param as ActorParam; ;
        Weapon _actor = (Weapon)Object.Instantiate(prefab);

        _actor._param = (WeaponParam)_param;

        return _actor;
    }
}
