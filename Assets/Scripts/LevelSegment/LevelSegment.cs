using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegment : MonoBehaviour
{
    [SerializeField] private PlayerChar _playerChar;
    public PlayerChar playerChar { get { return _playerChar; } }
    [SerializeField] private Transform nexSegmentSpawnPoint;

    public LevelSegment CreateNextSegment(LevelSegment nextSegment)
    {
        LevelSegment segment = Instantiate(nextSegment, transform.parent);
        segment.transform.position = nexSegmentSpawnPoint.position;
        segment.transform.rotation = transform.rotation;
        _playerChar.nextPlayer = segment.playerChar;
        Init();
        return segment;
    }
    void Init()
    {
        _playerChar.OnGetBall += ActivateEnemies;
    }

    public void ActivateEnemies()
    {
        _playerChar.nextPlayer.OnGetBall += DeactivateEnemies;
        //EnemyBehaviour[] enemies = GetComponentsInChildren<EnemyBehaviour>();
        //foreach (var item in enemies)
        //{
        //    if (item != null)
        //    {
        //        item.Activate(transform);
        //    }
        //}
    }

    public void DeactivateEnemies()
    {
        EnemyBehaviour[] enemies = GetComponentsInChildren<EnemyBehaviour>();
        foreach (var item in enemies)
        {
            if (item != null)
            {
                item.Diactivate();
            }
        }
    }

    private void OnDestroy()
    {
        _playerChar.OnGetBall -= ActivateEnemies;
        if (_playerChar.nextPlayer != null)
        {
            _playerChar.nextPlayer.OnGetBall -= DeactivateEnemies;
        }
    }


}
