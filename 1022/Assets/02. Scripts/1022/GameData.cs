using UnityEngine;

/*
[JSON]
-데이터를 저장하거나 전송할 때 많이 사용되는 경량의 Data 교환형식
{
	"이름: " "홍길동",
	"나이 :" 25,
}
*/

[System.Serializable]  //or 'using System;'

public class PlayerData
{

}

public class GameData
{
	public string playerName;
	public int level;
	public int score;
	public string lastCheckPointId;
}
