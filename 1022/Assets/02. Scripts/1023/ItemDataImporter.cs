using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using System.IO; //파일 디렉터리 관련

public class ItemDataImporter : EditorWindow
{
    //URL
    public string csvURL = "https://docs.google.com/spreadsheets/d/1wuhz2vNkhrJZx7zd7fwBipz_PaTUL4VL33KTMMKEz8c/export?format=csv";

    //스크립터블 오브젝트를 저장할 경로
    private string savePath = "Assets/Data/Items";

    //메뉴에 이 기능을 등록()
    [MenuItem("Tools/Import Item Data From Google Sheets")]
    public static void ShowWindow()
    {
        //Item CSV Importer 라는 이름의 에디터 창을 생성
        GetWindow(typeof(ItemDataImporter), false, "Item CSV Importer");
    }

    private void OnGUI()
    {
        //라벨
        GUILayout.Label("Google Sheet CSV URL", EditorStyles.boldLabel);

        csvURL = EditorGUILayout.TextField("CSV URL", csvURL);

        if(GUILayout.Button("Download and Generate SO"))
        {
            //코루틴 실행
            EditorCoroutineUtility.StartCoroutineOwnerless(ImportCSV());
        }
    }

    IEnumerator ImportCSV()
    {
        //저장경로가 없으면 새로 만들자
        if(!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        //구글 시트에서 csv텍스트 데이터를 받아오기 위한 요청 생성
        UnityWebRequest www = UnityWebRequest.Get(csvURL);

        //요청을 보내고 완료될 떄 까지 대기
        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Download failed" + www.error);
            yield break;
        }

        string[] lines = www.downloadHandler.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Split(',');

            ItemDataSO item = ScriptableObject.CreateInstance<ItemDataSO>();
            item.ID = int.Parse(values[0]); //ID
            item.itemName = values[1]; //이름
            item.type = values[2]; //타입
            item.power = int.Parse(values[3]); //능력치
            item.description = values[4];

            //에셋파일 경로 지정 (Item_1_검.asset)
            string assetpath = $"{savePath}/Item_{item.ID}_{item.itemName}.asset";

            AssetDatabase.CreateAsset(item, assetpath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("아이템 데이터 ScriptableObject 생성 완료!");
    }
}
