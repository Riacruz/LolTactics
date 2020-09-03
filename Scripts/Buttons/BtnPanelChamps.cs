using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnPanelChamps : MonoBehaviour
{
    public GameObject panelChamps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Click_OpenPanelChamps()
    {
        panelChamps.SetActive(true);
        S_Util.panelChampActive = panelChamps;
    }
}
