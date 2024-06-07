using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager = default;
    public void Start()
    {
        _levelManager.InitializeStart();
    }
}
