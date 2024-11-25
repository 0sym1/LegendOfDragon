using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanel : Panel
{
    public override void Close(){
        base.Close();
        Messenger.Broadcast(EventKey.CONTINUE_GAME);
    }
}
