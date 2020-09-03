using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndoRedoController : MonoBehaviour
{
    public List<GameObject> lGO; //GO 
    public List<GameObject> lGOUndo; //GO Undo
    public List<string> lActionsGO; //GO Acciones realizadas
    public List<string> lActionsGOUndo; //GO Acciones des realizadas
    // Start is called before the first frame update
    void Start()
    {
        lGO = new List<GameObject>();
        lGOUndo = new List<GameObject>();
        lActionsGO = new List<string>();
        lActionsGOUndo = new List<string>();
    }
    void Update()
    {
        /*
        var debug = GameObject.FindGameObjectWithTag("Debug").GetComponent<Text>();
        string str = "";
        for (int i=0;i<lGO.Count;i++)
        {
            str += lActionsGO[i]+" : "+lGO[i]+"\n";
        }
        str += "\nRedo\n";
        for (int i = 0; i < lGOUndo.Count; i++)
        {
            str += lActionsGOUndo[i] + " : " + lGOUndo[i]+"\n";
        }
        debug.text = str;
        */
    }

    public void ClearAllLGO()
    {
        lGO.Clear();
        lGOUndo.Clear();
        lActionsGO.Clear();
        lActionsGOUndo.Clear();
    }
}
