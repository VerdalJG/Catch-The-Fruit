using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    public static float bottomY = -20f;
    public float lifeSpeed = 0.35f; //Apple falling speed

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
        }
    }

    private void FixedUpdate()
    {
        LifeFall(); // Handling physics in Fixed update and calling a function - for organization
    }


    private void LifeFall() // Lower position of apple manually every fixed step, constant speed, no gravity.
    {
        Vector3 pos = this.transform.position;
        pos.y -= lifeSpeed;
        this.transform.position = pos;
    }

}
