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

    public static Action<Vector3> OnMove;
    public static Action <float> OnThrow;
    public static Action OnStop;
    public static Action OnDraw;
    public static Action OnFireLoaded;
    public static bool fireLoaded;
    public static List<GameObject> EnemiesInRange = new List<GameObject>();

    private Vector3 forward;
    private Vector3 right;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
        Marker = picher.transform.position + ((cacher.transform.position - picher.transform.position) * pointBetweenPlayers);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        GameController.OnGameWin -= GameEnd;
        GameController.OnGameLose -= GameEnd;
        if(Instance == this)
        {
            Instance = null;
        }
    }

    private void GameEnd()
    {
        StopAllCoroutines();
        OnStop?.Invoke();
    }

    public void ThrowProjectile(float offset)
    {
        if (canBeThrown && !isMoveing)
        {
            OnThrow?.Invoke(offset);
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
            fireLoaded = false;
            Marker = picher.transform.position + ((cacher.transform.position - picher.transform.position) * pointBetweenPlayers);
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
    
    private void SpinTheBall()
    {
        ballTransform.Rotate(spinDerection);
    }

}
