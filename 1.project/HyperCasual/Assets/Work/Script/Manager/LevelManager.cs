using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelStatelManager _stateManager = default;

    public void InitializeStart()
    {
        _stateManager.LevelStart();
        _stateManager.LevelRun();
        _stateManager.LevelFinalize();
    }
}