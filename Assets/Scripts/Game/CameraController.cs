using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }
    [SerializeField] private Transform handTransform;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform subject;
    [SerializeField] private float zoomDistance;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float cameraTilt = 0.3f;
    [SerializeField] private Camera cam;
    [SerializeField] float maxFov;
    [SerializeField] float halfFov;
    [SerializeField] float minFov;

 
    [SerializeField] private float side = 1;

    [Header("return to forward view")]
    [SerializeField] float returnDerectionSpeed = 0.1f;
    [SerializeField] float returnDerection;
    private Vector3 startReturnForward;
    private Vector3 returnForward;
    private Vector3 cameraDerection;
    private Vector3 CameraRelativePosition;
    [SerializeField] float cameraTurnSpeed;

    [Header("On win")]
    [SerializeField] private float zoomOnPlayerDistance;
    private float zoomOnPlayerDistanceRemain;
    private Vector3 cameraForvordOnStart;
    [SerializeField] private float zoomOnPlayerSpeed;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
        subject = ProjectileScript.Instance.transform;
        CameraRelativePosition = cam.transform.position - subject.position;
        ProjectileTouchControl.OnAimStart += StartZoomingOut;
        ProjectileScript.OnMove += Move;
        ProjectileScript.OnStop += StartHalfZooming;
        ProjectileScript.OnThrow += ChooseSide;
        ProjectileScript.OnStop += StartReturnCamera;
        handTransform.forward = subject.position - handTransform.position;
        startReturnForward = handTransform.forward;
        GameController.OnGameWin += StartLookAtWiner;


    }

    private void Update()
    {
        Vector3 derection = ProjectileScript.Instance.Marker - cam.transform.position;
        //Vector3 derection = subject.position - cam.transform.position;
        cam.transform.forward = derection;
    
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        ProjectileTouchControl.OnAimStart -= StartZoomingOut;
        ProjectileScript.OnMove -= Move;
        ProjectileScript.OnStop -=  StartHalfZooming;
        ProjectileScript.OnThrow -= ChooseSide;
        ProjectileScript.OnStop -= StartReturnCamera;
        GameController.OnGameWin -= StartLookAtWiner;
    }

    private void Move(Vector3 moveVector)
    {
        transform.position = subject.transform.position + CameraRelativePosition;
        StopCoroutine(ReturnToForwardView());
        float distanceLeft = (GameController.cacher.transform.position - subject.position- subject.position).magnitude / (GameController.cacher.transform.position - GameController.picher.transform.position).magnitude;
    }

    private void StartZoomingOut()
    {
        StopAllCoroutines();
        StartCoroutine(ZoomOut());
    }

    private void StartZoomingIn()
    {
        StopAllCoroutines();

        StartCoroutine(ZoomIn());
    }

    private void StartHalfZooming()
    {
        StopAllCoroutines();

        StartCoroutine(HalfZoom());
    }

    private IEnumerator ZoomIn()
    {
        yield return null;
        if (cam.fieldOfView > minFov )
        {
            cam.fieldOfView -= zoomSpeed*Time.deltaTime;
            StartCoroutine(ZoomIn());
        }
    }

    private IEnumerator ZoomOut()
    {
        yield return null;
        if (cam.fieldOfView < maxFov)
        {
            cam.fieldOfView += zoomSpeed * Time.deltaTime;
            StartCoroutine(ZoomOut());
        }
    }

    private IEnumerator HalfZoom()
    {
        yield return null;
        if (cam.fieldOfView < halfFov)
        {
            cam.fieldOfView += zoomSpeed * Time.deltaTime;
            if (cam.fieldOfView < halfFov)
            {
                StartCoroutine(HalfZoom());
            }
            else
            {
                cam.fieldOfView = halfFov;
            }
        }
        else if (cam.fieldOfView > halfFov)
        {
            cam.fieldOfView -= zoomSpeed * Time.deltaTime;
            if (cam.fieldOfView > halfFov)
            {
                StartCoroutine(HalfZoom());
            }
            else
            {
                cam.fieldOfView = halfFov;
            }
        }
    }

    private void StartReturnCamera()
    {
        returnForward = handTransform.forward;
        StartCoroutine(ReturnToForwardView());
        returnDerection = 1;
    }

    private IEnumerator ReturnToForwardView()
    {
        yield return null;

        if (returnDerection > 0.1)
        {
           
            handTransform.forward = Vector3.Lerp(startReturnForward,returnForward, returnDerection);
            returnDerection -= returnDerectionSpeed * Time.deltaTime;
            StartCoroutine(ReturnToForwardView());
        }

    }

    private void ChooseSide(float offset)
    {
        StartZoomingIn();
        side = offset > 0 ? -1 : 1;
    }

    private void StartLookAtWiner()
    {
        zoomOnPlayerDistanceRemain = zoomOnPlayerDistance;
        StartCoroutine(LookAtWiner());
        cameraForvordOnStart = cameraTransform.forward;
    }

    private IEnumerator LookAtWiner()
    {
        yield return null;

        if(zoomOnPlayerDistanceRemain > 0)
        {
            zoomOnPlayerDistanceRemain -= zoomOnPlayerSpeed * Time.deltaTime;
            handTransform.position -= handTransform.up * zoomOnPlayerSpeed * Time.deltaTime;
            handTransform.forward = subject.position - handTransform.position;
            StartCoroutine(LookAtWiner());
            cameraTransform.forward = Vector3.Lerp(handTransform.forward, cameraForvordOnStart, zoomOnPlayerDistanceRemain / zoomOnPlayerDistance);
        }
    }
    
}












































