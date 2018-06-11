using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Character thePlayer;
    private float Y; //Vector3는 x,y,z 좌표.
    private float distanceToMove;

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<Character>();// Player 위치값 알아내기
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Character.bonus)
        {
            Y = thePlayer.transform.position.y;
            if (Y > 6.24) Y = 6.24f;
            transform.position = new Vector3(0f, Y + 3.76f, transform.position.z);
            if (Character.realBonus)
            {
                transform.position = new Vector3(0f, 10f, transform.position.z);
            }
        }
        else transform.position = new Vector3(0f, 0f, transform.position.z);
    }
}
