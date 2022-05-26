using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MaterialInstance : MonoBehaviour
{
    public Color outlineColor = Color.white;
    private Material material;

    /// <summary>
    /// Editor-only function that Unity calls when the script is loaded or a value changes in the Inspector.
    /// Use this to perform an action after a value changes in the Inspector.
    /// </summary>
    private void OnValidate()
    {
        material = GetComponent<Renderer>().material;
        material.SetColor("_Outline_Color", outlineColor);
    }
}
