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
        Messenger.AddListener(EventKey.CONTINUE_GAME, DisPauseGame);
    }
    private void OnDisable(){
        Messenger.RemoveListener<int, int>(EventKey.SCORE_INCREASE, PlusScore);
        Messenger.RemoveListener<int>(EventKey.LEVEL_UP, LevelUP);
        Messenger.RemoveListener(EventKey.CONTINUE_GAME, DisPauseGame);
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
                pauseGame = true;
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
    public void DisPauseGame(){
        pauseGame = false;
    }

    public void OnClickHomeBtn(){
        PauseGame();
        PanelManager.Instance.OpenPanel(GameConfig.BackHomePanel_Noti_Name);
    }
    public void OnClickRestartBtn(){
        PauseGame();
        PanelManager.Instance.OpenPanel(GameConfig.RestartPanel_Noti_Name);
    }
    public void OnClickPauseBtn(){
        PauseGame();
        PanelManager.Instance.OpenPanel(GameConfig.SettingPanel_Name);
    }
    public void OnClickHelpBtn(){
        PauseGame();
        PanelManager.Instance.OpenPanel(GameConfig.HelpPanel_Name);
    }
    public void OnClickSkillBtn(){}
}

///////////// lam cong diem, level up, button