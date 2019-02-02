using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayTutorial : MonoBehaviour
{
   public void Play()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
