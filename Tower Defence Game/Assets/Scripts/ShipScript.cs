using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    private int targetBreadcrumb = 0;    
    private Vector3 targetPosition;

    private float speed = 0.1f; //How fast the ship moves
    private float health = 1; //How many hitpoints the ship has
    private float armourLayers = 1; //The number of armour layers a ship has

    private GameObject audioManager;
    private AudioSource audioSource;
    public AudioClip deathSound;

    void move() //Calculate ships movement and then move the ship
    {              
        //Check if the ship has reached a breadcrumb
        targetPosition = ShipManager.breadCrumbPositions[targetBreadcrumb];
        if (transform.position.x > targetPosition.x - speed && transform.position.x < targetPosition.x + speed)
        {
            if (transform.position.y > targetPosition.y - speed && transform.position.y < targetPosition.y + speed)
            {
                transform.position.Set(targetPosition.x, targetPosition.y, 0);
                targetBreadcrumb++;
                if(targetBreadcrumb < ShipManager.breadCrumbPositions.Length)
                {
                    rotate(); //Rotate the ship to face the new breadcrumb
                }
                
            }

        }

        //Move the ship
        transform.Translate(0.1f, 0, 0);
    }

    void rotate() //Calculate ships rotation then rotate the ship
    {
        targetPosition = ShipManager.breadCrumbPositions[targetBreadcrumb];
        float rotation = 0;

        //Calculate the rotation of the ship
        rotation = Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * Mathf.Rad2Deg;       

        //Set the rotation
        transform.eulerAngles = Vector3.forward * rotation;
        
    }

    public void reduceHealth(float damage)
    {
        health -= damage;
    }

    void checkDespawn() //Check to see if the ship should despawn
    {
        if(targetBreadcrumb >= ShipManager.breadCrumbPositions.Length) //End of track
        {
            Destroy(gameObject);
        }

        if(health <= 0) //Death
        {
            audioSource.PlayOneShot(deathSound, 0.5f);
            Destroy(gameObject);
        }
    }

    public void setup()
    {        
        audioManager = GameObject.Find("World/AudioManager");
        audioSource = audioManager.transform.GetComponent<AudioSource>();
    }

    public void ShipFunction() //Main function call of the ship
    {             
        move();
        checkDespawn();
    }

    public Vector2 getShipPosition()
    {
        return transform.position;
    }
}
