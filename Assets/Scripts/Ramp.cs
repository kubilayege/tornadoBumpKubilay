using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    public int index;
    int i;
    // Start is called before the first frame update
    void Start()
    {
        i = index+1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnNextRamp()
    {
        if (LevelManager.main.currentLevel.data.rampList.Length == i)
            return;

        
        Debug.Log("Hey");
        //GameObject nextRamp = Instantiate(this.gameObject, transform.GetChild(0).transform.position,transform.rotation);
        //nextRamp.transform.Rotate(Vector3.forward, 180);
        LevelManager.main.currentLevel.data.ramps[i] = Instantiate(LevelManager.main.currentLevel.data.rampList[i], LevelManager.main.currentLevel.data.ramps[i - 1].transform.GetChild(0).transform.position,
                                                LevelManager.main.currentLevel.data.rampList[i].transform.rotation, LevelManager.main.currentLevel.transform).GetComponent<Ramp>();
        LevelManager.main.currentLevel.data.ramps[i].transform.Rotate(Vector3.forward, (i % 2) * 180);
        LevelManager.main.currentLevel.data.ramps[i].index = i;
    }
}