/*
public static CameraController Instance { get; private set; }
[SerializeField] private Transform handTransform;
[SerializeField] private Transform cameraTransform;
[SerializeField] private Transform subject;
[SerializeField] private float zoomDistance;
[SerializeField] private float zoomSpeed;
[SerializeField] private float cameraTilt = 0.3f;



[SerializeField] private float side = 1;

[Header("return to forward view")]
[SerializeField] float returnDerectionSpeed = 0.1f;
[SerializeField] float returnDerection;
private Vector3 startReturnForward;
private Vector3 returnForward;


[Header("On win")]
[SerializeField] private float zoomOnPlayerDistance;
private float zoomOnPlayerDistanceRemain;
private Vector3 cameraForvordOnStart;
[SerializeField] private float zoomOnPlayerSpeed;

private void Awake()
{
    Instance = this;
}

private void Start()
{
    subject = ProjectileScript.Instance.transform;
    ProjectileTouchControl.OnAimStart += StartZoomingOut;
    ProjectileScript.OnMove += Move;
    ProjectileScript.OnThrow += ChooseSide;
    ProjectileScript.OnStop += StartReturnCamera;
    handTransform.forward = subject.position - handTransform.position;
    startReturnForward = handTransform.forward;
    GameController.OnGameWin += StartLookAtWiner;
}

private void OnDestroy()
{
    StopAllCoroutines();
    ProjectileTouchControl.OnAimStart -= StartZoomingOut;
    ProjectileScript.OnMove -= Move;
    ProjectileScript.OnThrow -= ChooseSide;
    ProjectileScript.OnStop -= StartReturnCamera;
    GameController.OnGameWin -= StartLookAtWiner;
}

private void Move(Vector3 moveVector)
{
    StopCoroutine(ReturnToForwardView());
    Vector3 derection = subject.position - handTransform.position;
    handTransform.forward = derection;//new Vector3(0,v3.y, v3.z);
    transform.position += transform.forward*(moveVector.x);
    transform.position -= transform.up*moveVector.y * cameraTilt * side; 
}

private void StartZoomingOut()
{
    StopAllCoroutines();
    StartCoroutine(ZoomOut());
}

private void StartZoomingIn()
{
    StopAllCoroutines();

    StartCoroutine(ZoomIn());
}

private IEnumerator ZoomIn()
{
    yield return null;
    handTransform.position += (transform.position - handTransform.position).normalized * zoomSpeed * Time.deltaTime;
    if ((transform.position - handTransform.position).magnitude <= zoomSpeed * Time.deltaTime)
    {
        handTransform.position = transform.position;
    }
    else
    {
        StartCoroutine(ZoomIn());
    }
}

private IEnumerator ZoomOut()
{
    yield return null;
    handTransform.position -= handTransform.forward * zoomSpeed * Time.deltaTime;
    if ((transform.position - handTransform.position).magnitude >= zoomDistance)
    {

    }
    else
    {
        StartCoroutine(ZoomOut());
    }
}

private void StartReturnCamera()
{
    returnForward = handTransform.forward;
    StartCoroutine(ReturnToForwardView());
    returnDerection = 1;
}

private IEnumerator ReturnToForwardView()
{
    yield return null;

    if (returnDerection > 0.1)
    {

        handTransform.forward = Vector3.Lerp(startReturnForward,returnForward, returnDerection);
        returnDerection -= returnDerectionSpeed * Time.deltaTime;
        StartCoroutine(ReturnToForwardView());
    }

}

private void ChooseSide(float offset)
{
    StartZoomingIn();
    side = offset > 0 ? -1 : 1;
}

private void StartLookAtWiner()
{
    zoomOnPlayerDistanceRemain = zoomOnPlayerDistance;
    StartCoroutine(LookAtWiner());
    cameraForvordOnStart = cameraTransform.forward;
}

private IEnumerator LookAtWiner()
{
    yield return null;

    if(zoomOnPlayerDistanceRemain > 0)
    {
        zoomOnPlayerDistanceRemain -= zoomOnPlayerSpeed * Time.deltaTime;
        handTransform.position -= handTransform.up * zoomOnPlayerSpeed * Time.deltaTime;
        handTransform.forward = subject.position - handTransform.position;
        StartCoroutine(LookAtWiner());

        cameraTransform.forward = Vector3.Lerp(handTransform.forward, cameraForvordOnStart, zoomOnPlayerDistanceRemain / zoomOnPlayerDistance);
    }
}
 */
