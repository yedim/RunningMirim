using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public List<GameObject> box;//장애물종류
    public List<GameObject> jellyLetter;//젤리글자 종류
    public List<GameObject> cloud;//보너스 배경구름
    public List<GameObject> teacher;//선생님
    public List<GameObject> bonusJelly;//보너스 젤리

    public int backgroundNum; //배경(현재 맵)

    public GameObject powerItem;//파워업아이템
    public GameObject loveItem;//생명업아이템
    public GameObject JellyPrefab, JellyMakePosition;//아마스빈 젤리

    //장애물사이(최소 150 이상(거리단위는 아니지만...))
    public int distanceBetween;
    //파워사이
    public int powerDistanceBetween;
    //선생님
    public int tchDistanceBetween;
    //젤리글자
    public int jellyLetterDistanceBetween;
    //구름
    public int cloudEistanceBetween;
    //보너스 젤리
    public int BonusJDistanceBetween;
    //아이템사이
    public int loveDistanceBetween;


    //랜덤 거리
    public int distanceranBetween;
    public int powerDistanceranBetween;
    public int tchDistanceranBetween;
    public int jellyLetterranBetween;
    public int loveDistanceranBetween;

    //점수
    public Text jellyScoreText;
    int score = 0;
    public int jellyScore = 0;
    public static bool isGamePlaying;


    //죽었을 때 나오는 메뉴
    public GameObject DeathMenu;
    public Text DeathScoreText;

    //helloworld 메뉴
    public List<Image> helloWorldLetter;//helloWorld위에있는 letter종류
    public bool[] helloJelly = new bool[9];

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
    void Start()
    {
        backgroundNum = 1;
        isGamePlaying = true;
        Time.timeScale = 1;
        isDanger = false;

        distanceranBetween = (int)Random.Range(100f, 200f);
        powerDistanceranBetween = (int)Random.Range(1000f, 1500f);
        loveDistanceranBetween = (int)Random.Range(1200f, 1700f);
        tchDistanceranBetween = (int)Random.Range(450f, 700f);
        jellyLetterranBetween = (int)Random.Range(100f, 200f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Character.bonus)
        {
            //생성들도 잠시 멈추게
            distanceBetween--;
            powerDistanceBetween--;
            tchDistanceBetween--;
            jellyLetterDistanceBetween--;
            cloudEistanceBetween++;
            BonusJDistanceBetween++;
            loveDistanceBetween--;
            //구름
            if (cloudEistanceBetween > 100)
            {
                cloudEistanceBetween = 0;
                Instantiate(cloud[Random.Range(0, 4)]);
            }
            if (BonusJDistanceBetween > 30)
            {
                BonusJDistanceBetween = 0;
                //작은 젤리 큰 젤리 3:1 비율 (작은젤리가 더 많이 나오게)
                Instantiate(bonusJelly[Random.Range(0, 4)], new Vector3(9.3f, 6.1f, 0f), Quaternion.identity);
                Instantiate(bonusJelly[Random.Range(0, 4)], new Vector3(9.3f, 8.1f, 0f), Quaternion.identity);
                Instantiate(bonusJelly[Random.Range(0, 4)], new Vector3(9.3f, 10.1f, 0f), Quaternion.identity);
                Instantiate(bonusJelly[Random.Range(0, 4)], new Vector3(9.3f, 12.1f, 0f), Quaternion.identity);
                Instantiate(bonusJelly[Random.Range(0, 4)], new Vector3(9.3f, 14.1f, 0f), Quaternion.identity);
            }
        }
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
                default://급식실
                    bg2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("schoolCafeteria") as Sprite;
                    break;
            }
            //배경 위치옮기고 nowBack은 2로
            bg2.transform.position = new Vector3(27.74f, -0.025f, 2f);
            nowBack = 1;
        }
        if (bg2.transform.position.x < 3.945f && nowBack == 1)
        {
            if (BackCnt % 10 == 0) backgroundNum++;
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
                default://급식실
                    bg1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("schoolCafeteria") as Sprite;
                    break;
            }
            bg1.transform.position = new Vector3(27.74f, -0.025f, 2f);
            nowBack = 2;
        }

        if (isGamePlaying == true)
        {
            distanceBetween++;
            powerDistanceBetween++;
            loveDistanceBetween++;
            tchDistanceBetween++;
            jellyLetterDistanceBetween++;


            //장애물 생성(10분의 1확률 && 이전꺼랑 150이상 차이)
            if (distanceBetween > distanceranBetween)
            {
                distanceBetween = 0;
                int RandomIndex = 0;
                distanceranBetween = (int)Random.Range(100f, 250f);

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
                    default://급식실
                        RandomIndex = Random.Range(11, 15);
                        break;
                }
                Instantiate(box[RandomIndex]);
            }
            //파워아이템생성
            if (powerDistanceBetween > powerDistanceranBetween)
            {
                powerDistanceBetween = 0;
                powerDistanceranBetween = (int)Random.Range(1000f, 1500f);
                Instantiate(powerItem);
            }
            //하트아이템생성
            if (loveDistanceBetween > loveDistanceranBetween)
            {
                loveDistanceBetween = 0;
                loveDistanceranBetween = (int)Random.Range(1200f, 1700f);
                Instantiate(loveItem);
            }
            //선생님생성
            if (tchDistanceBetween > tchDistanceranBetween)
            {
                tchDistanceBetween = 0;
                tchDistanceranBetween = (int)Random.Range(450f, 700f);
                Instantiate(teacher[Random.Range(0, 5)]);
            }
            //젤리글자 생성
            if (jellyLetterDistanceBetween > jellyLetterranBetween)
            {
                jellyLetterDistanceBetween = 0;
                jellyLetterranBetween = (int)Random.Range(100f, 200f);
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
        if (HPManager.time <= 0)
        {
            if (isGamePlaying)
            {
                isGamePlaying = false;
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        DeathMenu.SetActive(true);
        DeathScoreText.text = jellyScoreText.text;
        Time.timeScale = 0f;
        Ranking.InsertRank(jellyScore);
        box.Clear();
    }
}
