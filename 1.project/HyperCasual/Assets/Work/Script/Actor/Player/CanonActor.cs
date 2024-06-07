using UnityEngine;
using UniRx;

public class CanonParam
{
    public float Velocity { get; set; }
}

public class CanonActor : Actor
{
    //パラメーター
    private CanonParam _canonParam = new CanonParam();

    //マウス
    public MousePositionCommand _mouse = default;

    public override void InitializeStart() 
    {
        base.InitializeStart();

        //Scriptableから取得
        _canonParam.Velocity = 10;

        _mouse = new MousePositionCommand();
    }
    public override void SubscribeStart() 
    {
        _mouse._mousePosition
            .Subscribe(pos =>
            {
                var worldPoint = Camera.main.ScreenToWorldPoint(pos);
                worldPoint.z = 0f;
                transform.up = Vector3.MoveTowards(
                    transform.up,
                    worldPoint - transform.position,
                    _canonParam.Velocity * Time.deltaTime);
            })
            .AddTo(this);
    }
    public override void UpdateProcess() 
    {
    }
    public override void FinalizeStart() 
    {
    }
}
