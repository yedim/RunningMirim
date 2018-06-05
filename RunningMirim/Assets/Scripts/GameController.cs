using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public List<GameObject> box;//장애물종류
    public List<GameObject> jellyLetter;//젤리글자 종류

    public int backgroundNum; //배경(현재 맵)

    public GameObject powerItem;//파워업아이템

    public List<GameObject> teacher;//선생님

    //장애물사이(최소 150 이상(거리단위는 아니지만...))
    public int distanceBetween;
    //파워사이
    public int powerDistanceBetween;
    //선생님
    public int tchDistanceBetween;
    //젤리글자
    public int jellyLetterDistanceBetween;

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

    //helloworld 메뉴
    public List<Image> helloWorldLetter;//helloWorld위에있는 letter종류

    //인사안했을때 화면 붉게
    public GameObject DangerBox;
    public static bool isDanger;
    public int danCnt = 0;

    //background
    public GameObject bg1; 
    public GameObject bg2;
    public int nowBack = 2; //현재 옮겨야할 배경이 bg1인지 bg2인지
    public int BackCnt = 1;

    // Use this for initialization
    void Start () {
        backgroundNum = 1;
        isGamePlaying = true;
        Time.timeScale = 1;
        isDanger = false; 
    }

    // Update is called once per frame
    void Update()
    {
        //background
        if (bg1.transform.position.x < 3.945f && nowBack == 2)
        {
            if (BackCnt % 10 == 0) backgroundNum++;
            BackCnt++;
            switch (backgroundNum)
            {
                case 1://복도
                    bg2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("hallway") as Sprite;
                    break;
                case 2://계단
                    bg2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("step") as Sprite;
                    break;
                case 3://밖
                    bg2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("outside") as Sprite;
                    break;
                case 4://급식실
                    bg2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("schoolCafeteria") as Sprite;
                    break;
            }
            //배경 위치옮기고 nowBack은 2로
            bg2.transform.position = new Vector3(27.74f, -0.025f, 2f);
            nowBack = 1;
        }
        if (bg2.transform.position.x < 3.945f && nowBack == 1)
        {
            if (BackCnt % 3 == 0) backgroundNum++;
            BackCnt++;
            switch (backgroundNum)
            {
                case 1://복도
                    bg1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("hallway") as Sprite;
                    break;
                case 2://계단
                    bg1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("step") as Sprite;
                    break;
                case 3://밖
                    bg1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("outside") as Sprite;
                    break;
                case 4://급식실
                    bg1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("schoolCafeteria") as Sprite;
                    break;
            }
            bg1.transform.position = new Vector3(27.74f, -0.025f, 2f);
            nowBack = 2;
        }

        if (isGamePlaying==true)
        {
            distanceBetween++;
            powerDistanceBetween++;
            tchDistanceBetween++;
            jellyLetterDistanceBetween++;


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
            if ((int)Random.Range(0f, 100f) < 10 && powerDistanceBetween > 700)
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
            //젤리글자 생성
            if ((int)Random.Range(0f, 100f) < 10 && jellyLetterDistanceBetween > 100)
            {
                jellyLetterDistanceBetween = 0;
                Instantiate(jellyLetter[Random.Range(0, 10)]);
            }

            score++;//프레임 증가
            if (score % 50 == 0)//젤리 생성
            {
                Instantiate(JellyPrefab, JellyMakePosition.transform.position, Quaternion.identity);
            }
            jellyScoreText.text = jellyScore.ToString();

            //인사안했을때 붉은상자
            if (isDanger)
            {
                danCnt++;
                DangerBox.SetActive(true);
                if (danCnt > 20)
                {
                    DangerBox.SetActive(false);

                    isDanger = false;
                    danCnt = 0;
                }
            }
        }
       

        //time 0보다 작으면 게임오버
        if(HPManager.time<=0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        DeathMenu.SetActive(true);
        DeathScoreText.text = jellyScoreText.text;
        Time.timeScale = 0f;
        isGamePlaying = false;
        Ranking.InsertRank(jellyScore);
        box.Clear();       
    }

   

}
