using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    private int  _widthIndex = 0;
    public int  WidthIndex
    {
        get
        {
            return _widthIndex;
        }

        set
        {
           _widthIndex = value;
        }  
    } 

    private int  _heightIndex = 0;
    public int  HeightIndex
    {
        get
        {
            return _heightIndex;
        }

        set
        {
           _heightIndex = value;
        }  
    } 

    private Vector2  _pos = Vector2.zero;
    public Vector2 Pos
    {
        get
        {
            return _pos;
        }

        set
        {
           _pos = value;
        }  
    }

    private HexType _type = HexType.Empty;
    public HexType Type
    {
        get
        {
            return _type;
        }

        set
        {
           _type = value;
        }  
    }

    private GameObject _occupyObject = null;
    public GameObject OccupyObject
    {
        get
        {
            return _occupyObject;
        }

        set
        {
           _occupyObject = value;
        }  
    }
}

public enum HexType
{
    Empty,
    Element,
}


