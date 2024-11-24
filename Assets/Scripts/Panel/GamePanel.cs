using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : Singleton<GamePanel>
{
    [SerializeField] private Image timeLineImg;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI levelTxt;
    [SerializeField] private float timePlay;
    private bool pauseGame;

    private void OnEnable(){
        Messenger.AddListener<int, int>(EventKey.SCORE_INCREASE, PlusScore);
        Messenger.AddListener<int>(EventKey.LEVEL_UP, LevelUP);
    }
    private void OnDisable(){
        Messenger.RemoveListener<int, int>(EventKey.SCORE_INCREASE, PlusScore);
        Messenger.RemoveListener<int>(EventKey.LEVEL_UP, LevelUP);
    }

    private void Start(){
        scoreTxt.text = "0";
        levelTxt.text = "1";
        timeLineImg.fillAmount = 1;
        pauseGame = false;
    }

    private void Update(){
        if(!pauseGame){
            timeLineImg.fillAmount -= Time.deltaTime/timePlay;
            if(timeLineImg.fillAmount <= 0){
                GameController.Instance.GameOver(int.Parse(scoreTxt.text), int.Parse(levelTxt.text));
            }
        }
    }

    private void PlusScore(int amount, int level){
        scoreTxt.text = (int.Parse(scoreTxt.text) + amount*level).ToString();
    }
    private void LevelUP(int level){
        levelTxt.text = Math.Max(int.Parse(levelTxt.text), level).ToString();
    }
    public void ResetTimeline(){
        timeLineImg.fillAmount = 1;
    }
    public void PauseGame(){
        pauseGame = true;
    }

    public void OnClickHomeBtn(){}
    public void OnClickRestartBtn(){}
    public void OnClickPauseBtn(){}
    public void OnClickHelpBtn(){}
    public void OnClickSkillBtn(){}
}

///////////// lam cong diem, level up, button