using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {

    public Transform player;
    public float speed;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.Lerp(transform.position, player.position, Time.deltaTime * speed);
	}
}
