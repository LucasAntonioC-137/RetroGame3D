using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerMovement2 : MonoBehaviour
{

    public float xySpeed = 18;
    public float lookSpeed = 340f;
    public float forwardSpeed = 6;
    public float turnSpeed = 60f;
    public float maxAngle = 50.0f;
    //public CinemachineDollyCart dolly;
    public Transform cameraParent;
    public int invert = -1;
    private Transform playerModel;
    // Start is called before the first frame update
    void Start()
    {
        playerModel = transform.GetChild(0);
        //SetSpeed(forwardSpeed);
    }
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(h, invert * v, 0);
        Vector3 finalDirection = new Vector3(h, invert * v, 2.0f);
        LocalMove(direction, xySpeed);
        RotationLook(finalDirection, lookSpeed, maxAngle);
        HorizontalLean(playerModel, h, 80, .1f);
        if (Input.GetButtonDown("Fire2"))
            Boost(true);

        if (Input.GetButtonUp("Fire2"))
            Boost(false);

        if (Input.GetButtonDown("Fire3"))
            Break(true);

        if (Input.GetButtonUp("Fire3"))
            Break(false);

    }

    void LocalMove(Vector3 direction, float speed)
    {
        transform.localPosition += direction * speed * Time.deltaTime;
        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void RotationLook(Vector3 direction, float speed, float maxAngle)
    {
        if (direction.x != 0 || direction.y != 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), Mathf.Deg2Rad * maxAngle);
        }

    }

    void HorizontalLean(Transform target, float axis, float leanlimit, float lerpTime)
    {
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanlimit, lerpTime));
    }

    /*void SetSpeed(float x)
    {
        dolly.m_Speed = x;
    }*/

    void SetCameraZoom(float zoom, float duration)
    {
        cameraParent.DOLocalMove(new Vector3(0, 0, zoom), duration);
    }

    void DistortionAmount(float x)
    {
        Camera.main.GetComponent<PostProcessVolume>().profile.GetSetting<LensDistortion>().intensity.value = x;
    }

    void FieldOfView(float fov)
    {
        cameraParent.GetComponentInChildren<CinemachineVirtualCamera>().m_Lens.FieldOfView = fov;
    }

    void Chromatic(float x)
    {
        Camera.main.GetComponent<PostProcessVolume>().profile.GetSetting<ChromaticAberration>().intensity.value = x;
    }

    void Boost(bool state)
    {

        //if (state)
        //{
        //    cameraParent.GetComponentInChildren<CinemachineImpulseSource>().GenerateImpulse();
        //    trail.Play();
        //    circle.Play();
        //}
        //else
        //{
        //    trail.Stop();
        //    circle.Stop();
        //}
        //trail.GetComponent<TrailRenderer>().emitting = state;

        float origFov = state ? 40 : 55;
        float endFov = state ? 55 : 40;
        float origChrom = state ? 0 : 1;
        float endChrom = state ? 1 : 0;
        float origDistortion = state ? 0 : -30;
        float endDistorton = state ? -30 : 0;
        float starsVel = state ? -20 : -1;
        float speed = state ? forwardSpeed * 2 : forwardSpeed;
        float zoom = state ? -7 : 0;

        //DOVirtual.Float(origChrom, endChrom, .5f, Chromatic);
        //DOVirtual.Float(origFov, endFov, .5f, FieldOfView);
        //DOVirtual.Float(origDistortion, endDistorton, .5f, DistortionAmount);
        //var pvel = stars.velocityOverLifetime;
        //pvel.z = starsVel;

        //DOVirtual.Float(dolly.m_Speed, speed, .15f, SetSpeed);
        SetCameraZoom(zoom, .4f);
    }

    void Break(bool state)
    {
        float speed = state ? forwardSpeed / 3 : forwardSpeed;
        float zoom = state ? 3 : 0;
        //DOVirtual.Float(dolly.m_Speed, speed, .15f, SetSpeed);
        SetCameraZoom(zoom, .4f);
    }
}
