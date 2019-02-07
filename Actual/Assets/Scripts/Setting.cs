using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;

public class Setting : MonoBehaviour
{
    const int SIZE = 2;

    [SerializeField]
    protected Toggle defGamepad;
    [SerializeField]
    protected Toggle alterGamepad;
    [SerializeField]
    protected Toggle onSound;
    [SerializeField]
    protected Toggle offSound;
    [SerializeField]
    protected GameObject[] scenes = new GameObject[SIZE];
    protected Animator animActual;
    protected Animator animAppear;
    protected int actualScene = 0;

    public static string GetGamepad()
    {
        return PlayerPrefs.GetString("gamepad", "default");
    }

    public static void SetGamePad(string input)
    {
        PlayerPrefs.SetString("gamepad",input);
    }

    public static string GetSound()
    {
        return PlayerPrefs.GetString("sound", "ON");
    }

    public static void SetSound(string input)
    {
        PlayerPrefs.SetString("sound", input);
    }

    public void Back()
    {
        SceneManager.LoadScene("Init");
    }

    public void RotateRight()
    {
        if (actualScene + 1 < SIZE)
        {
            scenes[actualScene + 1].SetActive(true);
            animRight();
        }
    }

    public void RotateLeft()
    {
        if (actualScene - 1 >= 0)
        {
            scenes[actualScene - 1].SetActive(true);
            animLeft();
        }
    }

    public void animRight()
    {
        animActual = scenes[actualScene].GetComponent<Animator>();
        animAppear = scenes[actualScene + 1].GetComponent<Animator>();
        animActual.Play("RotateLeft");
        animAppear.Play("AppearRight");
    }

    public void animLeft()
    {
        animActual = scenes[actualScene].GetComponent<Animator>();
        animAppear = scenes[actualScene - 1].GetComponent<Animator>();
        animActual.Play("RotateRight");
        animAppear.Play("AppearLeft");
    }

    public void FinishAnimation()
    {
        scenes[actualScene].SetActive(false);
    }

    void Start()
    {
        if (GetGamepad() == "default")
        {
            defGamepad.isOn = true;
        }
        else
            alterGamepad.isOn = true;

        if (GetSound() == "OFF")
        {
            offSound.isOn = true;
            onSound.isOn = false;
        }
        else
        {
            onSound.isOn = true;
            offSound.isOn = false;
        }
    }
    void FixedUpdate()
    {
        if (defGamepad.isOn)
        {
            SetGamePad("default");
        }
        else
            SetGamePad("alternative");

        if (onSound.isOn)
        {
            SetSound("ON");
            AudioListener.volume = 1f;
        }
        else
        {
            SetSound("OFF");
            AudioListener.volume = 0f;
        }

        if (scenes[actualScene].transform.position.x < -14 && scenes[actualScene].transform.position.x > -17)
        {
            FinishAnimation();
            actualScene++;
        }

        if (scenes[actualScene].transform.position.x > 14 && scenes[actualScene].transform.position.x < 17)
        {
            FinishAnimation();
            actualScene--;
        }

    }

}
