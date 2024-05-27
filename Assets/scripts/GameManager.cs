using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameState
{
    ResetGame,
    NextLevel,
    Play,
    GameOver
}
public class GameManager : MonoBehaviour
{
   public static GameManager instance { get; private set; }

    public PlayerMovement ship;
    public ObstacleManager Obstacles;

    public int Score;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI NextLevel;

    public GameObject GameOverScreen;

    public GameState CurrentState;
    float stateTime;


    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            instance = this;
        }
    }
    private void Start()
    {
        ChangeState(GameState.ResetGame);
    }
 void SetScore(int score) { 
    Score = score;
        ScoreText.text = score.ToString();  
            }
    public void AddPoints(int points) {
    SetScore(Score +points);
    }
    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        stateTime = 0;

        switch (newState)
        {
            case GameState.ResetGame:
                GameOverScreen.SetActive(false);
                ship.gameObject.SetActive(true);
                ship.Reset();
                Obstacles.Reset();
                SetScore(0);
                ChangeState(GameState.NextLevel);
                break;
                case GameState.NextLevel:
                Obstacles.NextLevel();
                NextLevel.gameObject.SetActive(true);
                NextLevel.text = "LEVEL" + Obstacles.CurrentLevel;
                break;
                case GameState.Play:
                NextLevel.gameObject.SetActive(false);
                break; 
            case GameState.GameOver:
                NextLevel.gameObject.SetActive(false);
                GameOverScreen.SetActive(true);
                ship.gameObject.SetActive(false);
                break;
                default:
                throw new System.NotImplementedException();
        }
    }
    void UpdateState()
    {
        stateTime += Time.deltaTime;

        
        switch (CurrentState) { 

            case GameState.NextLevel:

                if(stateTime > 3)
                ChangeState(GameState.Play);
                break;

            case GameState.GameOver:
                if (stateTime > 3)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        ChangeState(GameState.ResetGame);
                    }
                }
                break;
        default:
                break;}
    }
    

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }
}
