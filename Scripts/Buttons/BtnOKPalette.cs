using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnOKPalette : MonoBehaviour
{
    public GameObject palette;
    public GameObject panelPinta;
    public GameObject panelTexto;
    public GameObject panelMoving;
    public ArrastraGOSinDuplicar arrastraGO;
    public Animator anim;

    private void Start()
    {
    }
    public void clickBtnOKPalette()
    {
        var rect = palette.GetComponent<RectTransform>();
        rect.position = new Vector4(5000,5000,5000,5000);

    }

    public void clickBtnOpenPalette()
    {
        var rect = palette.GetComponent<RectTransform>();
        rect.position = new Vector4(0.5f * Screen.width, 0.5f * Screen.height, 0, 0.5f * Screen.height);
        var menuObjeto = GameObject.FindGameObjectWithTag("MenuObjetos");
        menuObjeto.GetComponent<RectTransform>().localScale = Vector3.zero;
    }

    void panelesFalse()
    {
        
        panelPinta.SetActive(false);
        panelTexto.SetActive(false);
        panelMoving.SetActive(false);
        S_Util.isPainting = false;
    }
    public void btnSemiCirculo_click()
    {
        if (anim.GetBool("up"))
            anim.SetBool("up", false);
        else
        {
            panelesFalse();
            anim.SetBool("up", true);
        }
        arrastraGO.isActive = false;
        var menuObjeto = GameObject.FindGameObjectWithTag("MenuObjetos");
        menuObjeto.GetComponent<RectTransform>().localScale = Vector3.zero;
        S_Util.isMovingMap = false;
    }
    public void Click_MovingStop()
    {
        arrastraGO.isActive = false;
        var menuObjeto = GameObject.FindGameObjectWithTag("MenuObjetos");
        menuObjeto.GetComponent<RectTransform>().localScale = Vector3.zero;
        S_Util.isMovingMap = false;
        panelesFalse();
    }
}
