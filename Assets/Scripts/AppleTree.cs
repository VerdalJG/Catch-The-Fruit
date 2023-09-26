using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    //Prefab for initializing apples
    public GameObject applePrefab;
    public GameObject orangePrefab;
    public GameObject blueberryPrefab;
    public GameObject lifePrefab;
    public GameObject powerUpPrefab;
    public GameObject powerDownPrefab;
    public GameObject timertree;
    public Timer timer;

    //Speed at which the apple tree moves in meters/second
    public float speed = 1f;

    //Distance where apple tree turns around
    public float leftAndRightEdge = 10f;

    //Chance that the apple tree will change direction
    public float chanceChangeDirection = 0.1f;

    //Rate at which the apples will be instantiated
    public float secondsBetweenAppleDrops = 1f;

    //Rate at which the lives will have a chance at being instantiated
    public float secondsBetweenLifeDrops = 1f;

    public float secondsBetweenPowerUpDrops = 3f;

    public bool L2start = true;

    public bool L3start = true;


    void Start()
    {
        Timer timer = timertree.GetComponent<Timer>();
        if (timer.timerGame == 0)
        {
            InvokeRepeating("DropFruits", 2f, secondsBetweenAppleDrops);

        }

        //Call drop lives every second after 15 seconds have passed
        InvokeRepeating("DropLives", 15f, secondsBetweenLifeDrops);
        InvokeRepeating("DropPowerup", 15.5f, secondsBetweenPowerUpDrops);
    }
    
    void DropFruits()
    {
            GameObject apple = Instantiate(applePrefab) as GameObject;
            apple.transform.position = transform.position;
    }

    void DropFruitsL2()
    {
        float fruitNumber = Random.Range(0f, 1f);
        if (fruitNumber > 0.7)
        {
            GameObject orange = Instantiate(orangePrefab) as GameObject;
            orange.transform.position = transform.position;
        }

        else if (fruitNumber <= 0.7)
        {
            GameObject apple = Instantiate(applePrefab) as GameObject;
            apple.transform.position = transform.position;
        }

    }

    void DropFruitsL3()
    {
        float fruitNumber = Random.Range(0f, 1f);
        if (fruitNumber > 0.7 && fruitNumber <=0.9)
        {
            GameObject orange = Instantiate(orangePrefab) as GameObject;
            orange.transform.position = transform.position;
        }

        else if (fruitNumber <= 0.7)
        {
            GameObject apple = Instantiate(applePrefab) as GameObject;
            apple.transform.position = transform.position;
        }

        else if (fruitNumber > 0.9)
        {
            GameObject blueberry = Instantiate(blueberryPrefab) as GameObject;
            blueberry.transform.position = transform.position;
        }
    }

    void Update()
    {
        Timer timer = timertree.GetComponent<Timer>();

        ////Level up in terms of difficulty. Cancel invokes used to stop the earlier level functions to avoid double fruit drops. Boolean variables used to control the function.
        if (timer.timerGame > 30 && L2start)
        {
            CancelInvoke();
            InvokeRepeating("DropFruitsL2", 1f, secondsBetweenAppleDrops);
            //Call drop lives every second after 15 seconds have passed
            InvokeRepeating("DropLives", 15f, secondsBetweenLifeDrops);
            //Drop fruits every second after 15.5s
            InvokeRepeating("DropPowerup", 15.5f, secondsBetweenPowerUpDrops);
            L2start = false;
        }
        ;
        if (timer.timerGame > 60 && L3start)
        {
            CancelInvoke(); 
            InvokeRepeating("DropFruitsL3", 1f, secondsBetweenAppleDrops);
            //Call drop lives every second after 15 seconds have passed
            InvokeRepeating("DropLives", 15f, secondsBetweenLifeDrops);
            InvokeRepeating("DropPowerup", 15.5f, secondsBetweenPowerUpDrops);
            L3start = false;
        }

        //Basic Movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        //Changing direction
        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed); //Move Right
        }
        else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed); //Move Left
        }
        else if (Random.value < chanceChangeDirection)
        {
            speed *= -1; //Change direction
        }
    }


    private void FixedUpdate()
    {
        if (Random.value < chanceChangeDirection)
        {
            speed *= -1; //Change direction
        }
    }

    private void DropLives()
    {
        if (Random.value > 0.93)
        {
            GameObject life = Instantiate(lifePrefab) as GameObject;
            life.transform.position = transform.position;
        }
    }

    private void DropPowerup()
    {
        float powerDrop = Random.value;
        if (powerDrop > 0.7 && powerDrop <= 0.9) // 20% chance power up
        {
            GameObject powerUp = Instantiate(powerUpPrefab) as GameObject;
            powerUp.transform.position = transform.position;
        }

        else if (powerDrop > 0.9) // 10% chance power down
        {
            GameObject powerDown = Instantiate(powerDownPrefab) as GameObject;
            powerDown.transform.position = transform.position;
        }
    }
}
