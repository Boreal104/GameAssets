using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveScript : MonoBehaviour
{
    public TMP_Text WaveText;
    public Slider WaveSlider;
    public float WaveSpawnWaitTime;
    public List<string> EnemyTags;
    public GameObject EnemyPrefab;
    private bool IsInWave=false;
    private int WaveCount=1;
    private float TimeCounter;
    private int EnemyCount;

    private void Start() {
        WaveSlider.interactable=false;
    }

    void FixedUpdate()
    {
        if (IsInWave)
        {
            //When In Wave
            int CurrentEnemyCount = GetCountByTags(EnemyTags);
            WaveText.text=$"Wave {WaveCount}";
            WaveSlider.value = (float)CurrentEnemyCount / EnemyCount;
            if(CurrentEnemyCount==0f){
                IsInWave=false;
                WaveCount++;
            }
        }else{
            //When Waiting For The Wave
            TimeCounter+=Time.fixedDeltaTime;
            WaveText.text=$"Wave {WaveCount} Incoming";
            WaveSlider.value=TimeCounter/WaveSpawnWaitTime;
            if(TimeCounter>=WaveSpawnWaitTime){
                IsInWave=true;
                TimeCounter=0f;

                //(Change as needed) EnemySpawnCode Example:
                for (int i = 0; i < WaveCount; i++)
                {
                    Instantiate(EnemyPrefab);
                    Instantiate(EnemyPrefab);
                    Instantiate(EnemyPrefab);
                    Instantiate(EnemyPrefab);
                }
                // End Of EnemySpawnCode

                EnemyCount=GetCountByTags(EnemyTags);
            }
        }
    }

    private int GetCountByTags(List<string> ListOfTags){
        int count=0;
        foreach (string tag in ListOfTags)
        {
            count+=GameObject.FindGameObjectsWithTag(tag).Length;
        }
        return count;
    }
}
