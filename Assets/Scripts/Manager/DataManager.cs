using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
public class DataManager : MonoBehaviour
{
    [System.Serializable]//데이터직렬화

    public class CollectionData
    {
        public bool[] collect = new bool[5]; // 스테이지 보스가 죽었는지 확인하는 용도
        public int[] Clear = new int[5]; // 스테이지 최종 보스 몇 번 죽였는지
        public int SheetMusic = 3; //악보 조각
        public int puzzle = 0;
        
        public int Language = 0;
        public float Offset = 0;
        
    }

    public CollectionData collectionData;

    [ContextMenu("To Json Data")]
    public void SaveCollectionDataToJson()//데이터 저장
    {
        string jsonData = JsonUtility.ToJson(collectionData, true);
        string path = Path.Combine(Application.dataPath, "collectionData.json");
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")]
    public void LoadCollectionDataToJson()//데이터 로드
    {
        string path = Path.Combine(Application.dataPath, "collectionData.json");
        string jsonData = File.ReadAllText(path);
        collectionData= JsonUtility.FromJson<CollectionData>(jsonData);
    }
    
}
