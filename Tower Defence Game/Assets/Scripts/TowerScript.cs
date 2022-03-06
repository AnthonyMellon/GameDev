using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    private float aimRange = 1.5f; //How far the tower can reach (radius of a circle)
    private float power = 1; //Amount of damage points per shot
    private float speed = 10; //Amount of shots per second    
    private float punchThrough = 1; //Layers of armour a shot can destroy before being destroyed
    private float shotRange = 1.5f; //How far a shot can reach

    private bool readyToShoot = true;
    private float timeSinceLastShot;

    private LineRenderer lineRenderer;
    private CircleCollider2D targetCircle;

    private List<GameObject> shipsInRange = new List<GameObject>();

    private void OnEnable()
    {
        targetCircle = transform.GetComponent<CircleCollider2D>();
        targetCircle.radius = aimRange;
    }

    public void towerMainFunction() //To be called every update
    {
        if(readyToShoot == false)
        {
            reload();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        shipsInRange.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        shipsInRange.Remove(collision.gameObject);
    }

    public void aim()
    {
        if(shipsInRange.Count > 0)
        {
            float rotation = 0;
            Vector2 target = shipsInRange[0].transform.position;
            rotation = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg - 90;

            transform.eulerAngles = transform.forward * rotation;
        }

    }

    public void shoot()
    {
        if(shipsInRange.Count > 0)
        {
            ShipScript currentShipScript = shipsInRange[0].transform.GetComponent<ShipScript>();
            currentShipScript.reduceHealth(power);
            timeSinceLastShot = 0;
            readyToShoot = false;
        }

    }

    private void reload()
    {
        //Check if the tower is able to shoot
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot > 1/speed)
        {
            readyToShoot = true;
        }
    }

    public void drawTargetArea()
    {
        lineRenderer = transform.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 360;
        for(int i = 0; i < lineRenderer.positionCount; i++)
        {
            float x = (aimRange * Mathf.Sin(i * Mathf.Deg2Rad)) + transform.position.x;
            float y = (aimRange * Mathf.Cos(i * Mathf.Deg2Rad)) + transform.position.y;
            float z = 0;

            lineRenderer.SetPosition(i, new Vector3(x, y, z));
        }                
    }

    public void removeTargetArea()
    {
        lineRenderer = transform.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    public float getTargetRadius()
    {
        return aimRange;
    }

    public bool isReadyToShoot()
    {
        return readyToShoot;
    }
}
