using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
public class Gacha : MonoBehaviour
{
    [SerializeField]
    private List<GachaData> gachaDataList = new List<GachaData>();
    
    ///<sumarry>
    ///해당 게임 오브젝트가 활성화 될경우 자동으로 확률표를 읽는 방식
    ///</sumarry>
    private void OnEnable()
    {
        //확률 데이터 초기화
        gachaDataList.RemoveRange(0, gachaDataList.Count);
        //확률표 읽기
        ReadData();
    }

    private void ReadData()
    {
        string path;
#if UNITY_EDITOR
        //실제 경로 == Assets/Data/GachaData.txt
        path = Application.dataPath + "/Data/GachaData.txt";
#else
        //실제 경로 == C:/Users/사용자이름/AppData/LocalLow/회사이름/프로젝트이름/GachaData.txt
        //또는 그냥 프로그램 폴더 내 GachaData.txt를 읽으려 시도함
        path = Application.persistentDataPath + "/GachaData.txt";
#endif
        //파일 줄마다 읽기
        using (var reader = new StreamReader(path, Encoding.UTF8))
        {
            while(!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var data = line.Split(',');
                gachaDataList.Add(new GachaData
                {
                    itemName = data[0],
                    probability = int.Parse(data[1]) / 100
                });
            }
        }
    }
#if UNITY_EDITOR
    //테스트용 함수 (에디터 내에서만 가능)
    [ContextMenu("TestPick")]
    private void TestPick()
    {
        var result = Pick();
        Debug.Log(result.itemName);
    }
#endif
    ///무작위로 아이템 하나 뽑기
    public GachaData Pick()
    {
        float totalProbability = 0f;
        foreach (var data in gachaDataList)
        {
            totalProbability += data.probability;
        }

        float randomValue = Random.Range(0f, totalProbability);
        float cumulativeProbability = 0f;

        foreach (var data in gachaDataList)
        {
            cumulativeProbability += data.probability;
            if (randomValue <= cumulativeProbability)
            {
                //뽑기 결과
                return data;
            }
        }
        
        //오류 방지
        return null;
    }
}
[System.Serializable]
public class GachaData
{
    public string itemName;
    public float probability;
}