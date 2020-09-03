using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class btnScreenShot : MonoBehaviour
{
    public GameObject canvas;
    public GameObject parentInicio;
    public Transform mapa;
    public GameObject panelSettings;
    public GameObject panelSemiCirculo;
    public MapaGuardado mapaGuardado;
    public DialogYesNo dialog;
    
    // Start is called before the first frame update
    void Start()
    {
        parentInicio = new GameObject();
        parentInicio.name = "parentInicio";
        parentInicio.transform.position = mapa.transform.position;
    }

    public async void clickScreenShot()
    {
        var anim = panelSemiCirculo.GetComponent<Animator>();
        anim.SetBool("up", false);
        //panelSettings.SetActive(false);
        Camera.main.orthographicSize = 10;
        mapa.position = parentInicio.transform.position;
        panelSettings.SetActive(false);
        await WaitOneSecondAsync();
        canvas.SetActive(false);
        var nombreCaptura = "lolmapScreenshot.png";
        string filePath = Path.Combine(Application.persistentDataPath, nombreCaptura);
        ScreenCapture.CaptureScreenshot(nombreCaptura);
        mapaGuardado.Text = nombreCaptura;
        mapaGuardado.Title = "Captura guardada";        
        await WaitOneSecondAsync();

#if UNITY_IOS
        if (Application.platform == RuntimePlatform.IPhonePlayer)
            new NativeShare().AddFile(filePath).SetSubject("Captura de LOL Tactics").SetText("Captura de LOL Tactics").Share();
#endif

#if UNITY_ANDROID
        if (Application.platform == RuntimePlatform.Android)
            new NativeShare().AddFile(filePath).SetSubject("Captura de LOL Tactics").SetText("Captura de LOL Tactics").Share();
#endif

        canvas.SetActive(true);
        mapaGuardado.gameObject.SetActive(true);
    }
    private async System.Threading.Tasks.Task WaitOneSecondAsync()
    {
        await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1f));
    }
    public void Click_OpenPanelSettings()
    {
        panelSettings.SetActive(true);
    }
    public void Click_BackPanelSettings()
    {
        panelSettings.SetActive(false);
    }

    public void Click_Salir()
    {
        dialog.Text = "¿Salir de la aplicación?";
        dialog.aAction = "salir";
        dialog.gameObject.SetActive(true);
    }
}
