using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public GameObject towerManager;
    public GameObject tower;
    public Camera cam;
    public static Vector3 mousePos;

    private const int TOWER_BASE_PRICE = 75;    
    public static int towerCost;
    public static bool towerPlaceable = true;

    public void towerManagerMainFunction() //To be called every update
    {
        towerCost = TOWER_BASE_PRICE + Mathf.FloorToInt(GameManager.totalCoinsSpent / 10); 

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && GameManager.numCoins >= towerCost && towerPlaceable)
        {            
            Instantiate(tower, new Vector3(mousePos.x, mousePos.y, 0), new Quaternion(0, 0, 0, 0), towerManager.transform);
            GameManager.spendCoins(towerCost);
        }

        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<TowerScript>().towerMainFunction();
            
            if (helperFunctions.inAreaCircle(mousePos, transform.GetChild(i).position, 0.3f))
            {
                transform.GetChild(i).GetComponent<TowerScript>().drawTargetArea();
            }
            else
            {
                transform.GetChild(i).GetComponent<TowerScript>().removeTargetArea();
            }
        }
        
    }

    public void aimTowers()
    {
        int numTowers = towerManager.transform.childCount;

        for(int i = 0; i < numTowers; i++)
        {
            TowerScript currentShipScript = transform.GetChild(i).GetComponent<TowerScript>();
            currentShipScript.aim();   
            
            if(currentShipScript.isReadyToShoot())
            {
                currentShipScript.shoot();
            }

        }
    }
}
