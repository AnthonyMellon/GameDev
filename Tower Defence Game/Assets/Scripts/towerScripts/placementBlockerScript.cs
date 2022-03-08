using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placementBlockerScript : MonoBehaviour
{
    LineRenderer myLineRenderer;
    private const int CIRCLERES = 360;
    private const float CIRCLERAD = 0.2f;
    private void OnEnable()
    {
        myLineRenderer = transform.GetComponent<LineRenderer>();
    }
    private void OnMouseOver()
    {
        TowerManager.towerPlaceable = false;       
    }

    private void OnMouseExit()
    {
        TowerManager.towerPlaceable = true;        
    }

    private void Update()
    {
        if(TowerManager.towerPlaceable)
        {
            myLineRenderer.positionCount = 0;
        }
        else
        {
            myLineRenderer.positionCount = CIRCLERES;
            myLineRenderer.SetPositions(helperFunctions.pointsAlongCircle(CIRCLERES, CIRCLERAD, 0, TowerManager.mousePos.x, TowerManager.mousePos.y));
        }
    }
}
