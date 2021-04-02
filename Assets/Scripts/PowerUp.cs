using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
   
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerUpID = 0;

    [SerializeField]
    private AudioClip _powerUpClip;


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

            AudioSource.PlayClipAtPoint(_powerUpClip, transform.position);
            if (player != null)
            {
                switch(_powerUpID)
                {
                    case 0: 
                        player.ActiveTripleShot();
                        break;
                    case 1:
                        player.ActiveSpeedBoost();
                        break;
                    case 2:
                        player.ActivateShield();
                        break;
                    default:
                        Debug.LogWarning("power up ID not found");
                        break;
                }

                
                
            }
            
            Destroy(this.gameObject);
        }
        
    }
}