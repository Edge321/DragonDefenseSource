using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashAnimation : MonoBehaviour
{
    public float rotateSpeed;
    public float rotateTimer;

    private float actualTimer;
    private float scaleSpeed = 0.0005f;

    private bool switchAnimation = true;

    private Vector3 rotatingVector;
    private Vector3 scaleVector;
    // Start is called before the first frame update
    void Start()
    {
        rotatingVector = new Vector3(0, 0, rotateSpeed); //How fast the text rotates 
        scaleVector = new Vector3(scaleSpeed, scaleSpeed, scaleSpeed); //How fast text scales

        actualTimer = rotateTimer;
    }
    // FixedUpdate is called once per fixed frame
    void FixedUpdate()
    {
        actualTimer -= Time.deltaTime;

        if (actualTimer < 0)
        {
            SwitchBoolean();
            actualTimer = rotateTimer;
        }

        ChangeAnimation();
    }
    /// <summary>
    /// Changes the boolean for when it is time for the text to change animation
    /// </summary>
    private void SwitchBoolean()
    {
        if (switchAnimation)
            switchAnimation = false;
        else
            switchAnimation = true;
    }
    /// <summary>
    /// Checks if text should change behavior of rotation and scaling
    /// </summary>
    private void ChangeAnimation()
    {
        if (switchAnimation) 
        {
            transform.Rotate(rotatingVector);
            transform.localScale += scaleVector;
        }
        else
        {
            transform.Rotate(-rotatingVector);
            transform.localScale -= scaleVector;
        }
    }
}
