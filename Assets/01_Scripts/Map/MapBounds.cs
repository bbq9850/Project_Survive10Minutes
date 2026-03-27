using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBounds : MonoBehaviour
{
    public static MapBounds Instance;

    [SerializeField] Vector3 minBounds = new Vector3(-35, 0, -35);
    [SerializeField] Vector3 maxBounds = new Vector3(35, 0, 35);

    [Header("Walls")]
    [SerializeField] WallFades wallLeft;
    [SerializeField] WallFades wallRight;
    [SerializeField] WallFades wallTop;
    [SerializeField] WallFades wallBottom;

    [Header("Fade Settings")]
    [SerializeField] float fadeDistance = 5f; // ? 이 거리 안에 들어오면 보이기 시작

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

    public void UpdateWalls(Vector3 playerPos)
    {
        float leftDist = Mathf.Abs(playerPos.x - minBounds.x);
        float rightDist = Mathf.Abs(maxBounds.x - playerPos.x);
        float bottomDist = Mathf.Abs(playerPos.z - minBounds.z);
        float topDist = Mathf.Abs(maxBounds.z - playerPos.z);

        UpdateWall(wallLeft, leftDist);
        UpdateWall(wallRight, rightDist);
        UpdateWall(wallBottom, bottomDist);
        UpdateWall(wallTop, topDist);
    }

    void UpdateWall(WallFades wall, float distance)
    {
        if (wall == null) return;

        if (distance <= fadeDistance)
        {
            wall.SetAlphaByDistance(distance, fadeDistance);
        }
        else
        {
            wall.Hide();
        }
    }

    public bool IsInside(Vector3 pos)
    {
        return pos.x >= minBounds.x && pos.x <= maxBounds.x &&
               pos.y >= minBounds.y && pos.y <= maxBounds.y &&
               pos.z >= minBounds.z && pos.z <= maxBounds.z;
    }
}
