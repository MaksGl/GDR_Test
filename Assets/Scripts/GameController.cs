using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameSettings gameSettings;
    [SerializeField]
    private Circle circle;

    private Camera myCamera;
    private Spawner spawner;
    private UiController uiController;
    private PathPointController pathPointControllr;
    private PathRender pathRender;

    private int score = 0;

    private void ScoreRaise()
    {
        score++;
        uiController.UpdateScore(score.ToString());

        if (score == gameSettings.MoneyCount)
        {
            uiController.PanelActive("Victory!");
        }
    }

    private void GameOver(string _message)
    {
        circle.GetComponent<Rigidbody2D>().isKinematic = true;
        uiController.PanelActive(_message);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        myCamera = GetComponent<Camera>();
        uiController = GetComponent<UiController>();
        spawner = GetComponent<Spawner>();

        pathPointControllr = new PathPointController();
        pathRender = new PathRender(circle.GetPositionsToVector2(), GetComponent<LineRenderer>(), pathPointControllr, circle);

        uiController.Initialization(myCamera, spawner);
        circle.Initialization(gameSettings);
        spawner.Initialization(gameSettings, myCamera, pathPointControllr);

        circle.moneyCollision += ScoreRaise;
        circle.spikeCollision += GameOver;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                pathPointControllr.SetNewPoint(touch, myCamera);
            }
        }

        if(pathPointControllr.GetQueue.Count > 0 && !circle.GetMoving)
        {
            circle.StartMove(pathPointControllr.GetQueue.Dequeue());
        }
    }
}
