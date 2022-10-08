using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRender
{
    private LineRenderer lineRender;

    public PathRender(Vector2 _playerPoint, LineRenderer _lineRenderer, PathPointController _pathPointController, Circle _circle)
    {
        lineRender = _lineRenderer;
        lineRender.positionCount = 0;

        _pathPointController.spawnNewPoint += AddNewPoint;
        _circle.delitePoint += DelitePoint;
        _circle.circleMoving += SetPlayerPoint;

        AddNewPoint(_playerPoint);
    }

    private void AddNewPoint(Vector2 _point)
    {
        lineRender.positionCount++;
        lineRender.SetPosition(lineRender.positionCount -1, _point);
    }

    private void DelitePoint()
    {
        Vector3[] pathPoints = new Vector3[lineRender.positionCount];
        Vector3[] newPathPoints = new Vector3[lineRender.positionCount -1];

        lineRender.GetPositions(pathPoints);
        newPathPoints[0] = pathPoints[0];

        for(int i = 2; i < lineRender.positionCount; i++)
        {
            newPathPoints[i - 1] = pathPoints[i];
        }

        lineRender.positionCount = newPathPoints.Length;
        lineRender.SetPositions(newPathPoints);
    }
    
    public void DeliteAllPoint()
    {
        lineRender.positionCount = 0;
    }

    public void SetPlayerPoint(Vector2 _point)
    {
        lineRender.SetPosition(0, _point);
    }
}
