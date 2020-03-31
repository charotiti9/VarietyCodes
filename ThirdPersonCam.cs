using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    #region 전역변수
    public Transform player;

    [Header("카메라 피봇")]
    public Vector3 pivot = new Vector3(0.0f, 1.0f, 0.0f);
    [Header("카메라 상대거리")]
    public Vector3 camOffset = new Vector3(0.4f, 0.5f, -2.0f);

    [Header("회전 속도")]
    public float horizontalSpeed = 6f;
    public float verticalSpeed = 6f;
    [Header("이동 속도")]
    public float moveSpeed = 30;

    [Header("Vertical 각도")]
    public float maxVerticalAngle = 30f;
    public float minVerticalAngle = -60f;

    // 마우스 움직임에 따른 카메라 앵글 저장
    private float angleH = 0;
    private float angleV = 0;

    // 카메라 진로 방향
    Quaternion camHorizontalRot;
    Quaternion camRot;

    // 에이밍
    [Header("Zoom 속도")]
    public float smooth = 10f;
    // Fire2 클릭시 true;
    private bool aim;
    // Fire2 클릭시 늘어날 값
    private int zoom = 30;
    // Fire2 클릭시 줄어들 값
    private int normal = 60;
    #endregion

    void Awake()
    {
        // 카메라의 디폴트 위치를 지정
        // 카메라 움직임 = 플레이어의 위치 + 피봇 + 플레이어와의 카메라 상대거리 좌표
        transform.position = player.position + pivot + camOffset;
        // 카메라 회전 = 회전없음
        transform.rotation = Quaternion.identity;
    }

    void LateUpdate()
    {
        // 기본 움직임
        Moving();

        // 카메라 Zoom
        Zoom();
    }

    private void Moving()
    {
        // 마구 돌아가지 않도록 Clamp
        angleH += Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1) * horizontalSpeed;
        angleV += Mathf.Clamp(Input.GetAxis("Mouse Y"), -1, 1) * verticalSpeed;
        // 각도 한계 세팅
        angleV = Mathf.Clamp(angleV, minVerticalAngle, maxVerticalAngle);
        if(angleH >= 360 || angleH <= -360)
            angleH = 0;

        // Y축을 기준으로, Horizontal의 입력을 받아라
        camHorizontalRot = Quaternion.Euler(0, angleH, 0);
        // Horizontal과 Vertical의 입력을 받아라
        camRot = Quaternion.Euler(-angleV, angleH, 0);

        // Angle: Mouse X, Mouse Y
        transform.rotation = camRot;
        // Orbit: 플레이어 위치 + (y축회전 * 피봇) + (angle * 거리)
        transform.position = player.position + (camHorizontalRot * pivot) + (camRot * camOffset);
        //transform.position = Vector3.Lerp(transform.position, player.position + camHorizontalRot * pivot + camRot * camOffset, moveSpeed * Time.deltaTime);
    }

    private void Zoom()
    {
        if (Input.GetButtonDown("Fire2"))
            aim = true;
        if (Input.GetButtonUp("Fire2"))
            aim = false;

        if (aim)
        {
            // true인 경우 zoom을 한다
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoom, Time.deltaTime * smooth);

            // 플레이어가 카메라 방향을 바라보게 하자
            player.transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
        }
        else
        {
            // false인 경우 다시 원래 값으로 돌아온다
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, normal, Time.deltaTime * smooth);
        }
    }
}
