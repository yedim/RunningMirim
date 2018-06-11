using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    
    //캐릭터 애니메이션 제어
    private Animator myAnimator;

    //점프
    public bool grounded;
    public LayerMask whatIsGround; //땅이랑 닿아있는지 확인하게 해줌 list
    public GameObject Ground;
    public int jumpCnt=0;

    //인사
    public static bool greet;

    //파워(체육복)
    public static bool power;
    private float powerTime;

    //보너스(사복)
    public static bool bonus;
    public float bonusTime;
    public static bool realBonus;//이동시간 제외
    private float realBonusTime;//이동시간 제외

    //게임컨트롤러 접근 위한 선언
    public GameController GC;

    //trigger 제어
    public static bool isTriggerOn;

    // Use this for initialization
    void Start () {
        myAnimator = GetComponent<Animator>();
        grounded = true;
        powerTime = 7.0f;
        bonusTime = 12.0f;
        realBonusTime = 10.0f;
        isTriggerOn = true;
        jumpCnt = 0;
    }
	
	// Update is called once per frame
	void Update () {
        //점프하면서 인사했을때 인사되게
        if (!greet) grounded = Physics2D.IsTouchingLayers(GetComponent<Collider2D>(), whatIsGround);
        else grounded = true;

        //땅에 닿아있으면 점프 카운트 초기화
        if (grounded) jumpCnt = 0;

        if (power)//일정시간 지나면 파워 풀림
        {
            powerTime -= Time.deltaTime;
            if (powerTime < 0){
                power = false;
                powerTime = 7.0f;
            }
        }

        if (bonus)//일정시간 지나면 보너스 풀림
        {
            if (bonusTime > 11.5f) MoveUp();
            bonusTime -= Time.deltaTime;
            if (bonusTime < 0)
            {
                bonus = false;
                bonusTime = 12.0f;
            }
            else if (bonusTime > 11 && bonusTime < 11.5f)
            {
                realBonus = true;
                Ground.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("None") as Sprite;
                Ground.transform.position = new Vector3(Ground.transform.position.x, 5.45f, Ground.transform.position.z);
            }
            else if (!realBonus)
            {
                MoveDown();
            }
        }

        if (realBonus)
        {
            realBonusTime -= Time.deltaTime;
            if (realBonusTime < 0)
            {
                realBonus = false;
                realBonusTime = 10.0f;

                for (int j = 0; j < 10; j++)
                {
                    GC.helloWorldLetter[j].gameObject.SetActive(false);
                    GC.helloJelly[j] = false;
                }
            }
        }

        for (int i = 0; i < 10; i++)
        {
            if (GC.helloJelly[i] && i == 9)
            {
                bonus = true;
            }
            else if (!GC.helloJelly[i]) break;
        }

        myAnimator.SetBool("Grounded", grounded);
        myAnimator.SetBool("Greet", greet);
        myAnimator.SetBool("Power", power);
        myAnimator.SetBool("Bonus", bonus);
    }

    public void JumpBtn()
    {
        jumpCnt += 1;// 점프횟수 세기
        if (jumpCnt < 2)// 점프 연속으로 두번까지만 할 수 있게
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 320f);
        }
        else jumpCnt = 2;
    }

    public void GreetBtnDown()//인사버튼눌렀을때
    {
        greet = true;
    }

    public void GreetBtnUp()//인사버튼안누를때
    {
        greet = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isTriggerOn == true)
        {
            if (col.transform.tag.Equals("powerup"))
            {
                Destroy(col.gameObject);
                power = true;
            }
            //power모드일때는 장애물무시
            else if (col.transform.tag.Equals("DamageBox") && power == false && bonus == false)
            {
                GameController.isDanger = true;
                //점수깎이기
                HPManager.time -= 30;
            }
            else if (col.transform.tag.Equals("Jelly"))
            {
                GC.jellyScore += 500;
                Destroy(col.gameObject);
            }
            else if (col.transform.tag.Equals("BigJelly"))
            {
                GC.jellyScore += 700;
                Destroy(col.gameObject);
            }
            else if (col.transform.tag.Equals("H")) { Destroy(col.gameObject); GC.helloWorldLetter[0].gameObject.SetActive(true); GC.helloJelly[0] = true; }
            else if (col.transform.tag.Equals("E")) { Destroy(col.gameObject); GC.helloWorldLetter[1].gameObject.SetActive(true); GC.helloJelly[1] = true; }
            else if (col.transform.tag.Equals("L_1")) { Destroy(col.gameObject); GC.helloWorldLetter[2].gameObject.SetActive(true); GC.helloJelly[2] = true; }
            else if (col.transform.tag.Equals("L_2")) { Destroy(col.gameObject); GC.helloWorldLetter[3].gameObject.SetActive(true); GC.helloJelly[3] = true; }
            else if (col.transform.tag.Equals("O_1")) { Destroy(col.gameObject); GC.helloWorldLetter[4].gameObject.SetActive(true); GC.helloJelly[4] = true; }
            else if (col.transform.tag.Equals("W")) { Destroy(col.gameObject); GC.helloWorldLetter[5].gameObject.SetActive(true); GC.helloJelly[5] = true; }
            else if (col.transform.tag.Equals("O_2")) { Destroy(col.gameObject); GC.helloWorldLetter[6].gameObject.SetActive(true); GC.helloJelly[6] = true; }
            else if (col.transform.tag.Equals("R")) { Destroy(col.gameObject); GC.helloWorldLetter[7].gameObject.SetActive(true); GC.helloJelly[7] = true; }
            else if (col.transform.tag.Equals("L_3")) { Destroy(col.gameObject); GC.helloWorldLetter[8].gameObject.SetActive(true); GC.helloJelly[8] = true; }
            else if (col.transform.tag.Equals("D")) { Destroy(col.gameObject); GC.helloWorldLetter[9].gameObject.SetActive(true); GC.helloJelly[9] = true; }
        }
    }

    private void MoveUp()
    {
        transform.Translate(Vector2.up * 0.5f);
    }

    private void MoveDown()
    {   
        Ground.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ground") as Sprite;
        Ground.transform.position = new Vector3(Ground.transform.position.x, -4.55f, Ground.transform.position.z);
    }
}
