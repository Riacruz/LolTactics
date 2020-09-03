using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class CargarMapa : MonoBehaviour
{
    public SaveLoad saveLoad;
    List<string> lFiles = new List<string>();
    List<string> lFullFiles = new List<string>();
    // The position on of the scrolling viewport
    public Vector2 scrollPosition = Vector2.zero;
    public DialogYesNo dialog;
    bool start = false;
    private void Start()
    {
        loadFiles();
        
    }

    private void OnEnable()
    {
        loadFiles();
    }
    public void loadFiles()
    {
        start = false;
        lFullFiles.Clear();
        lFiles.Clear();
        if (!Directory.Exists(saveLoad.folderPath))
        {
            Directory.CreateDirectory(saveLoad.folderPath);
        }
        var totalFiles = Directory.GetFiles(saveLoad.folderPath);

        for (int i = 0; i < totalFiles.Length; i++)
        {
            var split = totalFiles[i].Split('.');
         //   print("Split: " + split[split.Length - 1] + " " + split[split.Length - 1].Trim().Equals("lolmap"));
            if (split[split.Length - 1].Trim().Equals("lolmap"))
            {
                lFullFiles.Add(totalFiles[i]);
                split = totalFiles[i].Split('/');
                lFiles.Add(split[split.Length - 1].Trim());
            }
        }
        start = true;
    }
    void OnGUI()
    {
        if (start)
        {
            // An absolute-positioned example: We make a scrollview that has a really large client
            // rect and put it in a small rect on the screen.
            var rt = GameObject.FindGameObjectWithTag("Canvas").gameObject.GetComponent<RectTransform>();
            var btnsCount = lFiles.Count;
            var ratioX = Screen.width / 743;
            var ratioY = Screen.height / 418;
            var ratioFont = 18 * ratioX;
            GUIStyle style = new GUIStyle(GUI.skin.button)
            {
                alignment = TextAnchor.MiddleCenter,
                margin = new RectOffset(),
                padding = new RectOffset(),
                fontSize = ratioFont,
                fontStyle = FontStyle.Normal
            };
           // print(Screen.width + "," + Screen.height);
            scrollPosition = GUI.BeginScrollView(new Rect(350*ratioX, 10*ratioY, 350*ratioX, 500 * ratioY), scrollPosition, new Rect(1*ratioX, 1*ratioY, 350 * ratioX, 27 * btnsCount*ratioY));

            for (int i = 0; i < btnsCount; i++)
            {
                try
                {
                    GUILayout.BeginHorizontal("box");
                    style.normal.textColor = Color.white;
                    if (GUILayout.Button(lFiles[i], style, GUILayout.Width(200*ratioX)))
                    {
                      //  Debug.Log("Clicked Button " + i + " " + lFiles[i]);
                        saveLoad.fileToLoad = lFullFiles[i];
                        saveLoad.Load();
                    }
                    //var style = new GUIStyle(GUI.skin.button);
                    style.normal.textColor = Color.red;
                    if (GUILayout.Button("X", style, GUILayout.Width(30*ratioX)))
                    {
                        Click_BorrarArchivo(lFullFiles[i]);
                        Debug.Log("Borrar");
                    }
#if UNITY_ANDROID
                    if (Application.platform == RuntimePlatform.Android)
                    {
                        style.normal.textColor = Color.blue;
                        if (GUILayout.Button("Share", style, GUILayout.Width(100 * ratioX)))
                        {
                            string filePath = Path.Combine(Application.persistentDataPath, lFiles[i]);
                            new NativeShare().AddFile(lFullFiles[i]).SetSubject("Mapa de LOL Tactics").SetText(lFiles[i]).Share();
                            Debug.Log("Borrar");
                        }
                    }
#endif                    
                    GUILayout.EndHorizontal();
                }
                catch (System.ArgumentOutOfRangeException) { }
            }

            // End the scroll view that we began above.
            GUI.EndScrollView();
        }
    }

    public void  Click_btnX()
    {
        this.gameObject.SetActive(false);
    }
    
    public void Click_BorrarArchivo(string file)
    {
        var split = file.Split('/');
        dialog.Text = "Va a borrar "+split[split.Length-1]+" ¿Está seguro?";
        dialog.Action = "deleteFile,"+file;
        dialog.gameObject.SetActive(true);
        dialog.ob = this;
        this.gameObject.SetActive(false);
        //File.Delete(file);
        //loadFiles();
    }


}
