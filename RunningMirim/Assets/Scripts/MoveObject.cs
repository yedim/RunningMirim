using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{

    public float speed;
    public bool isBack = false;
    public bool isBonusItem = false;

    // Use this for initialization
    void Start()
    {
        if (!isBack)
        {
            if (isBonusItem) Destroy(gameObject, 8f);
            else Destroy(gameObject, 6f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Character.bonus || isBonusItem)//보너스 일때 아래 세상은 다 멈추게...
        {
            if (!GameController.isGamePlaying) { }
            else
            {
                if (Character.power) transform.Translate(Vector3.left * (speed * 2.0f));
                else transform.Translate(Vector3.left * speed);
            }

        }
    }
}
