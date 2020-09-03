using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

public class SaveLoad : MonoBehaviour
{
    public UndoRedoController ur;
    public GameObject[] prefabs;
    public Transform parent;
    public GameObject parentInicio;
    public FlexibleColorPicker colorPicker;
    private ArrastraGOSinDuplicar script;
    private string filePath;
    public Util util;
    public Animator animPanelSemiCirculo;
    public GameObject panelSettings;
    public string folderPath;
    public string fileToLoad;
    public MapaGuardado panelMapaGuardado;
    public GameObject panelCargarArchivo;
    public DialogYesNo dialog;
    // Start is called before the first frame update
    void Awake()
    {
        parentInicio = new GameObject();
        parentInicio.name = "parentInicio";
        parentInicio.transform.position = parent.transform.position;
        folderPath = (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer ? Application.persistentDataPath : Application.dataPath) + "/maps/";

    }

    public void Save()
    {
        // var objetos = new ListaObjetos();
        // objetos.Objetos = new Objeto[ur.lGO.Count];
        // var count = 0;
        panelSettings.SetActive(false);
        parent.position = parentInicio.transform.position;
        var filePath = creaArchivo("map");
        var count = 0;
        List<Objeto> lObjs = new List<Objeto>();
        foreach (var u in ur.lGO)
        {            
            var ob = new Objeto();
            ob.Tipo = u.name;
            ob.PositionX = u.transform.position.x;
            ob.PositionY = u.transform.position.y;
            ob.Size = u.transform.localScale.x;        
            if(ob.Tipo.Contains("ob_clone"))
            {
                var sr = u.GetComponent<SpriteRenderer>();
                ob.spriteName = sr.sprite.name;
                var argo = sr.GetComponent<ArrastraGOSinDuplicar>();
                if(argo.liineasColor == Color.red)
                {
                    ob.isEnemy = true;
                }
                else
                {
                    ob.isEnemy = false;
                }
            }
            if(u.gameObject.GetComponent<LineRenderer>() != null)
            {
                var lr = u.gameObject.GetComponent<LineRenderer>();
                Vector3[] positions = new Vector3[lr.positionCount];
                ob.PointsX = new List<float>();
                ob.PointsY = new List<float>();
                var o = lr.GetPositions(positions);
                for (int i=0;i< lr.positionCount;i++)
                {
                    ob.PointsX.Add(positions[i].x);
                    ob.PointsY.Add(positions[i].y);
                }
            }

            if (u.gameObject.GetComponent<Renderer>() != null &&
                u.gameObject.GetComponent<Renderer>().material != null &&
                u.name.Contains("DrawLine"))
            {
                ob.Color = new List<float>();
                var renderer = u.gameObject.GetComponent<Renderer>();
                var color = renderer.material.color;
                ob.Color.Add(color.r);
                ob.Color.Add(color.g);
                ob.Color.Add(color.b);
                ob.Color.Add(color.a);
            }
            if (u.gameObject.GetComponent<TextMeshPro>() != null)
            {
                var text = u.gameObject.GetComponent<TextMeshPro>();
                if (!string.IsNullOrWhiteSpace(text.text))
                {
                    ob.Text = text.text;
                    ob.Color = new List<float>();
                    var color = text.color;
                    ob.Color.Add(color.r);
                    ob.Color.Add(color.g);
                    ob.Color.Add(color.b);
                    ob.Color.Add(color.a);
                }
            }
            
           // objetos.Objetos[count] = ob;
           // count++;
            string jsonOb = JsonUtility.ToJson(ob);
            print(jsonOb);
            if(lObjs.Count>0)
            {
                bool ok = true;
                foreach (var o in lObjs)
                {
                    if (!ob.Tipo.Contains("DrawLine") && o.PositionX.Equals(ob.PositionX) && o.PositionY.Equals(ob.PositionY))
                    {
                        ok = false;
                        break;
                    }
                }
                if(ok)
                {
                    lObjs.Add(ob);
                    File.AppendAllText(filePath, jsonOb + "\n");
                }

            }
            else
            {
                lObjs.Add(ob);
                File.AppendAllText(filePath, jsonOb + "\n");
            }
            count++;
        }
        var split = filePath.Split('/');
        panelMapaGuardado.Text = split[split.Length - 1];
        panelMapaGuardado.Title = "Mapa guardado";
        panelMapaGuardado.gameObject.SetActive(true);

    }

