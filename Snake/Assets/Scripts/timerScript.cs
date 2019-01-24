using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;

public class timerScript : MonoBehaviour {
    public float maxValue = 135.0f;
    public float minValue = 0f;
    public float startTime;
    private  float timer;
    private RectTransform rt;
    public delegate void gameOver();
    public static event gameOver onTimerExpired;
	private bool stopTime;

    //GameObject[] num = new GameObject[25];

    public void AddToTime(float addedTime)
    {
        timer += addedTime;
        //var objs = new[] { 0, 5, 3 }; // 0 5 3
        //var timerScriptArr = objs.OrderBy(number => Random.Range(0, 100)).ToArray();

        

    }

    /*GameObject SelectGameObjectFromNum(int number)
    {
        return num[number];
    }*/

	// Use this for initialization
	void Start () {
        timer = startTime;
		stopTime = false;
        rt = GetComponent<RectTransform>();

    }

	public void StopTime(){
		stopTime = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer >= startTime)
        {
            timer = startTime;
        }
		if (timer > 0)
        {
			if(!stopTime){
           		timer -= Time.deltaTime;
			}
        }
        else
        {
            timer = 0;
            if (onTimerExpired != null)
            {
                onTimerExpired();
            }
            Destroy(transform.gameObject);
        }
        rt.offsetMin = new Vector2(maxValue - maxValue * (timer/startTime), rt.offsetMin.y);

    }
}
