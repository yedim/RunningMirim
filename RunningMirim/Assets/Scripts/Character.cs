using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    
    //캐릭터 애니메이션 제어
    private Animator myAnimator;

    //점프
    public bool grounded;
    public LayerMask whatIsGround; //땅이랑 닿아있는지 확인하게 해줌 list
    public int jumpCnt=0;

    //인사
    public static bool greet;

    //파워(체육복)
    public static bool power;
    private float powerTime;

    //보너스(사복)
    public bool bonus;

    //게임컨트롤러 접근 위한 선언
    public GameController GC;

    //trigger 제어
    public static bool isTriggerOn;

    // Use this for initialization
    void Start () {
        myAnimator = GetComponent<Animator>();
        grounded = true;
        powerTime = 7.0f;
        isTriggerOn = true;
    }
	
	// Update is called once per frame
	void Update () {
        grounded = Physics2D.IsTouchingLayers(GetComponent<Collider2D>(), whatIsGround);

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

        myAnimator.SetBool("Grounded", grounded);
        myAnimator.SetBool("Greet", greet);
        myAnimator.SetBool("Power", power);
        myAnimator.SetBool("Bonus", bonus);
    }

    public void JumpBtn()
    {
        jumpCnt++;// 점프횟수 세기
        if (jumpCnt < 2)// 점프 연속으로 두번까지만 할 수 있게
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 320f);
        }
    }

    public void GreetBtnDown()//인사버튼눌렀을때
    {
        if(grounded) greet = true;
    }

    public void GreetBtnUp()//인사버튼안누를때
    {
        greet = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isTriggerOn==true)
        {
            if (col.transform.tag.Equals("powerup"))
            {
                Destroy(col.gameObject);
                power = true;
            }
            //power모드일때는 장애물무시
            else if (col.transform.tag.Equals("DamageBox") && power == false)
            {
                //게임오버
                GC.GameOver();
            }
            else if (col.transform.tag.Equals("Jelly"))
            {
                GC.jellyScore += 300;
                Destroy(col.gameObject);
            }
            else if (col.transform.tag.Equals("H"))   { Destroy(col.gameObject); GC.helloWorldLetter[0].gameObject.SetActive(true); }
            else if (col.transform.tag.Equals("E")) { Destroy(col.gameObject); GC.helloWorldLetter[1].gameObject.SetActive(true); }
            else if (col.transform.tag.Equals("L_1")) { Destroy(col.gameObject); GC.helloWorldLetter[2].gameObject.SetActive(true); }
            else if (col.transform.tag.Equals("L_2")) { Destroy(col.gameObject); GC.helloWorldLetter[3].gameObject.SetActive(true); }
            else if (col.transform.tag.Equals("O_1")) { Destroy(col.gameObject); GC.helloWorldLetter[4].gameObject.SetActive(true); }
            else if (col.transform.tag.Equals("W")) { Destroy(col.gameObject); GC.helloWorldLetter[5].gameObject.SetActive(true); }
            else if (col.transform.tag.Equals("O_2")) { Destroy(col.gameObject); GC.helloWorldLetter[6].gameObject.SetActive(true); }
            else if (col.transform.tag.Equals("R")) { Destroy(col.gameObject); GC.helloWorldLetter[7].gameObject.SetActive(true); }
            else if (col.transform.tag.Equals("L_3")) { Destroy(col.gameObject); GC.helloWorldLetter[8].gameObject.SetActive(true); }
            else if (col.transform.tag.Equals("D")) { Destroy(col.gameObject); GC.helloWorldLetter[9].gameObject.SetActive(true); }

        }
        
    }
}
