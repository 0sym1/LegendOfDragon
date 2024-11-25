using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : Panel
{
    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;
    [SerializeField] private Image musicImg;
    [SerializeField] private Image soundImg;
    private int isMusic;
    private int isSound;
    void Start()
    {
        LoadMusic();
        LoadSound();
    }

    public override void Close(){
        base.Close();
        Messenger.Broadcast(EventKey.CONTINUE_GAME);
    }

    private void LoadMusic(){
        isMusic = PlayerPrefs.GetInt(GameConfig.Music);
        if(isMusic == 0) isMusic = 1;
        if(isMusic == 1) musicImg.sprite = musicOnSprite;
        else musicImg.sprite = musicOffSprite;
    }
    private void LoadSound(){
        isSound = PlayerPrefs.GetInt(GameConfig.Sound);
        if(isSound == 0) isSound = 1;
        if(isSound == 1) soundImg.sprite = soundOnSprite;
        else soundImg.sprite = soundOffSprite;
    }

    public void OnClickFacebookInfo(){
        Application.OpenURL(GameConfig.Facebook_Info);
    }
    public void OnClickGithubInfo(){
        Application.OpenURL(GameConfig.Github_Info);
    }
    public void OnClickDonate(){}
    public void OnClickMusic(){
        if(isMusic == 1){
            isMusic = 0;
            musicImg.sprite = musicOffSprite;
        }
        else{
            isMusic = 1;
            musicImg.sprite = musicOnSprite;
        }
        PlayerPrefs.SetInt(GameConfig.Music, isMusic);
    }
    public void OnClickSound(){
        if(isSound == 1){
            isSound = 0;
            soundImg.sprite = soundOffSprite;
        }
        else{
            isSound = 1;
            soundImg.sprite = soundOnSprite;
        }
        PlayerPrefs.SetInt(GameConfig.Sound, isSound);
    }

}
