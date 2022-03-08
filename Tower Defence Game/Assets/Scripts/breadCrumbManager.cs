using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breadCrumbManager : MonoBehaviour
{
    public GameObject breadCrumb;
    public void spawnBreadcrumb()
    {
        Instantiate(breadCrumb, transform);
        transform.GetChild(transform.childCount - 1).name = $"breadcrumb{transform.childCount - 1}";
    }
}
