using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawLine2D : MonoBehaviour
{
    public LineRenderer m_LineRenderer;
    public bool m_AddCollider = false;
    protected EdgeCollider2D m_EdgeCollider2D;
    private Camera m_Camera;
    protected List<Vector2> m_Points;
    public bool isActive = false;
    private bool dibujando = false;
    //private bool lineUsada = false;
    public Material material;
    public Color color;
    private bool siguiente = true;
   // public Transform pelota;

    public virtual LineRenderer lineRenderer
    {
        get
        {
            return m_LineRenderer;
        }
    }

    public virtual bool addCollider
    {
        get
        {
            return m_AddCollider;
        }
    }

    public virtual EdgeCollider2D edgeCollider2D
    {
        get
        {
            return m_EdgeCollider2D;
        }
    }
    

    public virtual List<Vector2> points
    {
        get
        {
            return m_Points;
        }
    }

    protected virtual void Awake()
    {
        if (m_LineRenderer == null)
        {
            //Debug.LogWarning("DrawLine: Line Renderer not assigned, Adding and Using default Line Renderer.");
            CreateDefaultLineRenderer();
        }
        
        if (m_EdgeCollider2D == null && m_AddCollider)
        {
            //Debug.LogWarning("DrawLine: Edge Collider 2D not assigned, Adding and Using default Edge Collider 2D.");
            CreateDefaultEdgeCollider2D();
        }
        
        if (m_Camera == null)
        {
            m_Camera = Camera.main;
        }
        m_Points = new List<Vector2>();
    }
    private void Start()
    {
        Camera m_Camera = Camera.main;
        // material.color = new Color(0, 0, 0, 0);
        //SphereCollider sc = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
        Material mat = new Material(material);
        Renderer rend = GetComponent<Renderer>();
        rend.material = mat;


    }
    protected virtual void Update()
    {
        // m_Camera.transform.position = new Vector3(m_Camera.transform.position.x, pelota.position.y, m_Camera.transform.position.z);
       
        
        if (Input.GetMouseButtonUp(0) && dibujando)
        {
            dibujando = false;
            isActive = false;
            var outLine = this.GetComponent<cakeslice.Outline>();
            outLine.color = -1;

        }
        if (Input.GetMouseButtonDown(0) && !isActive && !dibujando && siguiente)
        {
            if (S_Util.isPainting)
            {
                if (EventSystem.current.currentSelectedGameObject && 
                    (EventSystem.current.currentSelectedGameObject.name.Equals("BtnSemiCirculo") ||
                    EventSystem.current.currentSelectedGameObject.name.Equals("BtnPinta")))
                    return;
                var pincel = GameObject.FindGameObjectWithTag("Pinta");
                var pinta = pincel.GetComponent<Pinta>();
                pinta.OnMouseUp();
                siguiente = false;
            }
        }

        if (Input.GetMouseButton(0) && isActive)
        {
            var outLine = this.GetComponent<cakeslice.Outline>();
            outLine.color = 0;
            dibujando = true;
            Vector2 mousePosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
            if (!m_Points.Contains(mousePosition))
            {
                m_Points.Add(mousePosition);
                m_LineRenderer.positionCount = m_Points.Count;
                m_LineRenderer.SetPosition(m_LineRenderer.positionCount - 1, mousePosition);
                
                if (m_EdgeCollider2D != null && m_AddCollider && m_Points.Count > 1)
                {
                    m_EdgeCollider2D.points = m_Points.ToArray();
                }
                
            }
        }
        
    }
    protected virtual void Reset()
    {
        if (m_LineRenderer != null)
        {
            m_LineRenderer.positionCount = 0;
        }
        if (m_Points != null)
        {
            m_Points.Clear();
        }
        if (m_EdgeCollider2D != null && m_AddCollider)
        {
            m_EdgeCollider2D.Reset();
        }
        
        Vector2[] puntos;
        puntos = new Vector2[2];
        puntos[0] = new Vector2(0, 0);
        puntos[1] = new Vector2(0, 0);
        m_EdgeCollider2D.points = puntos;

    }

    protected virtual void CreateDefaultLineRenderer()
    {
        m_LineRenderer = gameObject.AddComponent<LineRenderer>();
        m_LineRenderer.positionCount = 0;
        m_LineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        m_LineRenderer.startColor = Color.white;
        m_LineRenderer.endColor = Color.white;
        m_LineRenderer.startWidth = 0.2f;
        m_LineRenderer.endWidth = 0.2f;
        m_LineRenderer.useWorldSpace = true;
    }

    protected virtual void CreateDefaultEdgeCollider2D()
    {
        m_EdgeCollider2D = gameObject.AddComponent<EdgeCollider2D>();
        m_EdgeCollider2D.edgeRadius = 0.1f;
    }

}