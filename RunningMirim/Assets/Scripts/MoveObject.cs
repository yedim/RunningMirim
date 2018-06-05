using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {

    public float speed;
    public bool isBack = false;


	// Use this for initialization
	void Start () {
        if (!isBack) { Destroy(gameObject, 6f); }
    }
	
	// Update is called once per frame
	void Update () {
        if (Character.power) transform.Translate(Vector3.left * (speed*2.0f));
        else transform.Translate(Vector3.left * speed);
	}

}
