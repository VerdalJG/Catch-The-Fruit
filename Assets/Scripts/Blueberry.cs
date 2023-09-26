using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueberry : MonoBehaviour
{
    public static float bottomY = -20f;
    public float blueberrySpeed = 0.05f; //Blueberry falling speed
    public float frequency = 10f;
    public float magnitude = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y < bottomY)
        {
            Destroy(this.gameObject);

            //Get a reference to the ApplePicker component of Main Camera
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            //Call the public AppleDestroyed() method of apScript
            apScript.FruitsDestroyed();
        }
    }

    private void FixedUpdate()
    {
        BlueberryFall(); // Handling physics in Fixed update and calling a function - for organization
    }
    

    private void BlueberryFall() // Lower position of apple manually every fixed step, constant speed, no gravity.
    {
        Vector3 pos = this.transform.position;
        pos.y -= blueberrySpeed;
        this.transform.position = pos + transform.right * (magnitude * (Mathf.Sin(Time.time * frequency))) * Time.deltaTime; 
        //Sinusodial Movement (transform.right (sine curve multiplied by time delta time))
    }

}
