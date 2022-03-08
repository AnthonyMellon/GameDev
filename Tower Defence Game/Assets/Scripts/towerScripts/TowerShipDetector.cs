using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShipDetector : MonoBehaviour
{
    Transform myParent;
    TowerScript towerScript;

    private void OnEnable()
    {
        myParent = transform.parent;
        towerScript = myParent.GetComponent<TowerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        towerScript.addShip(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {        
        towerScript.removeShip(collision.gameObject);
    }

}
 