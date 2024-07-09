using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public static class Vector3extensions
{
    public static Vector3 randomDisplaced(this Vector3 a, float maxAmount)
    {
        var xDisplacement = Random.Range(0, maxAmount);
        var yDisplacement = Random.Range(0, maxAmount);
        var zDisplacement = Random.Range(0, maxAmount);
        return new Vector3(a.x + xDisplacement, a.y + yDisplacement, a.z + zDisplacement);
    }
    public static float randomDisplaced(this float a, float maxAmount)
    {
        return Random.Range(0, maxAmount);
    }
}
public class Manager : MonoBehaviour
{
    public Camera camera;
    public bool ThreeD;
    public GameObject DotPrefab;
    public GameObject SpherePrefab;
    public LineRenderer LR;
    public int DotsNum;
    List<GameObject> Dots = new List<GameObject>();
    List<GameObject> Spheres = new List<GameObject>();
    int CurrentDot = 0;
    float foundRadius = 0;
    public float randomDisplacement = 2f;

    int linePointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        //initialize camera 
        if (!ThreeD)
        {
            camera.orthographic = true;
        }

        InstantiateDots();

        AddLinePoint(0);

        StartCoroutine(Operate());


    }

    // Update is called once per frame
    void Update()
    {

    }
    void SelectNext()
    {
        Dots[CurrentDot].GetComponent<Dot>().SelectDot();
        Dots[CurrentDot].GetComponent<Dot>().SetLabel((CurrentDot + 1).ToString());


        //if not enclosing
        if (!encloses(Dots[CurrentDot].transform.position))
        {
            AddLinePoint(CurrentDot);

            if (CurrentDot > 0)
            {
                var radius = -Dots[CurrentDot].transform.position + Dots[0].transform.position;
                Debug.Log($"radius {radius.magnitude}");

                createSphere(Dots[0].transform.position, radius.magnitude);
            }
        }
        else
        {
            Debug.Log($"not enclosing {CurrentDot}");
        }

        CurrentDot += 1;
    }

    private void AddLinePoint(int CurrentDot)
    {
        Debug.Log($"LINE POINT {CurrentDot}");
        LR.positionCount++;
        LR.SetPosition(linePointIndex++, Dots[CurrentDot].transform.position);
    }

    IEnumerator Operate()
    {
        for (int i = 0; i < Dots.Count; i++)
        {
            yield return new WaitForSeconds(1f);
            SelectNext();
        }
        highlightSphere(Spheres.Last());
    }

    void highlightSphere(GameObject S)
    {
        Material material = S.GetComponent<Renderer>().material;
        Color color = Color.yellow;
        color.a = .5f;
        material.color = color;
    }
    void InstantiateDots()
    {
        var positions = getPoisitions();
        for (int i = 0; i < DotsNum; i++)
        {
            var go = Instantiate(DotPrefab, positions[i], Quaternion.identity);
            Dots.Add(go);
        }
    }
    List<Vector3> getPoisitions()
    {
        var c = 0.3f * Vector3.up;
        var y = 0.3f * Vector3.up;

        //make displacement in all 3 axis in case of 3d
        if (ThreeD)
        {
            return Enumerable.Range(1, DotsNum).Select((x, index) => c.randomDisplaced(randomDisplacement) + Vector3.one * index * .1f).ToList();

        }
        return Enumerable.Range(1, DotsNum).Select((x, index) => new Vector3(c.x.randomDisplaced(randomDisplacement) + index * .1f, c.y.randomDisplaced(randomDisplacement) + index * .1f, 0)).ToList();
    }

    void createSphere(Vector3 center, float radius)
    {
        foundRadius = radius;
        var go = Instantiate(SpherePrefab, center, Quaternion.identity);
        go.transform.localScale = Vector3.one * radius * 2;
        decreaseSpheresOpa();
        Spheres.Add(go);
    }

    void decreaseSpheresOpa()
    {
        for (int i = 0; i < Spheres.Count - 1; i++)
        {
            Material material = Spheres[i].GetComponent<Renderer>().material;
            Color color = material.color;
            color.a *= .5f; // Decrease the opacity by 10%
            material.color = color;
        }
    }

    bool encloses(Vector3 position)
    {
        if (Dots.Count < 2)
            return false;

        var newDiameter = (position - Dots[0].transform.position).magnitude * 2;
        return newDiameter <= foundRadius * 2;
    }



}
