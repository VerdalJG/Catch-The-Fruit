using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePicker : MonoBehaviour
{
    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;

    // Start is called before the first frame update
    void Start()
    {
        basketList = new List<GameObject>();
        for (int i=0; i<numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate(basketPrefab) as GameObject;
            
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void FruitsDestroyed()
    {
        ////Destroy all of the falling apples
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tGOA in tAppleArray)
        {
            Destroy(tGOA);
        }

        ////Destroy all of the falling oranges
        GameObject[] tOrangeArray = GameObject.FindGameObjectsWithTag("Orange");
        foreach (GameObject tGOO in tOrangeArray)
        {
            Destroy(tGOO);
        }

        ////Destroy all of the falling blueberries
        GameObject[] tBlueberryArray = GameObject.FindGameObjectsWithTag("Blueberry");
        foreach (GameObject tGOB in tBlueberryArray)
        {
            Destroy(tGOB);
        }

        ////Destroy all of the falling extra lives
        GameObject[] tLifeArray = GameObject.FindGameObjectsWithTag("Life");
        foreach (GameObject tGOL in tLifeArray)
        {
            Destroy(tGOL);
        }

        LifeLost();
    }


    public void LifeLost()
    {
         ////Destroy one of the Baskets
         //Get the index of the last Basket in basketList
         int basketIndex = basketList.Count - 1;
         //Get a reference to that Basket GameObject
         GameObject tBasketGO = basketList[basketIndex];
         //Remove the Basket from the List and destroy the GameObject
         basketList.RemoveAt(basketIndex);
         Destroy(tBasketGO);

         Basket basketScript = tBasketGO.GetComponent<Basket>(); // Access Basket (script), call SpeedChange function
         basketScript.SpeedChange();
        

        //Restart the game, which doesn´t affect HighScore.Score
        if (basketList.Count == 0)
         {
              Application.LoadLevel("Scene0");
         }
    }

    public void LifeGained()
    {
        int livesLeft = basketList.Count;

        if (livesLeft < 4)
        {
            GameObject tbasketGO = Instantiate(basketPrefab) as GameObject;
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * livesLeft);
            tbasketGO.transform.position = pos;
            basketList.Add(tbasketGO);

        } 
    }
}
