// 에셋 스토어의 베지어커브가 오류가 있으므로
// 고쳐서 쓰려고 만든 코드
public class BezierSolved : MonoBehaviour
{

    BezierCurve bc;
    float t = 0;
    Transform player;
    Transform particle;


    void Start()
    {
        bc = transform.GetComponent<BezierCurve>();
        player = GameObject.Find("Player").transform;
        particle = GameObject.Find("RoadGuideParticle").transform;

        // 플레이어와 가까울때만 플레이 되어야함 ==> 플레이어와 오브젝트 사이의 거리가 15 미만일 때
        if (Vector3.Distance(player.position, particle.position) < 15f)
        {
            StartCoroutine("BezierTest");
        }

    }

    // 끝까지 갔을 때 멈춰야 함 ==> t(퍼센트가) 1보다 작을 때 실행
    public IEnumerator BezierTest()
    {
        // 퍼센트
        float t = 0;
        // 퍼센트가 1이 되기 전까지 실행
        while (t < 0.99f)
        {
            // 움직일 오브젝트의 포지션 = 베지어 곡선의. GetPointAt(퍼센트);
            particle.position = bc.GetPointAt(t);
            // 오류를 줄이기 위해 이동 후 퍼센트를 더해준다
            t += 0.01f;
            yield return null;
        }
    }
}
