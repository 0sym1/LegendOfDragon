using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : Panel
{
    [SerializeField] private TextMeshProUGUI score_Txt;
    [SerializeField] private TextMeshProUGUI highScore_Txt;
    [SerializeField] private Image level_Img;

    private void Start(){
        score_Txt.text = PlayerPrefs.GetInt(GameConfig.Score).ToString();
        highScore_Txt.text = PlayerPrefs.GetInt(GameConfig.High_Score).ToString();
        level_Img.sprite = ObjectPool.Instance.GetSpriteEgg(PlayerPrefs.GetInt(GameConfig.Level) - 1); //-1 vì list bắt đầu từ 0
    }
    public void OnClickBackHome(){
        SceneManager.LoadScene("HomeScene");
    }
    public void OnClickAchievements(){
        PanelManager.Instance.OpenPanel(GameConfig.AchievementPanel_Name);
    }
    public void OnClickRestart(){
        SceneManager.LoadScene("GameScene");
    }
    public void OnClickBonus(){
        PanelManager.Instance.OpenPanel(GameConfig.BonusPanel_Name);
    }
}
