// 크로스헤어를 끝점에 위치시키는 코드(VR용)
public void Crosshair()
{
    // 변수
    float rayDis = 80.0f;
    float origSize = 1.0f;
    float kSizeAdjust = 0.7f;

    LineRenderer lr = GetComponent<LineRenderer>();
    Transform crosshair;
    Transform pos;
    RaycastHit hitInfo;

    if (Physics.Raycast(ray, out hitInfo, rayDis, ~gameObject.layer))
    {
        // 라인렌더러
        lr.SetPosition(0, pos.position);
        lr.SetPosition(1, hitInfo.point);
        // 색상 다시 제자리로
        crosshair.GetComponent<SpriteRenderer>().color = Color.cyan;
        // 파랑 크로스헤어
        Vector3 dir = Camera.main.transform.position;
        // 크로스헤어가 건물 족굼 앞으로 나온다
        crosshair.position = hitInfo.point + new Vector3(0, 0, -0.1f);
        // 거리에 따라 크기 보정
        crosshair.localScale = origSize * hitInfo.distance * kSizeAdjust;

        if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer(/*원하는 레이어*/))
        {
            // 하얀 크로스헤어
            crosshair.GetComponent<SpriteRenderer>().color = Color.white;
        }

    }
    // 아무데도 닿은데가 없으면?
    else
    {
        // 라인렌더러
        lr.SetPosition(0, pos.position);
        lr.SetPosition(1, ray.GetPoint(rayDis));

        crosshair.GetComponent<SpriteRenderer>().color = Color.cyan;
        // 레이의 끝점에 크로스헤어를 위치시키는 명령
        crosshair.position = ray.GetPoint(rayDis) + new Vector3(0, 0, -0.1f);
        crosshair.localScale = origSize * rayDis * kSizeAdjust;
    }
}