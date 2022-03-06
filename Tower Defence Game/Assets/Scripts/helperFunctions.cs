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
    public bool inAreaCircle(Vector2 position, Vector2 target, float range)
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
}
