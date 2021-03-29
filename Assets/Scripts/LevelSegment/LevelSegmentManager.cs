using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegmentManager : MonoBehaviour
{
    [SerializeField] LevelSegmentFactory levelFactory;
    [SerializeField] int MaxSegmentaAmount;
    [SerializeField] int displaySegments = 3;
    private List<LevelSegment> levelSegments = new List<LevelSegment>();
    private int segmentsDisplayed = 0;

    void Start()
    {
        levelSegments.Add(Instantiate(levelFactory.GetFirstLevelSegment()));
        while (levelSegments.Count < MaxSegmentaAmount)
        {
            levelSegments.Add(levelSegments[levelSegments.Count - 1].CreateNextSegment(levelFactory.GetLevelSegment(false)));
        }


        ProjectileScript.Instance.Init(levelSegments[0].playerChar, levelSegments[1].playerChar);
        PlayerChar.OnSegmentPassed += AddSegment;
    }

    private void OnDestroy()
    {
        PlayerChar.OnSegmentPassed -= AddSegment;
    }

    void AddSegment()
    {
        levelSegments.Add(levelSegments[levelSegments.Count - 1].CreateNextSegment(levelFactory.GetLevelSegment()));
       
        if(segmentsDisplayed < displaySegments)
        {
            segmentsDisplayed++;
        }
        else
        {
            Destroy(levelSegments[0].gameObject);
            levelSegments.RemoveAt(0);
        }
    }

}
