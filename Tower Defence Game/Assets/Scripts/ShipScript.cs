using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    private int targetBreadcrumb = 0;    
    private Vector3 targetPosition;

    private float speed = 0.1f; //How fast the ship moves
    private float health = 1; //How many hitpoints the ship has
    private int armourLayers = 1; //The number of armour layers a ship has

    private GameObject audioManager;
    private AudioSource audioSource;
    public AudioClip deathSound;

    private SpriteRenderer spriteRenderer;

    public void move() //Calculate ships movement and then move the ship
    {
        //Move the ship
        transform.Translate(0.1f, 0, 0);

        //Check if the ship has reached a breadcrumb
        targetPosition = ShipManager.breadCrumbPositions[targetBreadcrumb];
        if (transform.position.x > targetPosition.x - 0.1 && transform.position.x < targetPosition.x + 0.1)
        {
            if (transform.position.y > targetPosition.y - 0.1 && transform.position.y < targetPosition.y + 0.1)
            {
                transform.position.Set(targetPosition.x, targetPosition.y, 0);
                targetBreadcrumb++;
                if(targetBreadcrumb < ShipManager.breadCrumbPositions.Length)
                {
                    rotate(); //Rotate the ship to face the new breadcrumb
                }
                
            }

        }        
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
        updateColour();
        audioSource.PlayOneShot(deathSound, 0.5f);
        GameManager.numCoins += 1;
    }

    private void updateColour()
    {        
        spriteRenderer.color = new Color(health / 10, 1 - (health / 10), 0);
    }

    public void checkDespawn() //Check to see if the ship should despawn
    {
        if(targetBreadcrumb >= ShipManager.breadCrumbPositions.Length) //End of track
        {
            Destroy(gameObject);
        }

        if(health <= 0) //Death
        {
            Destroy(gameObject);
        }
    }

    public void setup(float health, int armourLayers, float speed)
    {
        this.health = health;
        this.armourLayers = armourLayers;
        this.speed = speed;

        audioManager = GameObject.Find("World/AudioManager");
        audioSource = audioManager.transform.GetComponent<AudioSource>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        updateColour();

    }
}
