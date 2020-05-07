using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 데이터 가져올떄 파라메터로 사용할 Enum 값
/// </summary>
public enum PartDataType
{
    /// <summary>
    /// 오브젝트 이름
    /// </summary>
    NAME_OBJ,
    /// <summary>
    /// 해당오브젝트 설명
    /// </summary>
    DESCRIPTION
}


public class CsvExample : MonoBehaviour
{

    [SerializeField]
    /// <summary>
    /// 데이터 CSV 파일
    /// </summary>
    TextAsset csvFile;

    // csv를 나누는 기준
    char lineSeperator = '\n';
    char fieldSeperator = ',';

    /// <summary>
    /// 데이터 class
    /// </summary>
    class CsvData
    {
        /// <summary>
        /// 모델오브젝트 이름
        /// </summary>
        public string nameObj;
        /// <summary>
        /// 해당 오브젝트 설명
        /// </summary>
        public string desc;
    }
    /// <summary>
    /// 데이터가 저장될 dictionary
    /// </summary>
    Dictionary<string, CsvData> dataDic = new Dictionary<string, CsvData>();



    private void Start()
    {
        Csv2Dic();
    }

    void Csv2Dic()
    {
        // 행별로 나눠서 저장
        string[] records = csvFile.text.Split(lineSeperator);

        int lineCount = 0;
        int skipLine = 1; // 스킵하고 싶은 라인Count
                          // 열별로 나눠서 저장
        foreach (string record in records)
        {
            lineCount++;

            if (lineCount <= skipLine) // csv에서 라인 스킵
                continue;

            // value 내 comma 처리 추가
            string[] fields = FieldSplitter(record);

            if (string.IsNullOrEmpty(fields[0]))
                break;

            // csv 변경시 같이 변경해주어야함-----------
            CsvData objData = new CsvData
            {
                nameObj = fields[0],
                desc = fields[1]
            };

            // 정리된 objData를 데이터dic에 최종 Add
            if (dataDic.ContainsKey(fields[0]) == false)
            {
                dataDic.Add(fields[0], objData);
                Debug.Log("Added: " + objData.nameObj);
            }
            else
            {
                Debug.Log("duplicated object name exists");
            }
        }
    }

    /// <summary>
    /// 메인데이터 딕셔너리로부터 string 데이터 가져오기
    /// </summary>
    /// <param name="objName">선택한 오브젝트 이름</param>
    /// <param name="dataName">가져올 데이터종류</param>
    /// <returns></returns>
    public string GetObjData(string objName, PartDataType dataName)
    {
        string data = "";

        if (dataDic.ContainsKey(objName) == false)
        {
            data = "None";
            return data;
        }

        switch (dataName)
        {
            case PartDataType.NAME_OBJ:
                data = dataDic[objName].nameObj;
                break;
            case PartDataType.DESCRIPTION:
                data = dataDic[objName].desc;
                break;
        }

        return data;
    }

    /// <summary>
    /// value내 comma와 일반 comma의 구분을 위한 ""판별, 반환
    /// </summary>
    /// <param name="line">레코드 입력</param>
    /// <returns></returns>
    internal string[] FieldSplitter(string line)
    {
        List<string> fieldsList = new List<string>();

        int fieldStart = 0;
        bool metDoubleQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            if (line[i].Equals('"'))
            {
                for (i++; line[i].Equals('"') == false; i++) { }
                metDoubleQuotes = true;
            }

            if (line[i] == fieldSeperator)
            {
                if (metDoubleQuotes)
                {
                    fieldsList.Add(line.Substring(fieldStart + 1, i - fieldStart - 2));
                    fieldStart = i + 1;
                    metDoubleQuotes = false;
                }
                else
                {
                    fieldsList.Add(line.Substring(fieldStart, i - fieldStart));
                    fieldStart = i + 1;
                }
            }

            if (i == line.Length - 1)
            {

                fieldsList.Add(line.Substring(fieldStart, i - fieldStart + 1));
            }

        }
        return fieldsList.ToArray();
    }
}
