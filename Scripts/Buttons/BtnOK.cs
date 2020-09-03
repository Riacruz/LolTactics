using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BtnOK : MonoBehaviour
{
    public GameObject canvas;
    public TMP_InputField inputField;
    public Transform prefab;
    public UndoRedoController undoRedo;
    public Transform parent;
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void clickMouseUp()
    {

        script.OnMouseUp();
    }
    private ArrastraGOSinDuplicar script;
    public void clickOK()
    {
        string texto = inputField.text;
        if (string.IsNullOrWhiteSpace(texto))
        {
            canvas.SetActive(false);
            return;
        }
        else
        {
            var go = Instantiate(prefab, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 5)), Quaternion.identity, parent);
            var text = go.GetComponent<TextMeshPro>();
            text.text = inputField.text;
            inputField.text = "";
            text.color = material.color;
            undoRedo.lGO.Add(go.gameObject);
            var position = this.transform.localPosition.ToString();
            position = position.Replace("(", "");
            position = position.Replace(")", "");
            undoRedo.lActionsGO.Add("crea,"+position);
            script = go.GetComponent<ArrastraGOSinDuplicar>();
            Invoke("clickMouseUp", 0.01f);
            canvas.SetActive(false);
        }
    }
}
