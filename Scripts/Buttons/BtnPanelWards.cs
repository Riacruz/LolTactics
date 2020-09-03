using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnPanelWards : MonoBehaviour
{
    public GameObject panelWards;
    public Toggle toggle;

    public void Click_OpenPanelWards()
    {
        S_Util.panelChampActive = panelWards;
        panelWards.SetActive(true);
    }

    public void Click_BtnX()
    {
        panelWards.SetActive(false);
        toggle.isOn = false;
        S_Util.isEnemyActive = false;
    }
}
