using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
    public Shader shader;
    //public Texture texture;
    public GameObject block;
    public int spawnedCount;
    private float currentTime, targetTime;
    private bool spawn;
    private Color[] sequence;
    private int colseq = 0;


    private Renderer blockrenderer;
    private void Start()
    {
        
        sequence = new Color[3];
        sequence[0] = Color.red;
        sequence[1] = Color.yellow;
        sequence[2] = Color.blue;
        spawnedCount = 0;
        targetTime = 5.0f;
        spawn = true;
        currentTime = targetTime;
    }
    private void FixedUpdate()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0.0f)
        {
            timerEnded();
        }

        if (spawn)
        {
            //block.GetComponent<Renderer>().material = new Material(shader);
            //block.GetComponent<Renderer>().material.mainTexture = texture;
            

            if (colseq == 2)
            {
                colseq = 0;
            }
            else
            {
                colseq++;
            }

            Vector3 spawnposition = new Vector3(transform.position.x+Random.Range(-spawnedCount, spawnedCount+1), transform.position.y, transform.position.z);
            GameObject temp = Instantiate(block, spawnposition, this.transform.rotation);
            blockrenderer = temp.transform.GetComponent<Renderer>();
            blockrenderer.material.color = sequence[colseq];
            
            
            if (spawnedCount < 4)
            {
                spawnedCount++;
            }

            spawn = false;
        }
    }
    private void timerEnded()
    {
        spawn = true;
        currentTime = targetTime;
    }
}

/*public class Spawner : MonoBehaviour
{
    public Vector3[] spawns;//raycast spawn points
    public Vector3[] sp;//block spawn points
    public GameObject block, origin;
    private bool spawn = false;
    public bool straight = true, diagonal = true;
    public float targetTime = 5.0f;


    public GameObject target;
    private void Start()
    {
        spawns = new Vector3[8];
        spawns[0] = new Vector3(1.0f, transform.position.y, transform.position.z);//straight
        spawns[1] = new Vector3(-1.0f, transform.position.y, transform.position.z);//straight
        spawns[2] = new Vector3(transform.position.x, 1.0f, transform.position.z);//straight
        spawns[3] = new Vector3(transform.position.x, -1.0f, transform.position.z);//straight
        spawns[4] = new Vector3(1.0f, 1.0f, transform.position.z);//diagonal
        spawns[5] = new Vector3(-1.0f, 1.0f, transform.position.z);//diagonal
        spawns[6] = new Vector3(-1.0f, -1.0f, transform.position.z);//diagonal
        spawns[7] = new Vector3(1.0f, -1.0f, transform.position.z);//diagonal


        sp = new Vector3[3];
        sp[0] = new Vector3(origin.transform.position.x, origin.transform.position.y, 0.0f);
        sp[1] = new Vector3(origin.transform.position.x+1, origin.transform.position.y, 0.0f);
        sp[2] = new Vector3(origin.transform.position.x-1, origin.transform.position.y, 0.0f);

        Instantiate(block, sp[0], origin.transform.rotation);
    }

    private void Update()
    {
        targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;
        if (spawn)
        {
            int count = 0;

            //Straight
            for (int i = 0; i < 4; i++)
            {
                if (Physics.Raycast(spawns[i], transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
                {
                    Debug.DrawRay(spawns[i], transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    Debug.Log(hit.transform.tag);
                    if (hit.transform.CompareTag("Outer"))
                    {
                        count++;
                    }
                }
                else
                {
                    Debug.DrawRay(spawns[i], transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                }
            }

            if (count == 4)
            {
                straight = false;
            }
            else
            {
                straight = true;
            }

            count = 0;
            //Diagonal
            for (int i = 4; i < 8; i++)
            {
                if (Physics.Raycast(spawns[i], transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
                {
                    Debug.DrawRay(spawns[i], transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    Debug.Log(hit.transform.tag);
                    if (hit.transform.CompareTag("Outer"))
                    {
                        count++;
                    }
                }
                else
                {
                    Debug.DrawRay(spawns[i], transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                }
            }

            if (count == 4)
            {
                diagonal = false;
            }
            else
            {
                diagonal = true;
            }

            if (straight && diagonal)
            {
                Instantiate(block, sp[Random.Range(0, 3)], origin.transform.rotation);
            }
            
            if(straight && !diagonal)
            {
                Instantiate(block, sp[0], origin.transform.rotation);
            }

            if (!straight && diagonal)
            {
                Instantiate(block, sp[Random.Range(1,3)], origin.transform.rotation);
            }
            spawn = !spawn;

            //if (Physics.Raycast(spawns[1], transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            //{
            //    Debug.DrawRay(spawns[1], transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //    Instantiate(block, spawns[Random.Range(0, 3)], this.transform.rotation);
            //    spawn = !spawn;
            //}
            //else
            //{
            //    Debug.DrawRay(spawns[1], transform.TransformDirection(Vector3.forward) * 1000, Color.white);

            //    if (Physics.Raycast(spawns[2], transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            //    {
            //        Debug.DrawRay(spawns[2], transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //        Instantiate(block, spawns[Random.Range(0, 3)], this.transform.rotation);
            //        spawn = !spawn;
            //    }
            //    else
            //    {
            //        Debug.DrawRay(spawns[2], transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //        Debug.DrawRay(spawns[0], transform.TransformDirection(Vector3.forward) * 1000, Color.yellow);
            //        Instantiate(block, spawns[0], this.transform.rotation);
            //        spawn = !spawn;
            //    }
            //}
        }
    }

    private void timerEnded()
    {
        spawn = true;
        targetTime = 5.0f;
    }
    private void FixedUpdate()
    {
        
    }
}*/
