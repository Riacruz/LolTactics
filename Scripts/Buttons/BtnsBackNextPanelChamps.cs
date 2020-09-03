using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtnsBackNextPanelChamps : MonoBehaviour
{
    public GameObject[] panels;
    public Toggle[] toggles;
   // public GameObject[] btns; //0 next, 1 back
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Click_Next()
    {  
        panels[S_Util.panelSelected +1].SetActive(true);
        panels[S_Util.panelSelected].SetActive(false);
        S_Util.panelSelected = S_Util.panelSelected + 1;
        S_Util.panelChampActive = panels[S_Util.panelSelected];
        foreach (var t in toggles)
        {
            t.isOn = false;
        }
        S_Util.isEnemyActive = false;

    }

    public void Click_Back()
    {
        panels[S_Util.panelSelected - 1].SetActive(true);
        panels[S_Util.panelSelected].SetActive(false);
        S_Util.panelSelected = S_Util.panelSelected - 1;
        S_Util.panelChampActive = panels[S_Util.panelSelected];
        foreach (var t in toggles)
        {
            t.isOn = false;
        }
        S_Util.isEnemyActive = false;
    }

    public void Click_Close()
    {
        panels[S_Util.panelSelected].SetActive(false);
        S_Util.panelSelected = 0;
        foreach (var t in toggles)
        {
            t.isOn = false;
        }
        S_Util.isEnemyActive = false;
    }

    public void Click_ToogleIsEnemy(bool b)
    {
        if (EventSystem.current.currentSelectedGameObject)
        {
            var to = EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>();
            if(to) S_Util.isEnemyActive = to.isOn;
        }
    }
}
