using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnsZoom : MonoBehaviour
{
    private Camera cam;
    private float valorZoom;
    public  ArrastraGOSinDuplicar arrastraGO;
    public Animator animPanelSemiCirculo;
    public GameObject panelMoving;
    //public Text btnMove;
    private void Start()
    {
        cam = Camera.main;
        valorZoom = 0.5f;

    }
    float lastDistance = 0;
    private void Update()
    {
        if (Input.touchCount >= 2)
        {
            Vector2 touch0, touch1;
            float distance;
            touch0 = Input.GetTouch(0).position;
            touch1 = Input.GetTouch(1).position;
            distance = Vector2.Distance(touch0, touch1);    
            if(distance-lastDistance!=distance)
            if(distance-lastDistance>0)
            {
                clickZoomMas();
            }
            else if (distance - lastDistance < 0)
            {
                clickZoomMenos();
            }
            lastDistance = distance;
        }
        else
        {
            lastDistance = 0;
        }
    }
    public void clickZoomMas()
    {
        //hace con pinch zoom para android (pellizcar los dedos)
        if (cam.orthographicSize>1)
        cam.orthographicSize = cam.orthographicSize - valorZoom;
        var menuObjeto = GameObject.FindGameObjectWithTag("MenuObjetos");
        menuObjeto.GetComponent<RectTransform>().localScale = Vector3.zero;
    }
    public void clickZoomMenos()
    {
        cam.orthographicSize = cam.orthographicSize + valorZoom;
        var menuObjeto = GameObject.FindGameObjectWithTag("MenuObjetos");
        menuObjeto.GetComponent<RectTransform>().localScale = Vector3.zero;
    }
    public void clickMove()
    {
        animPanelSemiCirculo.SetBool("up", false);
        panelMoving.SetActive(true);
        if (arrastraGO.isActive)
        {
           // btnMove.text = "Move";
            arrastraGO.isActive = false;
        }
        else
        {
            //btnMove.text = "Moving";
            arrastraGO.isActive = true;
        }
        var menuObjeto = GameObject.FindGameObjectWithTag("MenuObjetos");
        menuObjeto.GetComponent<RectTransform>().localScale = Vector3.zero;
    }


}
