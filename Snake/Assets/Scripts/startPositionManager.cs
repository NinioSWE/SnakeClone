using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startPositionManager : MonoBehaviour {
    public GameObject wallPrefab;


	// Use this for initialization
	void Start () {
        transform.GetComponent<SpriteRenderer>().sprite = null;
	}
    public void ChangeToWall()
    {
        Instantiate(wallPrefab,transform.position,Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
