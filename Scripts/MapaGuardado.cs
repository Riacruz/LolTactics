using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapaGuardado : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public TextMeshProUGUI textMeshTitle;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Desactiva", 3);
    }

    public string Text
    {
        get { return textMesh.text; }
        set
        {
            textMesh.text = value;
        }
    }

    public string Title
    {
        get { return textMeshTitle.text; }
        set
        {
            textMeshTitle.text = value;
        }
    }

   

    private void OnEnable()
    {
        Invoke("Desactiva", 3);
    }

    public void Desactiva()
    {
        this.gameObject.SetActive(false);
    }
}
