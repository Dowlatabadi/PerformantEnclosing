using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float startScale = 0f; // The initial scale of the sphere
    public float endScale = 0f; // The initial scale of the sphere
    public float duration = 2f; // The duration of the animation

    private float timer = 0f; // A timer to control the animation
public void grow(float Scale,float animationDuration)
    {
        timer = 0f;
        duration = animationDuration;
        startScale = transform.localScale.x;
        endScale = Scale;
    }
    // Update is called once per frame
    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Calculate the current scale using lerp
        float currentScale = Mathf.Lerp(startScale, endScale, 2*timer / duration);

        // Apply the current scale to the sphere
        transform.localScale = new Vector3(currentScale, currentScale, currentScale);

        // Check if the animation is finished
        if (timer >= duration)
        {
            timer = duration; // Set the timer to the end of the animation
        }
    }
}
