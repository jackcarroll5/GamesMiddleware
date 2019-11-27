using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vec3Extensions 
{
    public static short[] ToShortXZ(this Vector3 vector)
    {
        var values = new short[2];
        values[0] = (short)(vector.x);
        values[1] = (short)(vector.z);
        return values;
    }

    public static Vector3 FromShortXZ(this Vector3 vector, short[] values)
    {
        vector.x = values[0];
        vector.z = values[1];
        return vector;
    }
}
