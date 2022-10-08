using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPointController
{
    private Queue<Vector2> pathPoint;

    public delegate void SpawnNewPoint(Vector2 _positions);
    public event SpawnNewPoint spawnNewPoint;

    public Queue<Vector2> GetQueue => pathPoint;

    public PathPointController()
    {
        pathPoint = new Queue<Vector2>();
    }

    public void SetNewPoint(Touch _touch, Camera _camera)
    {
        Vector2 worldPoint = _camera.ScreenToWorldPoint(new Vector2(_touch.position.x, _touch.position.y));
        pathPoint.Enqueue(worldPoint);
        spawnNewPoint?.Invoke(worldPoint);
    }
}
