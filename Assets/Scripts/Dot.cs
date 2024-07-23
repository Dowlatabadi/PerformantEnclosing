using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public static Color initialColor=Color.red;
    public static Color selectedColor=Color.blue;
    public Material objectMaterial;
    public TextMeshPro textMesh;
    // Start is called before the first frame update
    void Start()
    {
        objectMaterial = GetComponent<Renderer>().material;
        // Change the color
        objectMaterial.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectDot()
    {
        objectMaterial.color = Color.red;
        transform.localScale = 1.2f* transform.localScale;
    }
    public void SetLabel(string label)
    {
        textMesh.text = label;
        textMesh.SetText(label);
    }
}
