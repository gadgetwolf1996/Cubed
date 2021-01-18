using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTouch : MonoBehaviour
{
    private Touch touch;
    private Vector2 touchPosition;
    private Quaternion rotationZ;
    private float rotateSpeedModifier = 0.3f;
    public int increment = 90;


    public Vector2 startPos;
    public Vector2 direction;
    public bool touching = false, command = true;

    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        command = false;
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for(var t = 0f; t <= 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        transform.rotation = toAngle;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, (Mathf.Round(transform.eulerAngles.z / 90) * 90));
        direction = new Vector2(0.0f, 0.0f);
        command = true;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && command)
        {
            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    break;

                case TouchPhase.Ended:
                    break;
            }
        }

        if (command)
        {
            if (direction.y > 0 || Input.GetKeyDown(KeyCode.W))
            {
                StartCoroutine(RotateMe(Vector3.forward * 90, 0.2f));
                
            }
            else if (direction.y < 0 || Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(RotateMe(Vector3.forward * -90, 0.2f));
            }
        }
        else
        {
            direction = new Vector2(0.0f, 0.0f);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Falling"))
        {
            command = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Falling"))
        {
            command = true;
        }
    }
}
