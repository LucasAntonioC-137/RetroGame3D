using Cinemachine;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerMovement2 : MonoBehaviour
{
    [Header("Ship parameters")]
    public float xySpeed = 18;
    public float lookSpeed = 340f;
    public float forwardSpeed = 20;
    public float turnSpeed = 60f;
    public float maxAngle = 50.0f;
    //public CinemachineDollyCart dolly;
    [Header("Others")]
    public Transform cameraParent;
    public int invert = -1;
    private Transform playerModel;
    public Transform aimTarget;
    public CinemachineDollyCart dolly;
    private float originalFov;
    public float currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        playerModel = transform.GetChild(0);
        SetSpeed(forwardSpeed);
        originalFov = Camera.main.fieldOfView;
        //Debug.Log(originalFov.ToString());
    }
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, invert * vertical, 0);
        LocalMove(horizontal, vertical, xySpeed);
        RotationLookPath(horizontal, vertical, lookSpeed);
        HorizontalLean(playerModel, horizontal, 80, .1f);
        currentSpeed = dolly.m_Speed;
        if (Input.GetButtonDown("Fire3"))
            Boost(true);

        if (Input.GetButtonUp("Fire3"))
            Boost(false);

        if (Input.GetButtonDown("Fire4"))
            Break(true);

        if (Input.GetButtonUp("Fire4"))
            Break(false);

    }


    void LocalMove(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, invert * y, 0) * speed * Time.deltaTime;
        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void RotationLookPath(float horizontal, float vertical, float speed)
    {
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(horizontal,invert * vertical, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speed * Time.deltaTime);
    }

    void HorizontalLean(Transform target, float axis, float leanlimit, float lerpTime)
    {
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanlimit, lerpTime));
    }

    void SetSpeed(float forwardSpeed)
    {
        dolly.m_Speed = forwardSpeed;
    }

    void SetCameraZoom(float zoom, float duration)
    {
        cameraParent.DOLocalMove(new Vector3(0, 0, zoom), duration);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, .5f);
        Gizmos.DrawSphere(aimTarget.position, .15f);

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
        PostProcessVolume postProcessVolume = Camera.main.GetComponent<PostProcessVolume>();
        if (postProcessVolume != null)
        {
            postProcessVolume.profile.GetSetting<ChromaticAberration>().intensity.value = x;
        }
    }

    void Boost(bool state)
    {
        if (!state)
        {
            Camera.main.fieldOfView = originalFov;
        }
        float origFov = state ? 50 : 60;
        float endFov = state ? 60 : 60;
        float origChrom = state ? 0 : 1;
        float endChrom = state ? 1 : 0;
        float origDistortion = state ? 0 : -30;
        float endDistorton = state ? -30 : 0;
        float speed = state ? forwardSpeed * 2 : forwardSpeed;
        float zoom = state ? -7 : 0;

        DOVirtual.Float(origChrom, endChrom, .5f, Chromatic);
        DOVirtual.Float(origFov, endFov, 0.5f, FieldOfView);
        DOVirtual.Float(origDistortion, endDistorton, .5f, DistortionAmount);
        DOVirtual.Float(dolly.m_Speed, speed, .15f, SetSpeed);
        SetCameraZoom(zoom, .4f);

        // Reset camera zoom to 0 when boost effect ends
        if (!state)
        {
            SetCameraZoom(0, .4f);
        }
    }

    void Break(bool state)
    {
        float speed = state ? forwardSpeed / 3 : forwardSpeed;
        float zoom = state ? 3 : 0;
        DOVirtual.Float(dolly.m_Speed, speed, .15f, SetSpeed);
        SetCameraZoom(zoom, .4f);
    }
}
