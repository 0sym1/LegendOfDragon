using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    protected string panelName;
    public void Open(){gameObject.SetActive(true);}
    public void Close(){Destroy(gameObject);}
}
