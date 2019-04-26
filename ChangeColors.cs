// 단순 Lerp이용
// 알파값변화(수정가능)
public IEnumerator ChangeColor_Simple()
{
    // 색 저장
    MeshRenderer mRenderer = GameObject.Find(/*원하는 오브젝트*/).GetComponent<MeshRenderer>();
    Color currentColor = mRenderer.material.color;                                      // 원하는 Color로 수정가능
    Color wantedColor = new Color(currentColor.r, currentColor.g, currentColor.b, 1);   // 원하는 Color로 수정가능

    // 변하는 속도
    int speed = 2;

    while (mRenderer.material.color.a < 1.0f)
    {
        currentColor = Color.Lerp(currentColor, wantedColor, speed * Time.deltaTime);
        mRenderer.material.color = currentColor;
        if (mRenderer.material.color.a >= 0.98f)
        {
            currentColor.a = 1;
            mRenderer.material.color = currentColor;
        }
        yield return null;
    }
}

// 좀더 세밀한 조정 가능
public IEnumerator ChangeColor_Detail()
{
    // 서서히 변화할 초
    float duration = 1.0f;
    float smoothness = 0.02f;
    float progress = 0.0f;
    float increment;

    increment = smoothness / duration;

    MeshRenderer mRenderer = GameObject.Find(/*원하는 오브젝트*/).GetComponent<MeshRenderer>();

    Color currentColor = mRenderer.material.color;      // 원하는 Color로 수정가능
    Color wantedColor = new Color(1, 1, 1, 1);          // 원하는 Color로 수정가능

    // 점차 색 바꿔라
    while (progress < 1)
    {
        mRenderer.material.color
                = Color.Lerp(mRenderer.material.color, currentColor, progress);
        progress += increment;
        yield return new WaitForSeconds(smoothness);
    }
}

// 알파값 루프로 변화
// 나타났다가 사라졌다가
IEnumerator ChangeAlpha_Loop(GameObject wantedObj)
{
    // 서서히 변화할 초
    float duration = 1.0f;
    float smoothness = 0.02f;
    float progress = 0.0f;
    float increment = 0.0f;
    float waitTime = 0.5f;
    // 오퍼시티
    float objOpecity = 0.0f;

    // 반복
    while (true)
    {
        // 초기화
        progress = 0;
        increment = smoothness / duration;
        while (progress < 1)
        {
            if (wantedObj.GetComponent<MeshRenderer>().material.GetFloat("_Opacity") > 0.5f)
            {
                // 현재 오퍼시티를 저장
                objOpecity = wantedObj.GetComponent<MeshRenderer>().material.GetFloat("_Opacity");
                // 오퍼시티를 점점 낮춘다
                objOpecity -= 0.01f;
                // 낮춘 오퍼시티를 다시 적용해준다
                wantedObj.GetComponent<MeshRenderer>().material.SetFloat("_Opacity", objOpecity);
            }
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }

        // 다시 초기화
        progress = 0;
        while (progress < 1)
        {
            if (wantedObj.GetComponent<MeshRenderer>().material.GetFloat("_Opacity") <= 0.99f)
            {
                // 오퍼시티를 점점 올린다
                objOpecity += 0.01f;
                // 낮춘 오퍼시티를 다시 적용해준다
                wantedObj.GetComponent<MeshRenderer>().material.SetFloat("_Opacity", objOpecity);
            }
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }

        yield return new WaitForSeconds(waitTime);
    }

}