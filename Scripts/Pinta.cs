using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinta : MonoBehaviour
{
    public DrawLine2D drawLine;
    private Transform transfInicio;
    //private bool yaPulsado = false;
    public Transform prefab;
    public Transform mapa;
    public UndoRedoController undoRedo;
    public Transform parent;
    public GameObject panelPinta;
    public Animator panelSemiCirculo;
    // Start is called before the first frame update
    void Start()
    {
        transfInicio = this.transform;        
    }

    public void OnMouseUp()
    {
        //this.transform.position = new Vector3(transfInicio.position.x, transfInicio.position.y+10, transfInicio.position.z);
        //drawLine.isActive = true;
        var go = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
        go.GetComponent<DrawLine2D>().isActive = true;
        var position = "";
        if (undoRedo.lActionsGO.Count > 0)
        {
            var split = undoRedo.lActionsGO[undoRedo.lActionsGO.Count - 1].Split(',');
            if (split.Length > 2)
            {
                position = $"{split[1]},{split[2]},{split[3]}";
            }
        }
        undoRedo.lGO.Add(go.gameObject);
        undoRedo.lActionsGO.Add("pinta,"+position);
        panelSemiCirculo.SetBool("up", false);
        var menuObjeto = GameObject.FindGameObjectWithTag("MenuObjetos");
        menuObjeto.GetComponent<RectTransform>().localScale = Vector3.zero;
        panelPinta.SetActive(true);
        S_Util.isPainting = true;
    }

    public void Click_StopPinta()
    {
        panelPinta.SetActive(false);
        S_Util.isPainting = false;
    }
}
