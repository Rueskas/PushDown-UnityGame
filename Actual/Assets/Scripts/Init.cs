using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Init : MonoBehaviour
{

    [SerializeField]
    protected SpriteRenderer button;
    protected float transparence;

    public string GetBlocked()
    {
        return PlayerPrefs.GetString("blocked", "true");
    }

    public static void Unlock()
    {
        PlayerPrefs.SetString("blocked", "false");
    }

    public void ShowBlocked()
    {
        if (GetBlocked() == "true")
        {
            transparence = 255f;
            button.color = new Color(255f, 255f, 255f, transparence);
        }

        else
        {
            SceneManager.LoadScene("GAME");
        }
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (button.color.a != 0)
        {
            button.color = new Color(255f, 255f, 255f, transparence);
            transparence -= 3;
        }
    }


}
