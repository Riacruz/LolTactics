using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnsScalingOb : MonoBehaviour
{
    public UndoRedoController undoRedo;
    public GameObject activeGO;
    public void clickMas()
    {
        if (undoRedo.lGO.Count < 1 || activeGO == null) return;
        if (activeGO.tag.Equals("Mapa")) return;
        var ob = activeGO;
        ob.transform.localScale = new Vector3(ob.transform.localScale.x+ 0.1f, ob.transform.localScale.y + 0.1f, ob.transform.localScale.z);
    }

    public void clickMenos()
    {
        if (undoRedo.lGO.Count < 1 || activeGO == null) return;
        if (activeGO.tag.Equals("Mapa")) return;
        var ob = activeGO;
        ob.transform.localScale = new Vector3(ob.transform.localScale.x - 0.1f, ob.transform.localScale.y - 0.1f, ob.transform.localScale.z);
    }

    public void clickBorrar()
    {
        var panel = GameObject.FindGameObjectWithTag("MenuObjetos");
        GameObject go;
        activeGO.SetActive(false); // OJO, Para Undo Redo no debes destruirlo
        var split = undoRedo.lActionsGO[undoRedo.lActionsGO.Count - 1].Split(',');
        var position = "";
        if(split.Length>2)
            position = $"{split[1]},{split[2]},{split[3]}";
        undoRedo.lGO.Add(activeGO.gameObject);
        undoRedo.lActionsGO.Add("borra,"+position);
        panel.transform.localScale = Vector3.zero;
    }
}
