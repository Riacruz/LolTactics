using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnObj : MonoBehaviour
{
    public Transform prefab;
    Vector3 screenPoint, offset;
    public UndoRedoController undoRedo;
    public Transform parent;
    public Sprite image;
    public GameObject panelChamps;
    public Toggle[] toggles;
    private void Start()
    {
        
    }
  
    void clickMouseUp()
    {        
        script.OnMouseUp();
        var go = EventSystem.current.currentSelectedGameObject;
        var imageComponent = go.GetComponent<Image>();
        image = imageComponent.sprite;
        script.image = this.image;
        if(S_Util.panelChampActive)
            S_Util.panelChampActive.SetActive(false);
        S_Util.panelSelected = 0;
        foreach (var t in toggles)
        {
            t.isOn = false;
        }
        S_Util.isEnemyActive = false;
    }
    private ArrastraGOSinDuplicar script;
    public void btnOb_Click()
    {
        Transform go = Instantiate(prefab, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane+15)), Quaternion.identity, parent);
        go.gameObject.name = "ob_clone";
        var goScript = go.GetComponent<ArrastraGOSinDuplicar>();
        if(S_Util.isEnemyActive)
        {
            goScript.liineasColor = Color.red;
        }
        else
        {
            goScript.liineasColor = Color.green;
        }        
        undoRedo.lGO.Add(go.gameObject);
        var position = this.transform.position.ToString();
        position = position.Replace("(", "");
        position = position.Replace(")", "");
        undoRedo.lActionsGO.Add("crea," + position);
        script = goScript;
        Invoke("clickMouseUp", 0.01f);
    }

}
