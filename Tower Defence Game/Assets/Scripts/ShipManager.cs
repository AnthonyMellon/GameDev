using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public GameObject shipManager;
    private Transform shipManagerTransform;
    public GameObject ship;
    public GameObject breadCrumbs;
    private Transform breadCrumbsTransform;

    public int shipBaseSpeed;
    public int shipBaseHealth;

    public bool debug;

    public static Vector3[] breadCrumbPositions;    

    // Start is called before the first frame update
    void Start()
    {
        shipManagerTransform = shipManager.transform;
        breadCrumbsTransform = breadCrumbs.transform;

        breadCrumbPositions = getBreadCrumbPositions();
    }

    public void shipManagerMainFunction() //To be called every update
    {
        if (debug == true)
        {
            debugSpawnShips();
        }
    }

    private void FixedUpdate()
    {        
        moveShips();
    }

    public Vector3[] getBreadCrumbPositions() //Return a 2d vector array containing all bread crumb positions
    {        
        int numBreadcrumbs = breadCrumbsTransform.childCount;
        Vector3[] myBreadCrumbPositions = new Vector3[numBreadcrumbs];

        for (int i = 0; i < numBreadcrumbs; i++)
        {
            Transform currentBreadCrumb = breadCrumbsTransform.GetChild(i);
            myBreadCrumbPositions[i] = currentBreadCrumb.position;
        }

        return myBreadCrumbPositions;
    }

    void debugSpawnShips() //Spawn in ships by pressing G
    {
        if (Input.GetKey(KeyCode.G))
        {
            Instantiate(ship, breadCrumbPositions[0], new Quaternion(0, 0, 0, 0), shipManagerTransform);
            transform.GetChild(transform.childCount-1).GetComponent<ShipScript>().setup();
        }
    }

    void moveShips()
    { 
        for(int i = 0; i < shipManagerTransform.childCount; i++)
        {
            Transform currentShip = shipManagerTransform.GetChild(i);
            currentShip.GetComponent<ShipScript>().ShipFunction();            
        }
    }

    public Vector2[] getShipPositions()
    {
        int numShips = shipManagerTransform.childCount;
        Vector2[] ships = new Vector2[numShips];

        for(int i = 0; i < numShips; i++)
        {
            ships[i] = shipManagerTransform.GetChild(i).transform.position;
        }

        return ships;
    }

    public Transform[] getShips()
    {
        int numShips = transform.childCount;
        Transform[] ships = new Transform[numShips];
        for(int i = 0; i < numShips; i++)
        {
            ships[i] = transform.GetChild(i);
        }
        return ships;
    }
}
