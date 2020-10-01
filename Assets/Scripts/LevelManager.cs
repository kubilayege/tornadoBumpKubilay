using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public LevelData[] levels;
    public GameObject endRampPrefab;
    public int currentLevelIndex;
    public Level currentLevel;
    void Awake()
    {
        main = this;
        InitLevel();
    }

    void InitLevel()
    {
        currentLevel = new GameObject().AddComponent<Level>();
        currentLevel.data = levels[currentLevelIndex%levels.Length];
        currentLevel.data.ramps = new Ramp[currentLevel.data.rampList.Length];
        currentLevel.data.ramps[0] = Instantiate(currentLevel.data.rampList[0], Vector3.zero, currentLevel.data.rampList[0].transform.rotation, currentLevel.transform).GetComponent<Ramp>();
        currentLevel.data.ramps[0].index = 0;
    }

    public void RestartLevel()
    {
        Player.main.ResetTransform();

        Destroy(currentLevel.gameObject);
        InitLevel();
        UIManager.main.InfoTextUpdate(currentLevelIndex);

    }

    public void PassLevel()
    {
        currentLevelIndex++;
        Player.main.ResetTransform();

        Destroy(currentLevel.gameObject);
        InitLevel();
        UIManager.main.InfoTextUpdate(currentLevelIndex+1);
    }
}
