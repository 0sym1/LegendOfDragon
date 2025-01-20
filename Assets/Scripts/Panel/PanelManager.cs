using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PanelManager : Singleton<PanelManager>
{
    private List<Panel> listPanel;
    private Dictionary<string, Panel> panelDictionary;
    private void Start()
    {
        LoadAllPanel();
    }

    private void LoadAllPanel(){
        panelDictionary = new Dictionary<string, Panel>();
        Panel[] panels = Resources.LoadAll<Panel>(GameConfig.Panel_Prefabs_Path);
        foreach (Panel panel in panels){
            panelDictionary.Add(panel.name, panel);
        }
    }

    public Panel GetPanel(string name){
        Panel panelPrefab = panelDictionary[name];
        var panel = Instantiate(panelPrefab, transform);
        return panel;
    }

    public void OpenPanel(string name){
        Panel panel = GetPanel(name);
        panel.Open();
    }
}
