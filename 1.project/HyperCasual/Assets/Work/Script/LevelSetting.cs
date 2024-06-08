using Alchemy.Inspector;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSetting")]
public class LevelSetting : ScriptableObject
{
    [TabGroup("PlayersInfo")] public PlayersInfo _playersInfo;
}

[System.Serializable]
public struct PlayersInfo
{
    [BoxGroup("Canon")] public CanonActor _playerPrefab;
    [BoxGroup("Canon")] public float _playerVelocity;

    [BoxGroup("Weapon")] public Weapon _weaponPrefab;
    [BoxGroup("Weapon")] public int _initWeaponNum;
    [BoxGroup("Weapon")] public float _weaponVelocity;
    [BoxGroup("Weapon"), Range(0.0f, 0.5f)] public float _weaponIntervalSecond;

    [BoxGroup("WeaponUI")] public WeaponUI _weaponUIPrefab;
}
