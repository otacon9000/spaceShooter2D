using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
   
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerUpID = 0;

    void Update()
    {
        Move();   
    }

    void Move()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6.0f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                switch(_powerUpID)
                {
                    case 0: 
                        player.ActiveTripleShot();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        Debug.Log("Shield active");
                        break;
                    default:
                        Debug.LogError("power up ID not found");
                        break;
                }
                
            }
            Destroy(this.gameObject);
        }
        
    }
}