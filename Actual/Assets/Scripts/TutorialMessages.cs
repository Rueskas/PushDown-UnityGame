using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TutorialMessages : MonoBehaviour
{
    [SerializeField]
    protected GameObject Player;
    [SerializeField]
    protected GameObject CountLives;
    [SerializeField]
    protected GameObject Lives;
    [SerializeField]
    protected GameObject Run;
    [SerializeField]
    protected GameObject Jump;
    [SerializeField]
    protected GameObject greenM;
    [SerializeField]
    protected GameObject blueM;
    [SerializeField]
    protected GameObject whiteM;
    [SerializeField]
    protected GameObject Escape;
    [SerializeField]
    protected GameObject Protect;
    [SerializeField]
    protected GameObject Button;
    [SerializeField]
    protected GameObject Score;
    [SerializeField]
    protected GameObject GetScore;
    [SerializeField]
    protected GameObject panel;
    [SerializeField]
    protected GameObject block;
    [SerializeField]
    protected GameObject gamePad;
    [SerializeField]
    protected GameObject pushDown;



    [SerializeField]
    protected GameObject Green;
    [SerializeField]
    protected GameObject Blue;
    [SerializeField]
    protected GameObject White;
    [SerializeField]
    protected GameObject PlayerControl;
    [SerializeField]
    protected GameObject GreenGenerator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerPresention()
    {
        Player.SetActive(true);
    }
    public void LivesPresentation()
    {
        Player.SetActive(false);
        Lives.SetActive(true);
    }
    public void RunPresentation()
    {
        gamePad.SetActive(true);
        PlayerControl.GetComponent<PlayerTutorial>().MoveRight();
        CountLives.SetActive(false);
        Lives.SetActive(false);
        Run.SetActive(true);
        
    }

    public void Stop()
    {
        PlayerControl.GetComponent<PlayerTutorial>().Stop();
    }
    public void JumpPresentation()
    {
        Run.SetActive(false);
        Jump.SetActive(true);
    }
    public void PushDownPresentation()
    {
        Jump.SetActive(false);
        pushDown.SetActive(true);
    }
    public void EnemyGreen()
    {
        pushDown.SetActive(false);
        gamePad.SetActive(false);
        Green.SetActive(true);
        greenM.SetActive(true);
    }
    public void EnemyBlue()
    {
        Jump.SetActive(false);
        Blue.SetActive(true);
        greenM.SetActive(false);
        blueM.SetActive(true);
    }
    public void EnemyWhite()
    {
        Jump.SetActive(false);
        White.SetActive(true);
        blueM.SetActive(false);
        whiteM.SetActive(true);
    }
    public void EscapeM()
    {
        Time.timeScale = 0.4f;
        Escape.SetActive(true);
    }
    public void TimeScale()
    {

        Time.timeScale = 1f;
    }
    public void ProtectM()
    {
        White.SetActive(false);
        Escape.SetActive(false);
        Protect.SetActive(true);
    }
    public void JumpPlayer()
    {
        PlayerControl.GetComponent<PlayerTutorial>().Jump();
    }

    public void ButtonDeactived()
    {
        Stop();
        Button.SetActive(true);
    }

    public void BestScore()
    {
        GetScore.SetActive(true);
        Score.SetActive(true);
    }


    int count = 0;

    void FixedUpdate()
    {
        if (Score.activeSelf == true)
        {
            Score.GetComponent<Text>().text = "Score: " + count;
            count += 10;
            if (count == 2000)
            {
                panel.GetComponent<Animator>().Play("Transition");
            }
            else if (count == 2490)
            {
                block.GetComponent<Blocked>().Unlock();
                SceneManager.LoadScene("Init");
            }
        }
    }
}
