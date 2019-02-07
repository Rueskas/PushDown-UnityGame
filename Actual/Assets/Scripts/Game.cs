using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField]
    protected GameObject generator;
    [SerializeField]
    protected GameObject initMessage;
    [SerializeField]
    protected GameObject livesMessage;
    [SerializeField]
    protected GameObject restartMessage;
    [SerializeField]
    protected GameObject scoreMessage;
    [SerializeField]
    protected GameObject player;
    [SerializeField]
    protected GameObject cameraGame;
    [SerializeField]
    protected GameObject gamepad;
    [SerializeField]
    protected GameObject alterGamepad;
    [SerializeField]
    protected GameObject yes;
    [SerializeField]
    protected GameObject no;
    protected int score = 0;

    public void SetScore(int score)
    {
        if ( score > 0 || this.score + score > 0)
        {
            this.score += score;
        }
    }
    public enum GameState { Idle, Playing, Finished}
    protected GameState gameState;
    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
    }
    public GameState GetGameState()
    {
        return this.gameState;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Setting.GetGamepad() == "default")
        {
            gamepad.SetActive(true);
            alterGamepad.SetActive(false);
        }
        else
        {
            gamepad.SetActive(false);
            alterGamepad.SetActive(true);
        }

        Destroy(GameObject.FindGameObjectWithTag("MusicGUI"));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.Idle)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameState = GameState.Playing;
            }
        }

        if (gameState == GameState.Playing && player.GetComponent<PlayerController>().GetLives() >= 0)
        {
            generator.SetActive(true);
            initMessage.SetActive(false);
            scoreMessage.SetActive(true);
            livesMessage.GetComponent<Text>().text = "LIVES: " + player.GetComponent<PlayerController>().GetLives();
            scoreMessage.GetComponent<Text>().text = "SCORE: " + score;
            livesMessage.SetActive(true);
            livesMessage.transform.position = new Vector2(cameraGame.GetComponent<Transform>().transform.position.x - 4, cameraGame.GetComponent<Transform>().transform.position.y + 3.7f);
            scoreMessage.transform.position = new Vector2(cameraGame.GetComponent<Transform>().transform.position.x + 5, cameraGame.GetComponent<Transform>().transform.position.y + 3.7f);
        }
            
        if (gameState == GameState.Finished)
        {
            restartMessage.transform.position = new Vector2(player.transform.position.x, player.transform.position.y+3);
            yes.transform.position = new Vector2(player.transform.position.x - 2, player.transform.position.y + 2);
            no.transform.position = new Vector2(player.transform.position.x + 2, player.transform.position.y + 2);

            livesMessage.SetActive(false);
            restartMessage.SetActive(true);
            scoreMessage.transform.position = new Vector2(cameraGame.GetComponent<Transform>().transform.position.x + 5, cameraGame.GetComponent<Transform>().transform.position.y + 3.7f);
            yes.SetActive(true);
            no.SetActive(true);
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Init");
    }

    public void Continue()
    {
        SceneManager.LoadScene("Game");
    }
   
}
