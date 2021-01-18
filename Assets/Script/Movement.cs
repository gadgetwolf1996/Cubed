using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject blockprefab, block;
    public bool[,] locations;
    private List<Vector3> shape;

    //death material
    public Material death;

    private int move = 1;
    Rigidbody RB;
    float Speed;
    private bool trigger = true, onstart = true;

    private void Start()
    {
        locations = new bool[3, 3];
        locations[1, 1] = true;
        //Y +1 to -1
        /*locations[0, 0] = new Vector3(transform.position.x - 1, transform.position.y + 1, transform.position.z);//Diagonal
        locations[1, 0] = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);//Straight
        locations[2, 0] = new Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z);//Diagonal

        locations[0, 1] = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);//Straight
        locations[1, 1] = new Vector3(transform.position.x, transform.position.y, transform.position.z);//Central
        locations[2, 1] = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);//Straight

        locations[0, 2] = new Vector3(transform.position.x - 1, transform.position.y - 1, transform.position.z);//Diagonal
        locations[1, 2] = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);//Straight
        locations[2, 2] = new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z);//Diagonal*/

        RB = GetComponent<Rigidbody>();
        Speed = 5.0f;
    }
    void Update()
    {
        if (onstart)
        {
            Vector3 nextLoc = this.transform.position;
            int xoffset = 0, yoffset = 0, altervar = 0;
            for (int i = 0; i < Random.Range(2,10); i++)
            {
                int a, b;
                locations[a = Random.Range(0, 2), b = Random.Range(0, 2)] = true;
                if (locations[a, b] == true)
                {
                    Debug.Log(a + ", " + b + "is true");
                }
            }

            //Check connections
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    if (x == 0)
                    {
                        if(y == 0)
                        {
                            if (locations[x, y] == true && (locations[x + 1, y] == false && locations[x, y + 1] == false))
                            {
                                locations[x, y] = false;
                            }
                        }

                        if(y == 1)
                        {
                            if (locations[x, y] == true && (locations[x + 1, y] == false && locations[x, y -1] == false && locations[x, y + 1] == false))
                            {
                                locations[x, y] = false;
                            }
                        }

                        if(y == 2)
                        {
                            if (locations[x, y] == true && (locations[x + 1, y] == false && locations[x, y - 1] == false))
                            {
                                locations[x, y] = false;
                            }
                        }
                    }

                    if (x == 1)
                    {
                        if (y == 0)
                        {
                            if (locations[x, y] == true && (locations[x - 1, y] == false && locations[x + 1, y] == false && locations[x, y + 1] == false))
                            {
                                locations[x, y] = false;
                            }
                        }

                        if (y == 1)
                        {
                            //centre
                        }

                        if (y == 2)
                        {
                            if (locations[x, y] == true && (locations[x - 1, y] == false && locations[x + 1, y] == false && locations[x, y - 1] == false))
                            {
                                locations[x, y] = false;
                            }
                        }


                    }

                    if (x == 2)
                    {
                        if (y == 0)
                        {
                            if (locations[x, y] == true && (locations[x - 1, y] == false && locations[x, y + 1] == false))
                            {
                                locations[x, y] = false;
                            }
                        }

                        if (y == 1)
                        {
                            if (locations[x, y] == true && (locations[x - 1, y] == false && locations[x, y - 1] == false && locations[x, y + 1] == false))
                            {
                                locations[x, y] = false;
                            }
                        }

                        if (y == 2)
                        {
                            if (locations[x, y] == true && (locations[x - 1, y] == false && locations[x, y - 1] == false))
                            {
                                locations[x, y] = false;
                            }
                        }
                    }
                }
            }

            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    if (locations[x,y] == true && x != y)
                    {
                        xoffset = (x - 1) * -1;
                        yoffset = (y - 1) * -1;
                        nextLoc = new Vector3(this.transform.position.x + xoffset, this.transform.position.y + yoffset, transform.position.z);
                        block = Instantiate(blockprefab, nextLoc, this.transform.rotation);
                        block.GetComponent<Renderer>().material = this.GetComponent<Renderer>().material;
                        block.transform.SetParent(this.transform);
                    }
                }
            }
            onstart = false;
            move = 0;
        }

        switch (move)
        {
            case 0:
                RB.velocity = transform.forward * Speed;
                break;
            case 1:
                break;
            case 2:
                RB.velocity = transform.up * Speed;
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Falling")
        {
            GameObject other = collision.gameObject;
            GameObject otherParent = GameObject.FindGameObjectWithTag("Player");
                //other.transform.parent.gameObject;

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<BoxCollider>().size = new Vector3(1.0f, 1.0f, 1.0f);
                //transform.GetChild(i).transform.localRotation = Quaternion.identity;
                //transform.GetChild(i).transform.localPosition = new Vector3((int)transform.localPosition.x, (int)transform.localPosition.y, (int)transform.localPosition.z);
                //transform.GetChild(i).transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
                transform.GetChild(i).tag = "Outer";
                //transform.GetChild(i).transform.SetParent(otherParent.transform);
            }

            this.gameObject.GetComponent<BoxCollider>().size = new Vector3(1.0f, 1.0f, 1.0f);
            transform.Rotate(0, 0, 0);
            transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
            Destroy(this.gameObject.GetComponent<Rigidbody>());
            transform.SetParent(otherParent.transform);
            move = 1;
            this.tag = "Outer";
            otherParent.GetComponent<FollowTouch>().command = true;
            if (trigger)
            {
                otherParent.GetComponent<CombineScript>().AddScore();
                trigger = false;
            }
        }
        //switch (collision.transform.tag)
        //{
        //    //case "Outer":
        //    //    Destruction();
        //    //    break;
        //    case "Core":

        //        break;
        //    default:
        //        break;
        //}
    }

    private void Destruction()
    {
        move = 2;
        this.gameObject.GetComponent<MeshRenderer>().material = death;
        //this.gameObject.transform.localScale /= 2;
        this.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -2.0f);
        StartCoroutine(Delayed());
    }

    IEnumerator Delayed()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
