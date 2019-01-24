using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalPositionManager : MonoBehaviour {
    public GameObject wallPrefab;
    private GameObject ourWallPrefab;
    // Use this for initialization
    void Start () {
        transform.GetComponent<SpriteRenderer>().sprite = null;
        ourWallPrefab = Instantiate(wallPrefab,transform.position,Quaternion.identity);
        ourWallPrefab.transform.SetParent(transform.parent);
    }

    public void OpenGoal()
    {
        Destroy(ourWallPrefab.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
