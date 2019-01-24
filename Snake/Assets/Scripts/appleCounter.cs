using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class appleCounter : MonoBehaviour {
    public string standardText = "Apples Left: ";
    private Text ourText;

	// Use this for initialization
	void Start () {
        ourText = GetComponent<Text>();
        ourText.text = standardText;

    }
    public void UpdateScore(int amoutLeft)
    {
        if (amoutLeft <= 0)
        {
            amoutLeft = 0;
        }
        ourText.text = standardText + amoutLeft;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
