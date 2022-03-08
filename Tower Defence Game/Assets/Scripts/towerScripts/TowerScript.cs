using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour
{
    public float aimRange = 1.5f; //How far the tower can reach (radius of a circle)
    private float power = 1; //Amount of damage points per shot
    private float speed = 5; //Amount of shots per second    
    private float punchThrough = 1; //Layers of armour a shot can destroy before being destroyed
    private int upgradeCost = 25;

    private bool readyToShoot = true;
    private float timeSinceLastShot;

    private LineRenderer lineRenderer;

    private List<GameObject> shipsInRange = new List<GameObject>();

    public GameObject aimCollider;
    private Transform myAimCollider;
    public GameObject placementBlocker;

    private void OnEnable()
    {
        Instantiate(aimCollider, transform);
        myAimCollider = transform.Find("AimCollider(Clone)");
        Instantiate(placementBlocker, transform);
        transform.Find("placementBlocker(Clone)").GetComponent<CircleCollider2D>().radius = transform.GetComponent<CircleCollider2D>().radius * 2;
    }

    public void towerMainFunction() //To be called every update
    {
        if(readyToShoot == false)
        {
            reload();
        }    
        
        if(helperFunctions.inAreaCircle(TowerManager.mousePos, transform.position, 0.3f))
        {
            GameManager.currentUpgradeCost = upgradeCost;
            if (Input.GetMouseButtonDown(0))
            {
                if(GameManager.numCoins >= upgradeCost)
                {
                    GameManager.numCoins -= upgradeCost;
                    upgradeCost += upgradeCost / 4;
                    aimRange += 0.1f;
                    myAimCollider.GetComponent<CircleCollider2D>().radius = aimRange;
                    power += 0.1f;
                    speed += 0.5f;
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                print("Right Click");
                TowerManager.towerPlaceable = true;
                Destroy(gameObject);
            }
        }

    }

    public void addShip(GameObject ship)
    {
        shipsInRange.Add(ship);
    }

    public void removeShip(GameObject ship)
    {
        shipsInRange.Remove(ship);
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
        lineRenderer.SetPositions(helperFunctions.pointsAlongCircle(360, aimRange, 0, transform.position.x, transform.position.y));
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
