using UnityEngine;
using UniRx;

public class CanonParam : ActorParam
{
    public float Velocity { get; set; }
}

public class CanonActor : Actor
{
    public MousePositionCommand MousePosition => _mouse;


    //�p�����[�^�[
    private CanonParam _canonParam = new CanonParam();

    //�}�E�X
    private MousePositionCommand _mouse = default;

    public override void InitializeStart(LevelSetting levelSetting) 
    {
        base.InitializeStart();

        //Scriptable����擾
        _canonParam.Velocity = levelSetting._playersInfo._playerVelocity;

        _mouse = new MousePositionCommand();
    }
    public override void SubscribeStart() 
    {
        _mouse._mousePosition
            .Subscribe(pos =>
            {
                //Debug.Log(pos);
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
