using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MaterialInstance : MonoBehaviour
{
    public Color outlineColor = Color.white;
    private Material material;

    private void OnValidate()
    {
        material = GetComponent<Renderer>().material;
        material.SetColor("_Outline_Color", outlineColor);
    }
}
