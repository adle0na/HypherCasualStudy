using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils_Pin
{
    public static Vector3 GetPositionFromAngle(float radius, float angle)
    {
        Vector3 position = Vector3.zero;

        angle = DegreeToRadian(angle);

        position.x = Mathf.Cos(angle) * radius;
        position.y = Mathf.Sin(angle) * radius;
        
        return position;
    }

    public static float DegreeToRadian(float angle)
    {
        return Mathf.PI * angle / 180;
    }

    public static float RadianToDegree(float angle)
    {
        return angle * (180 / Mathf.PI);
    }

}
