using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeMotion : MonoBehaviour
{
    private Vector3 fp;                     // 처음 터치 위치
    private Vector3 lp;                     // 마지막 터치 위치
    private float dragDistance;             // 드래그 기준거리

    void Start()
    {
        // 기준 거리 설정
        dragDistance = Screen.height * 15 / 100;
    }

    void Update()
    {
        // 마우스+터치를 클릭했을때
        if (Input.GetMouseButtonDown(0))
        {
            ButtonDownCheck();
        }
        // 누르고 있을 때
        else if (Input.GetMouseButton(0))
        {

        }
        // 놓았을 때
        else if (Input.GetMouseButtonUp(0))
        {
            WallUp();
        }


    }

    /// <summary>
    /// 클릭했을 때 클릭된 것이 무엇인지 판별
    /// </summary>
    void ButtonDownCheck()
    {
        // 레이를 쏜다.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        // 무언가 잡았다면
        if (Physics.Raycast(ray, out hitInfo, 100/*, 검사하고 싶은 레이어*/))
        {
            // 처리...(함수호출)
        }
        //  hit 물체가 아무것도 아니라면 스와이프 시작
        else
        {
            // 처음위치 저장
            fp = Input.mousePosition;
        }

    }

    /// <summary>
    /// 벽을 놓을 때
    /// </summary>
    void WallUp()
    {
        lp = Input.mousePosition;
        SwipeCheck();
    }

    /// <summary>
    /// 스와이프했다면 들어가는 함수
    /// </summary>
    void SwipeCheck()
    {
        if(Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
        {
            // 세로 가로 체크
            if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
            {
                if ((lp.x > fp.x))
                {   // 오른쪽
                    Debug.Log("Right Swipe");
                }
                else
                {   // 왼쪽
                    Debug.Log("Left Swipe");
                }
            }
            else
            {
                if (lp.y > fp.y)
                {   // 위
                    Debug.Log("Up Swipe");
                }
                else
                {   // 아래
                    Debug.Log("Down Swipe");
                }
            }
        }
        else
        {
            // 탭
            Debug.Log("Tap");
        }
    }
}
