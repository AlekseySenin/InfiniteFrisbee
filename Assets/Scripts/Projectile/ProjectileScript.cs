using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public static ProjectileScript Instance { get; private set; }
    [SerializeField] private PlayerChar picher;
    [SerializeField] private PlayerChar cacher;
    [SerializeField] private float speed;
    [SerializeField]
    [Range(-2f,2f)]
    private float offset;
    [SerializeField] private GameObject dotContainer;

    [SerializeField] private ProjectileDotsData dotsData;
    private List<GameObject> dots = new List<GameObject>();
    [SerializeField] private Vector3 spinDerection;
    [SerializeField] private Transform ballTransform;
    [SerializeField] float pointBetweenPlayers = 0.3f;
    [SerializeField] private GameObject fireTrale;
    [SerializeField] private GameObject ballPrefab;
    private float absOffset;
    private float distance;
    private float pos;
    public static bool isMoveing { get; private set; }
    public bool canBeThrown { get; private set; }
    public Vector3 Marker { get; private set; }
    public bool canAim { get; private set; } = true;
    public bool LevelFiniched = false;

    public static Action<Vector3> OnMove;
    public static Action <float> OnThrow;
    public static Action OnStop;
    public static Action OnDraw;
    public static Action OnFireLoaded;
    public static bool fireLoaded;
    public static List<GameObject> EnemiesInRange = new List<GameObject>();

    private Vector3 forward;
    private Vector3 right;

    public static float FireAmount { get; private set; }
    public static bool suupershot { get; private set; }
    public static float FireToShoot = 10;

    public static void AddFire(float value)
    {
        if (FireAmount <= FireToShoot)
        {
            FireAmount += value;
            fireLoaded = false;
        }

        else
        {
            OnFireLoaded?.Invoke();
            fireLoaded = true;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void Init(PlayerChar playerA, PlayerChar playerB)
    {
        picher = playerA;
        cacher = playerB;
        isMoveing = false;
        fireTrale.SetActive(false);
        picher.Cach();
        GoToHand(picher.hand);
        canBeThrown = picher != null && cacher != null;
        cacher = picher.nextPlayer;
        GameController.picher = picher;
        GameController.cacher = cacher;
        GameController.OnGameWin += GameEnd;
        GameController.OnGameLose += GameEnd;
        picher.projectile = this;
        //picher.Activate();
        StartCoroutine(CreateDots());
        Marker = picher.transform.position + ((cacher.transform.position - picher.transform.position) * pointBetweenPlayers);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        GameController.OnGameWin -= GameEnd;
        GameController.OnGameLose -= GameEnd;
        FireAmount = 0;
    }

    private void GameEnd()
    {
        StopAllCoroutines();
        LevelFiniched = true;
    }

    public void HideDots()
    {
        dotContainer.SetActive(false);
        canAim = false;
    }

    public void ThrowProjectile(float offset)
    {
        if (canBeThrown && !isMoveing)
        {
            transform.parent = picher.transform.parent;
            OnThrow?.Invoke(offset);
            dotContainer.SetActive(false);
            isMoveing = true;
            absOffset = speed * offset;
            this.offset = offset;
            transform.forward = (cacher.transform.position - transform.position).normalized;
            forward = transform.forward;
            right = transform.right;
            distance = (transform.position - cacher.transform.position).magnitude;
            pos = 0 - (distance / 2);
            StartCoroutine(Fly());
            if( TryGetComponent(out AudioSource source))
            {
                SoundManager.PlaySound(source,SoundType.throwBall);
            }
        }
    }

    public void SuperThrow()
    {
        fireTrale.SetActive(true);
        suupershot = true;
        FireAmount = 0;
        ThrowProjectile(0);

    }

    private IEnumerator Fly()
    {
        yield return null;
        Vector3 startMovePosition = transform.position;
        Vector3 EndMovePosition = transform.position;
        Vector3 yMove = (right * (absOffset * (pos / (distance / 2)))*Time.deltaTime);
        Vector3 xMove = (forward * (speed - yMove.magnitude)) * Time.deltaTime;
        EndMovePosition += xMove;
        EndMovePosition += yMove;
        transform.position += xMove;
        transform.position += yMove;

        OnMove?.Invoke(EndMovePosition - startMovePosition);
        pos += xMove.magnitude;

        transform.forward = (xMove + yMove).normalized;
        SpinTheBall();
        if (pos <= distance / 2)
        {
            StartCoroutine(Fly());
            Marker = transform.position;
        }
        else if(cacher.nextPlayer!=null)
        {
            if (TryGetComponent(out AudioSource source))
            {
                SoundManager.PlaySound(source, SoundType.cachBall);
            }
            fireTrale.SetActive(false);
            transform.position = cacher.transform.position;
            picher = cacher;
            cacher = picher.nextPlayer;
            isMoveing = false;
            GameController.picher = picher;
            GameController.cacher = cacher;
            GoToHand(picher.hand);
            picher.projectile = this;
            canAim = true;
            picher.Cach();
            GameController.playersPas++;
            ballTransform.rotation = transform.rotation;
            OnStop?.Invoke();
            suupershot = false;
            fireLoaded = false;
            Marker = picher.transform.position + ((cacher.transform.position - picher.transform.position) * pointBetweenPlayers);
            Debug.Log("Stoped");
        }

        else
        {
            fireTrale.SetActive(false);
            ballTransform.gameObject.SetActive(false);
            StopAllCoroutines();
            fireTrale.SetActive(false);
            if (TryGetComponent(out AudioSource source))
            {
                SoundManager.PlaySound(source, SoundType.win);
            }
            GameController.playersPas++;
            canBeThrown = false;
            GameController.OnGameWin?.Invoke();
            suupershot = false;
            fireLoaded = false;
        }

    }

    public void GoToHand (Transform hand)
    {
        transform.position = hand.position;
        transform.parent = hand;
        transform.rotation = hand.rotation;
        transform.Rotate(0, 0, 90);
        transform.position += transform.transform.right * 0.5f;
        ballTransform.rotation = transform.rotation;
    }

    public void DrowTrajectory(float offset)
    {
        //if (canBeThrown && !isMoveing && canAim)
        {
            OnDraw?.Invoke();
            dotContainer.SetActive(true);
            dotContainer.transform.parent = picher.transform;
            distance = (picher.transform.position - cacher.transform.position).magnitude;
            float section = distance / (dots.Count);
            float dotsOffset = section * distance;
            this.offset = offset;
            float dotOffset = offset!=0? offset / (dots.Count/2): (dots.Count / 2);
            transform.forward = (cacher.transform.position - picher.transform.position).normalized;
            Vector3 derection = (cacher.transform.position - picher.transform.position).normalized;
            Vector3 dotPositopn = transform.position;
            for (int i = 0; i < dots.Count; i++)
            {
                float dotsPos = section * (i + 0.5f);

                Vector3 yMove = picher.transform.right * dotOffset * (dotsPos - (distance / 2));

                Vector3 xMove = picher.transform.forward * (section);

                dotPositopn += xMove;
                dotPositopn += yMove;
                dots[i].transform.position = dotPositopn;
                dots[i].transform.up = Vector3.up;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnStop?.Invoke();
        if (!collision.gameObject == picher.gameObject || !collision.gameObject == cacher.gameObject && !suupershot)
        {
            Debug.Log(collision.gameObject.name);
            if (collision.transform.parent.parent.parent.gameObject.TryGetComponent(out EnemyProjectileLocator locator))
            {
                GoToHand(locator.hand);
            }
           GameController.OnGameLose?.Invoke();
            if (TryGetComponent(out AudioSource source))
            {
                SoundManager.PlaySound(source, SoundType.lose);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnStop?.Invoke();
        if (other.gameObject != picher.gameObject && other.gameObject != cacher.gameObject && !suupershot)
        {
            if (other.transform.gameObject.TryGetComponent(out EnemyProjectileLocator locator))
            {
                GoToHand(locator.hand);
            }
            GameController.OnGameLose?.Invoke();
        }
    }

    private void SpinTheBall()
    {
        ballTransform.Rotate(spinDerection);
    }

    private IEnumerator CreateDots()
    {
        yield return null;
        if (dots.Count < dotsData.numberOfDots)
        {
            GameObject newDot = Instantiate(dotsData.dotPrefab, dotContainer.transform);
            dots.Add(newDot);
            newDot.GetComponent<SpriteRenderer>().color = Color.Lerp(dotsData.StartColor, dotsData.endColor, (float)dots.Count / (float)dotsData.numberOfDots);
            StartCoroutine(CreateDots());
        }
    }
}
