using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ArrastraGOSinDuplicar : MonoBehaviour
{
    Vector3 screenPoint, offset;
    public bool isActive;
    private UndoRedoController undoRedo;
    private BtnsScalingOb btnsScaling;
    private GameObject menuObjetos;
    private Vector3 posInicial;
    private Vector3 posFinal;
    private cakeslice.Outline outLine;

    public Sprite image {
        get
        {            
            var sr = gameObject.GetComponent<SpriteRenderer>();
            return sr.sprite;
        }
        set
        {
            var sr = gameObject.GetComponent<SpriteRenderer>();
            sr.sprite = value;
        }
    }

    public Color liineasColor
    {
        get
        {
            var sr = GetComponentsInChildren<SpriteRenderer>();
            return sr[1].color;
        }

        set
        {
            var sr = GetComponentsInChildren<SpriteRenderer>();
            sr[1].color = value;
        }
    }
    private void Start()
    {
        var go = GameObject.FindGameObjectWithTag("GameController");
        undoRedo = go.GetComponent<UndoRedoController>();
        btnsScaling = go.GetComponent<BtnsScalingOb>();  
        menuObjetos = GameObject.FindGameObjectWithTag("MenuObjetos");
        outLine = this.GetComponent<cakeslice.Outline>();

    }
    
    
    public float InitialTouch;
    void OnMouseDown()
    {        
        if (!isActive) return;
        outLine.color = 0;
        if (!this.gameObject.tag.Equals("Mapa"))
            menuObjetos.transform.localScale = Vector3.zero;
        if (Time.time < InitialTouch + 0.5f && !this.gameObject.tag.Equals("Mapa"))
        {
            
            Debug.Log("DoubleTouch");
            menuObjetos.transform.localScale = Vector3.one;
            menuObjetos.SetActive(true);
            if (this.gameObject.name.Contains("DrawLine"))
            {
                var childs = menuObjetos.GetComponentsInChildren<RectTransform>();
                childs[1].localScale = Vector3.zero;
                childs[2].localScale = Vector3.zero;
            }
            else
            {
                var childs = menuObjetos.GetComponentsInChildren<RectTransform>();
                childs[1].localScale = Vector3.one;
                childs[2].localScale = Vector3.one;

            }

            
        }
        InitialTouch = Time.time;        
        btnsScaling.activeGO = this.gameObject;
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        posInicial = this.gameObject.transform.position;
        //print(offset);
    }

    void OnMouseDrag()
    {        
        if (!isActive) return;
        
        if (this.gameObject.name.Contains("DrawLine")) return;
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        if(Vector3.Distance(transform.position, curPosition)!= 0)
            S_Util.isDragNow = true;
        else
            S_Util.isDragNow = false;
        transform.position = curPosition;
    }

    public void OnMouseUp()
    {        
        if (!isActive) return;
        outLine.color = -1;
        S_Util.isDragNow = false;
        posFinal = this.gameObject.transform.position;
        if (this.gameObject.name.Contains("DrawLine") || this.gameObject.name.Contains("mapaReal")) return;
        var position = this.transform.localPosition.ToString();
        position = position.Replace("(", "");
        position = position.Replace(")", "");
        var debug = GameObject.FindGameObjectWithTag("Debug").GetComponent<Text>();
        debug.text = Vector3.Distance(posInicial, posFinal).ToString() +"," + (undoRedo.lActionsGO.Count > 0) +"," + !undoRedo.lActionsGO[undoRedo.lActionsGO.Count - 1].Equals("mueve," + position);
        if (undoRedo.lActionsGO.Count>0 && !undoRedo.lActionsGO[undoRedo.lActionsGO.Count - 1].Equals("mueve," + position) && Vector3.Distance(posInicial,posFinal)!=0)
        {
            undoRedo.lActionsGO.Add("mueve," + position);
            undoRedo.lGO.Add(this.gameObject);
        }
    }
}
