using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    protected string panelName;
    public virtual void Open(){gameObject.SetActive(true);}
    public virtual void Close(){Destroy(gameObject);}
}
