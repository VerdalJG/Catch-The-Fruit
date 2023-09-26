using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    [SerializeField] private float basketSpeed = 0.5f; //Speed of basket

    public Text scoreGT;

    public float basketMove; //Variable that works as a boolean, used to manage whether the basket should move or not based on position

    private bool speedChange = false;

    // Start is called before the first frame update
    void Start()
    {
        //Find a reference to the ScoreCounter GameObject
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        //Get the GUIText Component of that GameObject
        scoreGT = scoreGO.GetComponent<Text>();
        //Set the score count to 0 on start
        scoreGT.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();

        if (apScript.basketList.Count >= 3 && speedChange)
        {
            basketSpeed = 1f;
            Debug.Log(basketSpeed);
            speedChange = false;
        }

        else if (apScript.basketList.Count == 2 && speedChange)
        {
            basketSpeed = 2f;
            Debug.Log(basketSpeed);
            speedChange = false;
        }

        else if (apScript.basketList.Count == 1 && speedChange)
        {
            basketSpeed = 0.5f;
            speedChange = false;
        }

        FollowMouse();
    }


    private void OnCollisionEnter(Collision coll)
    {
        //Parse the text of the scoreGT into an int

        int score = int.Parse(scoreGT.text);

        //Find out what hit the basket
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.tag == "Apple")
        {
            Destroy(collidedWith);
            //Add points for catching the apple
            score += 100;
        }
        
        else if (collidedWith.tag == "Orange")
        {
            Destroy(collidedWith);
            //Add points for catching the orange
            score += 300;
        }

        else if (collidedWith.tag == "Blueberry")
        {
            Destroy(collidedWith);
            //Add points for catching the blueberry
            score += 600;
        }

        else if (collidedWith.tag == "Life")
        {
            Destroy(collidedWith);
            //Add points for catching the blueberry
            score += 100;
            
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            apScript.LifeGained();
        }

        else if (collidedWith.tag == "PowerDown")
        {
            Destroy(collidedWith);
            //Subtract points for catching the powerdown
            score -= 300;
            transform.localScale = new Vector3(2, 1, 4);
            Invoke("DisableBoost", 5f);
        }

        else if (collidedWith.tag == "PowerUp")
        {
            Destroy(collidedWith);
            //Add points for catching the powerup
            score += 300;
            transform.localScale = new Vector3(10, 1, 4);
            Invoke("DisableBoost", 5f);
        }

        //Convert the score back to a string and display it
        scoreGT.text = score.ToString();

        //Track the high score
        if (score > HighScore.score)
        {
            HighScore.score = score;
        }
    }

    public void DisableBoost()
    {
        transform.localScale = new Vector3(4, 1, 4);
    }

    public void SpeedChange()
    {
        speedChange = true;
        Debug.Log(speedChange);
    }

    public void FollowMouse()
    {
        //Get the current screen position of the mouse from input
        Vector3 mousePos2D = Input.mousePosition;

        //The camera´s z position sets how far to push the mouse into 3D
        mousePos2D.z = -Camera.main.transform.position.z;

        //Convert the point from 2D screen space into 3D game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 pos = this.transform.position;
        pos.x += basketMove; //Changing x position of basket based on basketMove's value
        transform.position = pos;


        if (Mathf.Abs(mousePos3D.x - transform.position.x) <= 1) //Controls the margin between the mouse and basket to see if it should move.
        {
            basketMove = 0; //If the mouse x is near basket x, set the movement to 0 - don't move.
        }
        else if (Mathf.Abs(mousePos3D.x - transform.position.x) > 1)
        {
            basketMove = basketSpeed; //If the basket is far enough away from the mouse, basketMove is set to basketSpeed's value

            if (mousePos3D.x > transform.position.x && basketSpeed < 0) //Controls left and right direction change
            {
                basketSpeed = (Mathf.Abs(basketSpeed)); //Move right
            }
            else if (mousePos3D.x < transform.position.x && basketSpeed > 0)
            {
                basketSpeed = -(Mathf.Abs(basketSpeed)); //Move left
            }
        }
    }
}
