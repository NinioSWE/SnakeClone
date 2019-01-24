using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class appleManager : MonoBehaviour
{
    private Tilemap map;
    private Transform mapTransform;
    public GameObject appleObject;
    public int tileMapWidth;
    public int tileMapHeight;
    // private Bounds bounds;
    // Use this for initialization
    void Start()
    {
        map = GetComponentInChildren<Tilemap>();
        mapTransform = map.GetComponent<Transform>();
        map.CompressBounds();
        //Debug.Log(map.size);
        // bounds = new Bounds(new Vector3(Random.Range(-21, 21)*0.5f, Random.Range(-10, 10), 0) * 0.5f, new Vector3(0.25f, 0.25f, 0f));
        //Debug.Log(bounds.center);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnNewApple()
    {
        //skriv klart om du orkar..
        /*int childCount = mapTransform.childCount;
        List<List<Vector3>> viablePositions = new List<List<Vector3>>();
        for (int y = 0; y < 20; y ++)
        {
            for (int x = 0; x < 42; x++)
            {
                viablePositions[y][x] = new Vector3((x-21)* 0.5f+ 0.25f, (y - 10) * 0.5f + 0.25f,0);
            }
        }

        for (int i = 0; i < childCount; i++)
        {
            Transform child = mapTransform.GetChild(i);
             //child.position.y
            viablePositions[(int)((child.position.y -0.25f)*2 +10)].RemoveAt((int)((child.position.x - 0.25f) * 2 + 21));
        }*/
        int childCount = mapTransform.childCount;
        //Debug.Log(map);
        bool tryToFindPlace = true;
        while (tryToFindPlace)
        {
            Bounds bounds = new Bounds(new Vector3(Random.Range(-(tileMapWidth / 2), (tileMapWidth / 2)) * 0.5f + 0.25f, Random.Range(-(tileMapHeight/2), (tileMapHeight / 2)) * 0.5f + 0.25f, 0), new Vector3(0.25f, 0.25f, 0f));
            //Debug.Log(bounds.center);

            bool findaplace = true;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = mapTransform.GetChild(i);
                if (bounds.Contains(child.position))
                {
                    findaplace = false;
                    break;
                }
            }
            if (findaplace)
            {
                GameObject tempApple = Instantiate(appleObject);
                tempApple.transform.SetParent(mapTransform);
                tempApple.transform.position = bounds.center;
                tryToFindPlace = false;
            }
        }
    }
}
