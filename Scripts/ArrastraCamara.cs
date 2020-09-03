using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrastraCamara : MonoBehaviour
{
    Vector3 screenPoint, offset;
    private void Start()
    {
    }
    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(Camera.main.transform.position);
        offset = Camera.main.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        print(offset);
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        Camera.main.transform.position = curPosition;
    }
}
