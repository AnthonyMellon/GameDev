using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helperFunctions : MonoBehaviour
{
    public static bool inAreaRect(Vector2 position, Vector2 target, float range)
    {
        if (position.x < target.x + range && position.x > target.x - range)
        {
            if (position.y < target.y + range && position.y > target.y - range)
            {
                return true;
            }
        }
        return false;
    }
    public static bool inAreaCircle(Vector2 position, Vector2 target, float radius)
    {
        if(Mathf.Pow((position.x - target.x), 2) + Mathf.Pow((position.x - target.x), 2) <= Mathf.Pow(radius, 2))
        {
            return true;
        }
        return false;
    }

    public static Vector2[] pointsAlongCircle(int resolution, float radius)
    {
        Vector2[] points = new Vector2[resolution];

        for(int i  = 0; i < resolution; i++)
        {
            points[i].x = radius * Mathf.Sin(i * Mathf.Deg2Rad);
            points[i].y = radius * Mathf.Cos(i * Mathf.Deg2Rad);
        }

        return points;
    }

    public static Vector3[] pointsAlongCircle(int resolution, float radius, float zValue, float offsetX, float offsetY)
    {
        Vector3[] points = new Vector3[resolution];

        for (int i = 0; i < resolution; i++)
        {
            points[i].x = (radius * Mathf.Sin(i * Mathf.Deg2Rad)) + offsetX;
            points[i].y = (radius * Mathf.Cos(i * Mathf.Deg2Rad)) + offsetY;
            points[i].z = 0;
        }
        return points;
    }
}
