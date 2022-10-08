using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Circle : MonoBehaviour
{
    private GameSettings gameSettings;
    private Rigidbody2D rigibody;
    private bool moving = false;
    private Vector2 pathPoint;

    public delegate void DelitePoint();
    public event DelitePoint delitePoint;

    public delegate void CircleMoving(Vector2 _point);
    public event CircleMoving circleMoving;

    public delegate void MoneyCollision();
    public event MoneyCollision moneyCollision;

    public delegate void SpikeCollision(string message);
    public event SpikeCollision spikeCollision;

    public bool GetMoving => moving;

    public void Initialization(GameSettings _gameSettings)
    {
        gameSettings = _gameSettings;
        rigibody = GetComponent<Rigidbody2D>();
        var originalSize = GetComponent<SpriteRenderer>().sprite.rect.size;
        transform.localScale = new Vector2(gameSettings.CircleSize / originalSize.x, gameSettings.CircleSize / originalSize.y);
    }

    public void StartMove(Vector2 _point)
    {
        moving = true;
        pathPoint = _point;
    }

    public Vector2 GetPositionsToVector2()
    {
        return new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
    }

    private void FixedUpdate()
    {
        if (pathPoint != Vector2.zero)
        {
            Vector2 pathPointVector = pathPoint - GetPositionsToVector2();
            rigibody.velocity = (pathPointVector * gameSettings.CircleSpeed).normalized;
            circleMoving?.Invoke(GetPositionsToVector2());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PathPoint")
        {
            moving = false;
            rigibody.velocity = Vector2.zero;
            gameObject.transform.position = pathPoint;
            pathPoint = Vector2.zero;
            Destroy(collision.gameObject);
            delitePoint?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Money":
                moneyCollision?.Invoke();
                Destroy(collision.gameObject);
                break;
            case "Spike":
                Destroy(gameObject);
                spikeCollision?.Invoke("Game Over");
                break;
            default: break;
        }
    }
}
