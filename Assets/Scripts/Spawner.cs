using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Camera myCamera;
    private GameSettings gameSettings;

    private Collider2D[] colliders;
    private int collidersIndex = 0;
    private int indent = 100;
    private int fixetUpdateStep = 0;
    private int fixetUpdateMaxStep = 15;
    private bool positionsSet = false;

    public delegate void SceneIsSet();
    public event SceneIsSet sceneIsSet;

    public void Initialization(GameSettings _gameSettings, Camera _myCamera, PathPointController _pathPointController)
    {
        gameSettings = _gameSettings;
        myCamera = _myCamera;
        colliders = new Collider2D[gameSettings.MoneyCount + gameSettings.SpikeCount];
        ObjectSpawn(gameSettings.MoneyCount, gameSettings.Money);
        ObjectSpawn(gameSettings.SpikeCount, gameSettings.Spike);

        _pathPointController.spawnNewPoint += PathPointSpawn;
    }

    private void ObjectSpawn(int _objectCount, GameObject _object)
    {
        if (_objectCount > 0)
        {
            for (int i = 0; i < _objectCount; i++)
            {
                GameObject newObject = Instantiate(_object);
                colliders[collidersIndex] = newObject.GetComponent<Collider2D>();
                collidersIndex++;
            }
        }
    }

    public void PathPointSpawn(Vector2 _positions)
    {
        GameObject newObject = Instantiate(gameSettings.PathPoint);
        newObject.transform.position = _positions;
    }

    private Vector2 SetRandomPoint()
    {
        int randomX = Random.Range(0, Screen.currentResolution.width - indent);
        int randomY = Random.Range(0, Screen.currentResolution.height - indent);

        return myCamera.ScreenToWorldPoint(new Vector2(randomX, randomY));
    }

    private bool CollisionChecking(Collider2D _collider)
    {
        Collider2D[] results = new Collider2D[1];
        ContactFilter2D filter = new ContactFilter2D().NoFilter();

        if (_collider.OverlapCollider(filter, results) == 0) return true;
        else return false;
    }

    private void FixedUpdate()
    {
        if (!positionsSet && fixetUpdateStep < fixetUpdateMaxStep)
        {
            List<Collider2D> collisionList = new List<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                if (collider != null)
                    if (!CollisionChecking(collider)) collisionList.Add(collider);
            }

            if (collisionList.Count != 0)
            {
                foreach (Collider2D collisionCollider in collisionList)
                {
                    collisionCollider.gameObject.transform.position = SetRandomPoint();
                }
            }
            else
            {
                positionsSet = true;
                foreach(Collider2D collider in colliders)
                {
                    collider.isTrigger = true;
                }
                sceneIsSet?.Invoke();
            }
        }

        if (fixetUpdateStep >= fixetUpdateMaxStep)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider != null)
                    if (!CollisionChecking(collider)) Destroy(collider.gameObject);
            }
            fixetUpdateStep = 0;
        }
    }
}
