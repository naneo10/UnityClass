using System.IO;
using UnityEngine;

public class SaveSystem
{
    private static readonly string SavePath = Path.Combine(Application.persistentDataPath, "save.json"); //(파일경로, 파일이름)

    //데이터를 JSON형식으로 저장
    public static void Save(GameData data, bool prettyPrint = true)
    {
        //객체를 JSON 형식으로 변환 (문자열로 변환)
        string json = JsonUtility.ToJson(data, prettyPrint);

        //문자열을 파일로 저장
        File.WriteAllText(SavePath, json);

        Debug.Log("저장 완료 : " + SavePath);
        Debug.Log(json);
    }

    //JSON 파일을 읽어서 객체로 돌리자
    public static bool TryLoad(out GameData data)
    {
        if (!File.Exists(SavePath))
        {
            data = null;
            Debug.Log("저장 파일이 없다");
            return false;
        }

        string json = File.ReadAllText(SavePath);

        data = JsonUtility.FromJson<GameData>(json);

        Debug.Log("불러오기 성공 : " + SavePath);
        Debug.Log(json);

        return true;
    }
}
