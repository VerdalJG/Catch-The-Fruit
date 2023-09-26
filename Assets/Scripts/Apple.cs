using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public static float bottomY = -20f;
    public float appleSpeed = 0.025f; //Apple falling speed

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
        AppleFall(); // Handling physics in Fixed update and calling a function - for organization
    }


    private void AppleFall() // Lower position of apple manually every fixed step, constant speed, no gravity.
    {
        Vector3 pos = this.transform.position;
        pos.y -= appleSpeed;
        this.transform.position = pos;
    }

}
