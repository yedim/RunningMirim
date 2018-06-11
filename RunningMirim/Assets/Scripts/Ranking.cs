using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour {

	//랭킹 텍스트를 가져올 변수
    public Text Rank;

	// Use this for initialization
	void Start () { //한번만 부르면 되기 때문에 Start로 불러옴
		for(int i=0; i<5;i++)
        {
            Rank.text = "1위 " + PlayerPrefs.GetInt("0") + "\n\n"+
                "2위 " + PlayerPrefs.GetInt("1") + "\n\n" +
                "3위 " + PlayerPrefs.GetInt("2") + "\n\n" +
                "4위 " + PlayerPrefs.GetInt("3") + "\n\n" +
                "5위 " + PlayerPrefs.GetInt("4");
        }
    }

    public static void InsertRank(int jellyScore)
    {
        for (int i = 0; i < 5; i++)
        {
            if (jellyScore > PlayerPrefs.GetInt(i.ToString()))
            {
                for (int j = 4 - i; j > 0; j--)
                {
                    PlayerPrefs.SetInt(j.ToString(), PlayerPrefs.GetInt((j - 1).ToString()));
                    //jellyScore가 1등 기준으로
                    //PlayerPrefs의 key값(j위치의 값 4(5등의 값))을 j-1위치의 값(4등의값)으로 바꾼다.
                }
                PlayerPrefs.SetInt(i.ToString(), jellyScore);
                break;
            }


        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
