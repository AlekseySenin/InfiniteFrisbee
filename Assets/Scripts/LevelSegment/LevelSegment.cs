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
    protected virtual void Init()
    {

    }

 



    private void OnDestroy()
    {

        if (_playerChar.nextPlayer != null)
        {

        }
    }


}
