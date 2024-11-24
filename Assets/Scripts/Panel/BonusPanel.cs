using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPanel : Panel
{
    // Start is called before the first frame update
    public void OnClickGift(){
        PanelManager.Instance.OpenPanel(GameConfig.GiftPanel_Name);
    }
}
