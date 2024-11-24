using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePanel : Panel
{
    public void OnClickPlayBtn(){
        SceneManager.LoadScene("GameScene");
    }
    public void OnClickShareBtn(){}
    public void OnClickAchievementBtn(){
        PanelManager.Instance.OpenPanel(GameConfig.AchievementPanel_Name);
    }
    public void OnClickBonusBtn(){
        PanelManager.Instance.OpenPanel(GameConfig.BonusPanel_Name);
    }
    public void OnClickSettingBtn(){
        PanelManager.Instance.OpenPanel(GameConfig.SettingPanel_Name);
    }
    public void OnClickHelpBtnBtn(){
        PanelManager.Instance.OpenPanel(GameConfig.HelpPanel_Name);
    }
}
