using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBounds : MonoBehaviour
{
    public static MapBounds Instance;
    [SerializeField] Vector3 minBounds = new Vector3 (-20, 0, -20);
    [SerializeField] Vector3 maxBounds = new Vector3(20, 0, 20);

    void Awake()
    {
        Instance = this;
    }

    public Vector3 ClampPosition(Vector3 pos)
    {
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);
        pos.z = Mathf.Clamp(pos.z, minBounds.z, maxBounds.z);

        return pos;
    }

    public bool IsInside(Vector3 pos)
    {
        return pos.x >= minBounds.x && pos.x <= maxBounds.x &&
               pos.y >= minBounds.y && pos.y <= maxBounds.y &&
               pos.z >= minBounds.z && pos.z <= maxBounds.z;
    }
}
