using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shopping : MonoBehaviour {

    public Text coinText;
    public int coinScore;
    public int[] characterPrice;        //가격
    public GameObject coinWarning;      //경고창
    public GameObject alreadyWarning;   //이미 구입됨 경고창
    public Button[] characterBuyButtons;//구입버튼 4개
    public Sprite boughtButtonImg;      //구입됨버튼 이미지

    public Button[] characterSelectButtons;//구입버튼 4개
    public Sprite selectButtonImg;         //선택버튼 이미지
    public Sprite selectedButtonImg;       //선택됨버튼 이미지

    /* PlayerPrefs 지정 값
     * coinScore : 코인의 값
     * buyCharcter0,buyCharcter1,buyCharcter2,buyCharcter3 : 내가 산 캐릭터, 0이면 아직 구매전, 1이면 구매됨.
     * selectCharacter : 내가 선택한 캐릭터. 
     */

    void Start () {
        coinScore = PlayerPrefs.GetInt("coinScore");//코인값 가져오기
        PlayerPrefs.SetInt("buyCharacter0", 1);//춘추복은 기본이라 무조건 구입됨으로 표시
        coinText.text = coinScore.ToString();
        characterPrice = new int[4] { 0, 5000, 20000, 25000 }; 

    
       //내가 산 캐릭터의 버튼은 구입됨 버튼으로 변경
       for(int i=0; i<4; i++)
       {
           if (PlayerPrefs.GetInt("buyCharacter"+i) == 1)
             characterBuyButtons[i].image.sprite = boughtButtonImg;
       }

        //내가 선택한 캐릭터는 선택됨 버튼으로 변경
        characterSelectButtons[PlayerPrefs.GetInt("selectCharacter")].image.sprite = selectedButtonImg; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BuyCharacter(int characterNum)
    {
        if (PlayerPrefs.GetInt("buyCharacter" + characterNum)==1)
        {
            alreadyWarning.SetActive(true);
            return;
        }
        

        if (coinScore < characterPrice[characterNum]) //돈이 부족한 경우
        {
            coinWarning.SetActive(true);
        }
        else //샀을 경우
        {
            coinScore-=characterPrice[characterNum];
            PlayerPrefs.SetInt("coinScore", coinScore); 

            //샀으면 1 안샀으면 0
            if(characterNum >0)
            {
                PlayerPrefs.SetInt("buyCharacter" + characterNum, 1);
                characterBuyButtons[characterNum].image.sprite = boughtButtonImg;
                coinText.text = coinScore.ToString();
            }
        }
    }

    public void SelectCharacter(int characterNum)
    {
        for(int i=0; i<4; i++)//다 선택되지 않은 걸로 초기화
        {
            characterSelectButtons[i].image.sprite = selectButtonImg;
        }
        if(PlayerPrefs.GetInt("buyCharacter" + characterNum) != 0) //버튼 누른게 이미 구매된 것이면 selectCharacter 바꾸기(선택됨)
        {
            PlayerPrefs.SetInt("selectCharacter", characterNum);
        }
        characterSelectButtons[PlayerPrefs.GetInt("selectCharacter")].image.sprite = selectedButtonImg; //선택된 항목에 선택됨 버튼

    }

    public void QuitWarning(string str)
    {
        if (str.Equals("coinWarning"))
            coinWarning.SetActive(false);
        else if (str.Equals("alreadyWarning"))
            alreadyWarning.SetActive(false);
    }

}
