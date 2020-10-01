using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Screens
{
    MainMenu,
    Ingame,
    Win,
    Fail
}

public class UIManager : MonoBehaviour
{
    Screens currentScreen;
    public GameObject[] screens;
    public static UIManager main;

    public TMPro.TextMeshProUGUI ingameInfo;

    private void Awake()
    {
        main = this;
    }

    public void InfoTextUpdate(int levelNumber)
    {
        ingameInfo.text = "Level " + levelNumber.ToString();
    }

    public void ChangeGameUI(Screens screenIndex)
    {
        screens[(int)currentScreen].SetActive(false);
        screens[(int)screenIndex].SetActive(true);
        currentScreen = screenIndex;

        switch (screenIndex)
        {
            case Screens.MainMenu:
                break;
            case Screens.Ingame:
                break;
            case Screens.Win:
                break;
            case Screens.Fail:
                break;
            default:
                break;
        }
    }
}
