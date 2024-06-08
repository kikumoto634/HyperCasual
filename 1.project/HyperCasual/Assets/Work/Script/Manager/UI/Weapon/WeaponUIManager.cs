using UniRx;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class WeaponUIManager : BaseMonoManager
{
    [SerializeField] private PlayerManager _playerManager = default;
    [SerializeField] private GameObject _weaponUILocations = default;
    [SerializeField] private GameObject _weaponUICollect = default;

    [SerializeField] private GameObject _pipeEnter = default;
    [SerializeField] private GameObject _pipeExit = default;
    [SerializeField] private GameObject _playerCanon = default;


    //����A�C�R���̈ʒu
    private List<RectTransform> _iconLocations = new List<RectTransform>();

    //����A�C�R��
    private List<Actor> _weaponUIs = new List<Actor>();

    private WeaponUIParam _param = new WeaponUIParam();
    private WeaponUISpawner _spawner = new WeaponUISpawner();

    public override void InitializeStart(LevelSetting levelSetting)
    {
        base.InitializeStart(levelSetting);

        //�ꏊ�̊m��
        foreach (RectTransform child in _weaponUILocations.transform)
        {
            _iconLocations.Add(child);
        }

        //�������핪�̐���
        for(int i = 0; i < _levelSetting._playersInfo._initWeaponNum; i++) 
        {
            Create();
        }
        RePosition();
    }
    public override void SubscribeStart()
    {
        base.SubscribeStart();

        //�ړ��J�n
        _playerManager.WeaponManager.IsAttack
            .Subscribe(flag =>
            {
                if(flag)MovementStart();
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


    //����
    public void Create()
    {
        WeaponUI weaponActor = (WeaponUI)_spawner.Spawn(_levelSetting._playersInfo._weaponUIPrefab, _param);
        weaponActor.transform.parent = _weaponUICollect.transform;

        _weaponUIs.Add(weaponActor);
    }

    //�ꏊ�̈ʒu�ݒ�
    public void RePosition()
    {
        int index = 0;
        foreach(var ui in _weaponUIs)
        {
            ui.transform.position = _iconLocations[index].transform.position;
            index++;
        }
    }

    //�ړ��J�n
    public void MovementStart()
    {
        float index = 0;    //index
        int completeIndex = 0;
        foreach (var ui in _weaponUIs)
        {
            index++;
            var sequence = DOTween.Sequence();
            sequence.Append(ui.transform.DOMoveY(_pipeEnter.transform.position.y, 1.0f))
                .SetDelay(index * _levelSetting._playersInfo._weaponUIIntervalSecond);
            sequence.Append(ui.transform.DOMoveX(_pipeEnter.transform.position.x, 1.0f));
            sequence.Append(ui.transform.DOMoveY(_pipeExit.transform.position.y, 1.0f));
            sequence.Append(ui.transform.DOMoveX(_playerCanon.transform.position.x, 1.0f))
                .OnComplete(() =>
                {
                    completeIndex++;
                    if (completeIndex == _weaponUIs.Count)
                    {
                        _playerManager.WeaponManager.IsFire = true;
                        RePosition();
                    }
                });

            sequence.Play();
        }
    }
}
