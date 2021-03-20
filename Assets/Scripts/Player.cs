using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        //if player position on y is grater then 0 
        //y position = 0 
        //else if position on the y is less than -3.5f
        //y = -3.5f

        if(transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0 ,0);
        }else if (transform.position.y <= -3.5f)
        {
            transform.position = new Vector3(transform.position.x, -3.5f ,0);
        }

        //if player position on x is grater then 9 
        //x position = 9 
        //else if position on the x is less than -9
        //x = -9

        if(transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y ,0);
        }else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f,transform.position.y ,0);
        }
    }
}
