using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrajectory : MonoBehaviour
{
    [SerializeField] private ProjectileDotsData dotsData;
    [SerializeField] private GameObject dotContainer;
    private List<GameObject> dots = new List<GameObject>();

    public static Action OnDraw;


    private void Start()
    {
        StartCoroutine(CreateDots());
        TouchControl.OnTouchMoved += DrowTrajectory;
        TouchControl.OnTouchEnded += HideDots;
    }

    private void OnDestroy()
    {
        TouchControl.OnTouchMoved -= DrowTrajectory;
        TouchControl.OnTouchEnded -= HideDots;

    }

    private void HideDots(float val)
    {
        dotContainer.SetActive(false);
    }

    private IEnumerator CreateDots()
    {
        yield return null;
        if (dots.Count < dotsData.numberOfDots)
        {
            GameObject newDot = Instantiate(dotsData.dotPrefab, dotContainer.transform);
            newDot.transform.parent = dotContainer.transform;
            dots.Add(newDot);
            newDot.GetComponent<SpriteRenderer>().color = Color.Lerp(dotsData.StartColor, dotsData.endColor, (float)dots.Count / (float)dotsData.numberOfDots);
            StartCoroutine(CreateDots());
        }
    }
    public void DrowTrajectory(float offset)
    {


            OnDraw?.Invoke();
            dotContainer.SetActive(true);
            dotContainer.transform.parent = GameController.picher.transform;
            float distance = (GameController.picher.transform.position - GameController.cacher.transform.position).magnitude;
            float section = distance / (dots.Count);
            float dotsOffset = section * distance;
            float dotOffset = offset != 0 ? offset / (dots.Count / 2) : (dots.Count / 2);
            Vector3 dotsPositopn = transform.position;
            for (int i = 0; i < dots.Count; i++)
            {
                float dotsPos = section * (i + 0.5f);

                Vector3 yMove = GameController.picher.transform.right * dotOffset * (dotsPos - (distance / 2));

                Vector3 xMove = GameController.picher.transform.forward * (section);

                dotsPositopn += xMove;
                dotsPositopn += yMove;
                dots[i].transform.position = dotsPositopn;
                dots[i].transform.up = Vector3.up;
            }
        
    }
}
