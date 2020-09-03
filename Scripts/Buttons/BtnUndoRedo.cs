using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnUndoRedo : MonoBehaviour
{
    UndoRedoController undoRedo;
    public Transform parent;
    private GameObject parentInicio;

    private void Start()
    {
        undoRedo = this.GetComponent<UndoRedoController>();
        parentInicio = GameObject.Find("parentInicio");
        parentInicio.transform.position = parent.transform.position;
        System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("en-US");
    }
    public async void btnUndo_Click()
    {
        parent.position = parentInicio.transform.position;
        if (undoRedo.lGO.Count < 1) return;

        if (undoRedo.lActionsGO.Count>1 && undoRedo.lActionsGO[undoRedo.lActionsGO.Count - 2].Contains("crea"))
        {
            undoRedo.lGOUndo.Add(undoRedo.lGO[undoRedo.lGO.Count - 1]);
            undoRedo.lActionsGOUndo.Add(undoRedo.lActionsGO[undoRedo.lActionsGO.Count - 1]);
            undoRedo.lGOUndo.Add(undoRedo.lGO[undoRedo.lGO.Count - 2]);
            undoRedo.lActionsGOUndo.Add(undoRedo.lActionsGO[undoRedo.lActionsGO.Count - 2]);

            undoRedo.lGO[undoRedo.lGO.Count - 1].SetActive(false);

            undoRedo.lGO.RemoveAt(undoRedo.lGO.Count - 1);
            undoRedo.lActionsGO.RemoveAt(undoRedo.lActionsGO.Count - 1);
            undoRedo.lGO.RemoveAt(undoRedo.lGO.Count - 1);
            undoRedo.lActionsGO.RemoveAt(undoRedo.lActionsGO.Count - 1);
        }
        else if (undoRedo.lActionsGO[undoRedo.lActionsGO.Count - 1].Contains("mueve"))
        {
            undoRedo.lGOUndo.Add(undoRedo.lGO[undoRedo.lGO.Count - 1]);
            undoRedo.lActionsGOUndo.Add(undoRedo.lActionsGO[undoRedo.lActionsGO.Count - 1]);
            print(undoRedo.lActionsGO[undoRedo.lActionsGO.Count - 1]);
            var split = undoRedo.lActionsGO[undoRedo.lActionsGO.Count - 2].Split(',');
            
            //undoRedo.lGO[undoRedo.lGO.Count - 1].transform.position = Vector3.zero;
           // await WaitOneSecondAsync();
            
            //undoRedo.lGO[undoRedo.lGO.Count - 1].transform.localPosition = new Vector3(float.Parse(split[1].Replace(".", ",")), float.Parse(split[2].Replace(".", ",")), float.Parse(split[3].Replace(".", ",")));
            undoRedo.lGO[undoRedo.lGO.Count - 1].transform.localPosition = new Vector3(float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3]));
            

            undoRedo.lGO.RemoveAt(undoRedo.lGO.Count - 1);
            undoRedo.lActionsGO.RemoveAt(undoRedo.lActionsGO.Count - 1);
        }
        else if (undoRedo.lActionsGO[undoRedo.lActionsGO.Count - 1].Contains("pinta"))
        {
            undoRedo.lGOUndo.Add(undoRedo.lGO[undoRedo.lGO.Count - 1]);
            undoRedo.lActionsGOUndo.Add(undoRedo.lActionsGO[undoRedo.lActionsGO.Count - 1]);

            undoRedo.lGO[undoRedo.lGO.Count - 1].SetActive(false);

            undoRedo.lGO.RemoveAt(undoRedo.lGO.Count - 1);
            undoRedo.lActionsGO.RemoveAt(undoRedo.lActionsGO.Count - 1);
        }
        else if (undoRedo.lActionsGO[undoRedo.lActionsGO.Count - 1].Contains("borra"))
        {
            undoRedo.lGOUndo.Add(undoRedo.lGO[undoRedo.lGO.Count - 1]);
            undoRedo.lActionsGOUndo.Add(undoRedo.lActionsGO[undoRedo.lActionsGO.Count - 1]);

            undoRedo.lGO[undoRedo.lGO.Count - 1].SetActive(true);

            undoRedo.lGO.RemoveAt(undoRedo.lGO.Count - 1);
            undoRedo.lActionsGO.RemoveAt(undoRedo.lActionsGO.Count - 1);
        }
    }
    public async void btnRedo_Click()
    {
        parent.position = parentInicio.transform.position;
        if (undoRedo.lGOUndo.Count < 1) return;
        print("Es mayor que uno");
        if (undoRedo.lActionsGOUndo[undoRedo.lActionsGOUndo.Count - 1].Contains("crea"))
        {
            undoRedo.lGO.Add(undoRedo.lGOUndo[undoRedo.lGOUndo.Count - 1]);
            undoRedo.lActionsGO.Add(undoRedo.lActionsGOUndo[undoRedo.lActionsGOUndo.Count - 1]);
            undoRedo.lGO.Add(undoRedo.lGOUndo[undoRedo.lGOUndo.Count - 2]);
            undoRedo.lActionsGO.Add(undoRedo.lActionsGOUndo[undoRedo.lActionsGOUndo.Count - 2]);

            undoRedo.lGOUndo[undoRedo.lGOUndo.Count - 1].SetActive(true);

            undoRedo.lGOUndo.RemoveAt(undoRedo.lGOUndo.Count - 1);
            undoRedo.lActionsGOUndo.RemoveAt(undoRedo.lActionsGOUndo.Count - 1);
            undoRedo.lGOUndo.RemoveAt(undoRedo.lGOUndo.Count - 1);
            undoRedo.lActionsGOUndo.RemoveAt(undoRedo.lActionsGOUndo.Count - 1);

        }
        else if (undoRedo.lActionsGOUndo[undoRedo.lActionsGOUndo.Count - 1].Contains("mueve"))
        {
           // undoRedo.lGOUndo[undoRedo.lGOUndo.Count - 1].SetActive(true); //Ojo, quizá inecesaria
            undoRedo.lGO.Add(undoRedo.lGOUndo[undoRedo.lGOUndo.Count - 1]);
            undoRedo.lActionsGO.Add(undoRedo.lActionsGOUndo[undoRedo.lActionsGOUndo.Count - 1]);

            var split = undoRedo.lActionsGOUndo[undoRedo.lActionsGOUndo.Count - 1].Split(',');
            //undoRedo.lGOUndo[undoRedo.lGOUndo.Count - 1].transform.position = Vector3.zero;
            //await WaitOneSecondAsync();
            //undoRedo.lGOUndo[undoRedo.lGOUndo.Count - 1].transform.localPosition = new Vector3(float.Parse(split[1].Replace(".", ",")), float.Parse(split[2].Replace(".", ",")), float.Parse(split[3].Replace(".", ",")));
            undoRedo.lGOUndo[undoRedo.lGOUndo.Count - 1].transform.localPosition = new Vector3(float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3]));

            undoRedo.lGOUndo.RemoveAt(undoRedo.lGOUndo.Count - 1);
            undoRedo.lActionsGOUndo.RemoveAt(undoRedo.lActionsGOUndo.Count - 1);
        }
        else if (undoRedo.lActionsGOUndo[undoRedo.lActionsGOUndo.Count - 1].Contains("pinta"))
        {
            undoRedo.lGO.Add(undoRedo.lGOUndo[undoRedo.lGOUndo.Count - 1]);
            undoRedo.lActionsGO.Add(undoRedo.lActionsGOUndo[undoRedo.lActionsGOUndo.Count - 1]);

            undoRedo.lGOUndo[undoRedo.lGOUndo.Count - 1].SetActive(true);

            undoRedo.lGOUndo.RemoveAt(undoRedo.lGOUndo.Count - 1);
            undoRedo.lActionsGOUndo.RemoveAt(undoRedo.lActionsGOUndo.Count - 1);
        }
        else if (undoRedo.lActionsGOUndo[undoRedo.lActionsGOUndo.Count - 1].Contains("borra"))
        {
            undoRedo.lGO.Add(undoRedo.lGOUndo[undoRedo.lGOUndo.Count - 1]);
            undoRedo.lActionsGO.Add(undoRedo.lActionsGOUndo[undoRedo.lActionsGOUndo.Count - 1]);

            undoRedo.lGOUndo[undoRedo.lGOUndo.Count - 1].SetActive(false);

            undoRedo.lGOUndo.RemoveAt(undoRedo.lGOUndo.Count - 1);
            undoRedo.lActionsGOUndo.RemoveAt(undoRedo.lActionsGOUndo.Count - 1);
        }
    }
    private async System.Threading.Tasks.Task WaitOneSecondAsync()
    {
        await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(0.2f));
        Debug.Log("Finished waiting.");
    }
}
