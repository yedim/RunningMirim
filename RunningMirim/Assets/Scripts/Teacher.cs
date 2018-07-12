using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacher : MonoBehaviour {

    //선생님객체마다 감정(발견, 기쁨, 슬픔) 및 이미지이름
    public string state;
    public bool isGreet;
    public int index;

	// Use this for initialization
	void Start () {
        state = "teacher"+index+"_normal";
        isGreet = false;
    }

    private void Update()
    {
        //선생님 상태변화(선생님이 미림이 발견후)
        if (4.7f >= GetComponent<Transform>().transform.position.x && isGreet == false && !Character.bonus)
        {
            if(Character.greet)//인사 했을때(선생님 표정바뀌고 인사처리 isGreet)
            {
                state = "teacher" + index + "_happy";
                isGreet = true;
                GameObject.Find("GreetSound").GetComponent<AudioSource>().Play();
            }
            else
            {
                state = "teacher" + index + "_find";//학생발견(선생님 표정바뀜)
            }
            if (-5.54f >= GetComponent<Transform>().transform.position.x && isGreet == false)
            {
                state = "teacher" + index + "_angry";//인사안하고 지나쳤을때(화난표정)
                isGreet = true;
                if (!Character.power&&!Character.bonus)//파워 상태에는 인사 안해도 상관없게
                {
                    GameController.isDanger = true;
                    //점수깎이기
                    HPManager.time -= 20;
                    GameObject.Find("NotGreetSound").GetComponent<AudioSource>().Play();
                }

            }
        }

        this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(state) as Sprite;
    }
}
