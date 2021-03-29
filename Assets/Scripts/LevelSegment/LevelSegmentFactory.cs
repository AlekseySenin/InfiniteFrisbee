using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSegmentFactory
{
    [SerializeField] LevelSegment firstSegment;
    [SerializeField] LevelSegment[] levelSegmentPrefabs;
    [SerializeField] int range;
    [SerializeField] int dificoultyStep;
    private int step;
    private int dificoulty = 0;
    int maxDif { get
        {
            return Mathf.Clamp(range + dificoulty, 0, levelSegmentPrefabs.Length - 1);
        } }
    int minDif { get
        {
            return Mathf.Clamp(maxDif - range,0,maxDif);
        } }

    public LevelSegment GetFirstLevelSegment()
    {
        return firstSegment;
    }

    public LevelSegment GetLevelSegment(bool addDificoulty = true)
    {
        if (addDificoulty)
        {
            AddDificoulty();
        }

        return levelSegmentPrefabs[Random.Range(minDif,maxDif)];
    }

    private void AddDificoulty()
    {
        step++;
        if (step % dificoultyStep == 0)
        {
            dificoulty++;
            step = 0;
        }
    }
}
