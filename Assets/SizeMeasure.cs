using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeMeasure : MonoBehaviour
{
    private Vector3[] position;
    public int offset;
    public float[] lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        offset = 5;
        lastPosition = new float[2];
        lastPosition[0] = 0.0f;
        lastPosition[1] = 0.0f;

        position = new Vector3[2];
        position[0] = this.transform.position;
        position[1] = this.transform.position;
        position[0] = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
        position[1] = new Vector3(transform.position.x - offset, transform.position.y, transform.position.z);
        //offset = -1;
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;
        for (int i = 0; i < 2; i++)
        {
            if (Physics.Raycast(position[i], transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(position[i], transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if(position[i].x >=5 || position[i].x <= -5)
                {
                    TriggerDestroy();
                }
                //else
                //{
                //    //if(position[i].x == lastPosition[i])
                //    //{
                //    //    lastPosition[i] = position[i].x;
                //    //    offset++;
                //    //}
                //}
            }
            else
            {
                Debug.DrawRay(position[i], transform.TransformDirection(Vector3.forward) * Mathf.Infinity, Color.white);
            }
        }

        //position[0] = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
        //position[1] = new Vector3(transform.position.x - offset, transform.position.y, transform.position.z);
    }

    private void TriggerDestroy()
    {
        Debug.Log("Destroy triggered");
    }
}
