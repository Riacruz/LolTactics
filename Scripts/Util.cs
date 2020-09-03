using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Util : MonoBehaviour
{
    public Sprite[] lSpriteChamps;
    public Animator animPanel;
    void Start()
    {
        lSpriteChamps = Resources.LoadAll<Sprite>("Sprites");
        Sprite[] sp2 = Resources.FindObjectsOfTypeAll(typeof(Sprite)) as Sprite[];
       
        foreach (var s in lSpriteChamps)
        {
            //print(lSpriteChamps.Length + "," + s.name);
        }
    }

    public static bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }
    ///Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }
        return false;
    }
    ///Gets all event systen raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }

    private void Update()
    {
        if (IsPointerOverUIElement() && S_Util.isDragNow)
        {
            animPanel.SetBool("up",false);
            print(IsPointerOverUIElement());
        }
        else
        {
           
           //imagePanel.color = new Color(255f, 255f, 255f, 255f);
          
        }
    }

}
