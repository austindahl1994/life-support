using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloorPaths : ScriptableObject
{
    public Vector2Tuple[] path;
}

[System.Serializable]
public class Vector2Tuple
{
    public Vector2 start;
    public Vector2 end;

    public Vector2Tuple(Vector2 start, Vector2 end)
    {
        this.start = start;
        this.end = end;
    }
}