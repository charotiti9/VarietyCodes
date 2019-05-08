/// <summary>
/// 레이어로 오브젝트들을 탐색한다.
/// (현재는 UI 레이어 검색 시 사용중)
/// </summary>
/// <param name="layer">검색할 레이어</param>
/// <returns></returns>
GameObject[] FindGameObjectsWithLayer(int layer)
{
    GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
    List<GameObject> goList = new List<GameObject>();
    for (var i = 0; i< goArray.Length; i++) 
    {
        if (goArray[i].layer == layer) 
        {
            goList.Add(goArray[i]);
        }
    }
    if (goList.Count == 0) 
        return null;
    return goList.ToArray();
}
