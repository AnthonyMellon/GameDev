using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject towerManagerObject;
    public GameObject shipManagerObject;

    private TowerManager towerManagerScript;
    private ShipManager shipManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        towerManagerScript = towerManagerObject.transform.GetComponent<TowerManager>();
        shipManagerScript = shipManagerObject.transform.GetComponent<ShipManager>();
    }

    void Update()
    {
        towerManagerScript.towerManagerMainFunction();
        towerManagerScript.aimTowers();
    }

    void FixedUpdate()
    {        
        shipManagerScript.shipManagerMainFunction();
        
    }
}
