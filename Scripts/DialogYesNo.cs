using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogYesNo : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public string aAction;
    public string vAction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Object ob { get; set; }

    public  string Text
    {
        get { return textMesh.text; }
        set
        {
            textMesh.text = value;
        }
    }

    public string Action
    {
        get
        {
            return Action;
        }
        set
        {
            var split = value.Split(',');
            aAction = split[0];
            vAction = split[1];
        }
    }

    public void Yes()
    {
        if(aAction.Equals("deleteFile"))
        {
            System.IO.File.Delete(vAction);
            CargarMapa cm = ob as CargarMapa;
            cm.gameObject.SetActive(true);
        }
        if(aAction.Equals("limpiarMapa"))
        {
            var sl = GameObject.FindGameObjectWithTag("GameController").GetComponent<SaveLoad>();
            sl.ur.ClearAllLGO();
            for (int i = 0; i < sl.parent.gameObject.transform.childCount; i++)
            {
                Transform t = sl.parent.gameObject.transform.GetChild(i);
                Destroy(t.gameObject);
            }
            sl.parent.position = sl.parentInicio.transform.position;
            Camera.main.orthographicSize = 10;
        }
        if (aAction.Equals("salir"))
        {
            Application.Quit();
        }
        this.gameObject.SetActive(false);
    }

    public void No()
    {
        print("NO");
        if (aAction.Equals("deleteFile"))
        {
            CargarMapa cm = ob as CargarMapa;
            cm.gameObject.SetActive(true);
        }
        this.gameObject.SetActive(false);
    }
}
