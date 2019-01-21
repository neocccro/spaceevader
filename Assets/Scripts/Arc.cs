using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//made by Coen van Diepen
public static class Arc
{
    public static Vector2[] ArcLocations(int amount, float startOffset, float endOffset, float magnitude, bool includeEnd)
    {
        List<Vector2> locs = new List<Vector2>();
        for (int i = 0; i < amount; i++)
        {
            locs.Add(locationInAnArc((float)i / (amount - (includeEnd ? 1 : 0)) * (endOffset - startOffset) + startOffset, magnitude));
        }
        return locs.ToArray();
    }

    public static float[] ArcRotations(int amount, float startOffset, float endOffset, bool includeEnd)
    {
        List<float> rots = new List<float>();
        for (int i = 0; i < amount; i++)
        {
            rots.Add((float)i / (amount - (includeEnd ? 1 : 0)) * (endOffset - startOffset) + startOffset);
        }
        return rots.ToArray();
    }

    public static Vector2 locationInAnArc(float angle, float magnitude)
    {
        float rad = angle * 2 * Mathf.PI;
        float x = Mathf.Sin(rad);
        float y = Mathf.Sin(rad + Mathf.PI / 2);
        return new Vector2(x, y) * magnitude;
    }
}