    private string creaArchivo(string nombre)
    {        
        var fileName = nombre;        

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        var filesCount = Directory.GetFiles(folderPath).Length;
        var filePath = folderPath + fileName+filesCount + ".lolmap";
        while(File.Exists(filePath))
        {
            filesCount++;
            filePath = folderPath + fileName + filesCount + ".lolmap";
        }
        print(filePath);
        File.WriteAllText(filePath, "");
        return filePath;
    }
    private async System.Threading.Tasks.Task WaitOneSecondAsync()
    {
        await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(0.01f));
        Debug.Log("Finished waiting.");
    }
    public async void Load()
    {
        panelSettings.SetActive(false);
        animPanelSemiCirculo.SetBool("up", false);
        ur.ClearAllLGO();
        for (int i = 0; i < parent.gameObject.transform.childCount; i++)
        {
            Transform t = parent.gameObject.transform.GetChild(i);
            Destroy(t.gameObject);
        }
        parent.position = parentInicio.transform.position;
        
       // var fileName = "map";
        var filePath = this.fileToLoad;
        this.filePath = filePath;
        S_Util.mapaCargando = this.fileToLoad;
       // var filePath = folderPath + fileName + ".lolmap";
        var lines = File.ReadAllLines(filePath);
        Vector3 lastGOPosition = Vector3.zero;
        Vector3 lastGOScale = Vector3.zero;
        string lastGOName = "";
        foreach (var l in lines)
        {
            if (!S_Util.mapaCargando.Equals(filePath)) return;
            var ob = JsonUtility.FromJson<Objeto>(l);
            GameObject go;
            if (l.Equals(lastGOName) && Vector3.Distance(new Vector3(ob.PositionX, ob.PositionY, -1), lastGOPosition) == 0 && 
                Vector3.Distance(new Vector3(ob.Size, ob.Size, 1), lastGOScale) == 0)
                continue;
           // print(l.Equals(lastGOName) + "," + Vector3.Distance(new Vector3(ob.PositionX, ob.PositionY, -1), lastGOPosition) + "," + 
               // Vector3.Distance(new Vector3(ob.Size, ob.Size, 1), lastGOScale));
            lastGOName = l;
            lastGOScale = new Vector3(ob.Size, ob.Size, 1);
            lastGOPosition = new Vector3(ob.PositionX, ob.PositionY, -1);
            //print(ob.Tipo);
            
            switch (ob.Tipo)
            {
                case "ob_clone":                    
                    go = Instantiate(prefabs[0], Vector3.zero, Quaternion.identity,parent);                    
                    go.transform.position = new Vector3(lastGOPosition.x, lastGOPosition.y, 0);
                    go.transform.localScale = lastGOScale;
                    go.gameObject.name = "ob_clone";
                    ur.lGO.Add(go.gameObject);
                    ur.lGO.Add(go.gameObject);

                    var position = go.transform.position.ToString();
                    position = position.Replace("(", "");
                    position = position.Replace(")", "");
                    ur.lActionsGO.Add("crea," + position);
                    ur.lActionsGO.Add("mueve," + position);
                    script = go.GetComponent<ArrastraGOSinDuplicar>();
                    var sr = this.gameObject.GetComponent<SpriteRenderer>();
                    Sprite sprite = sr.sprite;
                    foreach (var u in util.lSpriteChamps)
                    {
                        if(u.name.Equals(ob.spriteName))
                        {
                            sprite = u;
                        }
                    }
                    script.image = sprite;
                    if(ob.isEnemy)
                    {
                        script.liineasColor = Color.red;
                    }
                    else
                    {
                        script.liineasColor = Color.blue;
                    }
                    Invoke("clickMouseUp", 0.01f);
                    break;
                case "DrawLine(Clone)":
                    colorPicker.color=new Color(ob.Color[0], ob.Color[1], ob.Color[2], ob.Color[3]);
                    await WaitOneSecondAsync();
                    if (!S_Util.mapaCargando.Equals(filePath)) return;
                    go = Instantiate(prefabs[1], Vector3.zero, Quaternion.identity, parent);
                    go.transform.position = lastGOPosition;
                    go.transform.localScale = lastGOScale;
                    print(ob.Color[0]+","+ ob.Color[1] + "," + ob.Color[2] + "," + ob.Color[3]);
                    var lineRend = go.GetComponent<LineRenderer>();
                    var positions = new Vector3[ob.PointsX.Count];
                    var drawLine = go.GetComponent<DrawLine2D>();
                    drawLine.isActive = false;
                    lineRend.positionCount = ob.PointsX.Count;
                    for (int i=0;i<ob.PointsX.Count;i++)
                    {
                        positions[i] = new Vector3(ob.PointsX[i], ob.PointsY[i], 1);
                        lineRend.SetPosition(i, positions[i]);
                    }
                    var position2 = "";
                    if (ur.lActionsGO.Count > 0)
                    {
                        var split = ur.lActionsGO[ur.lActionsGO.Count - 1].Split(',');
                        if (split.Length > 2)
                        {
                            position2 = $"{split[1]},{split[2]},{split[3]}";
                        }
                    }
                    ur.lGO.Add(go.gameObject);
                    ur.lActionsGO.Add("pinta," + position2);
                    break;
                case "Text(Clone)":
                    go = Instantiate(prefabs[2], Vector3.zero, Quaternion.identity,parent);
                    go.transform.position = lastGOPosition;
                    go.transform.localScale = lastGOScale;
                    var textMesh = go.GetComponent<TextMeshPro>();
                    textMesh.text = ob.Text;
                    textMesh.color = new Color(ob.Color[0], ob.Color[1], ob.Color[2], ob.Color[3]);
                    ur.lGO.Add(go.gameObject);
                    var position3 = go.transform.position.ToString();
                    position3 = position3.Replace("(", "");
                    position3 = position3.Replace(")", "");
                    ur.lActionsGO.Add("crea," + position3);
                    script = go.GetComponent<ArrastraGOSinDuplicar>();
                    Invoke("clickMouseUp", 0.01f);
                    break;
            }
            
        }
    }
    void clickMouseUp()
    {
        if (!S_Util.mapaCargando.Equals(this.filePath)) return;
        script.OnMouseUp();
    }
    public void Click_CargarMapa()
    {
        panelSettings.SetActive(false);
        panelCargarArchivo.SetActive(true);
    }

    public void Click_LimpiarMapa()
    {
        dialog.Text = "Esta acción no se puede deshacer ¿Está seguro?";
        dialog.Action = "limpiarMapa,";
        dialog.gameObject.SetActive(true);
       // dialog.ob = this;
        panelSettings.SetActive(false);
        var rt = GameObject.FindGameObjectWithTag("MenuObjetos").GetComponent<RectTransform>();
        rt.localScale = Vector3.zero;
    }

}
