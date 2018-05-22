using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public List<GameObject> box;//장애물종류
    public int backgroundNum; //배경(현재 맵)

    public GameObject powerItem;//파워업아이템

    public List<GameObject> teacher;//선생님

    //장애물사이(최소 150 이상(거리단위는 아니지만...))
    public int distanceBetween;
    //파워사이
    public int powerDistanceBetween;
    //선생님
    public int tchDistanceBetween;

	//점수
    public Text jellyScoreText;
    int score = 0;
	public int jellyScore=0;
    public bool isGamePlaying;

    //아마스빈 젤리
    public GameObject JellyPrefab, JellyMakePosition;

    //죽었을 때 나오는 메뉴
    public GameObject DeathMenu;
    public Text DeathScoreText;

    // Use this for initialization
    void Start () {
        backgroundNum = 1;
        isGamePlaying = true;
        Time.timeScale = 1; 
    }

    // Update is called once per frame
    void Update()
    {
        if (isGamePlaying==true)
        {
            distanceBetween++;
            powerDistanceBetween++;
            tchDistanceBetween++;

            //장애물 생성(10분의 1확률 && 이전꺼랑 150이상 차이)
            if ((int)Random.Range(0f, 100f) < 10 && distanceBetween > 150)
            {
                distanceBetween = 0;
                int RandomIndex = 0;

                switch (backgroundNum)
                {
                    case 1://복도
                        RandomIndex = Random.Range(0, 5);
                        break;
                    case 2://계단
                        RandomIndex = Random.Range(3, 7);
                        break;
                    case 3://밖
                        RandomIndex = Random.Range(7, 11);
                        break;
                    case 4://급식실
                        RandomIndex = Random.Range(11, 15);
                        break;
                }

                Instantiate(box[RandomIndex]);
            }
            //파워아이템생성
            if ((int)Random.Range(0f, 100f) < 10 && powerDistanceBetween > 1000)
            {
                powerDistanceBetween = 0;
                Instantiate(powerItem);
            }
            //선생님생성
            if ((int)Random.Range(0f, 100f) < 10 && tchDistanceBetween > 500)
            {
                tchDistanceBetween = 0;
                Instantiate(teacher[Random.Range(0, 1)]);
            }

            
            score++;//프레임 증가
            if (score % 50 == 0)//젤리 생성
            {
                Instantiate(JellyPrefab, JellyMakePosition.transform.position, Quaternion.identity);
            }
            jellyScoreText.text = jellyScore.ToString();   
        }
    }

    public void GameOver()
    {
        DeathMenu.SetActive(true);
        DeathScoreText.text = jellyScoreText.text;
        Time.timeScale = 0;
        isGamePlaying = false;
    }
}
