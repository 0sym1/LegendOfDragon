using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartNoti : Panel
{
    public override void Close(){
        base.Close();
        Messenger.Broadcast(EventKey.CONTINUE_GAME);
    }
    public void OnClickConfirm(){
        SceneManager.LoadScene("GameScene");
    }
}
