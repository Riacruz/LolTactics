using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Texto : MonoBehaviour
{
    public GameObject panelTexto;
    public TMP_InputField inputField;
    public UndoRedoController undoRedo;
    public Animator animPanelSemiCirculo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseUp()
    {
        animPanelSemiCirculo.SetBool("up", false);
        var menuObjeto = GameObject.FindGameObjectWithTag("MenuObjetos");
        menuObjeto.GetComponent<RectTransform>().localScale = Vector3.zero;
        panelTexto.SetActive(true);
        inputField.Select();
    }

}
