using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class ListaObjetos
{
    public Objeto[] Objetos;
}

[SerializeField]
public class Objeto
{
    public string Tipo;
    public float PositionX;
    public float PositionY;
    public float Size;
    public List<float> PointsX; 
    public List<float> PointsY; 
    public List<float> Color;
    public string Text;
    public string spriteName;
    public bool isEnemy;

}

