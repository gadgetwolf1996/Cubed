using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineScript : MonoBehaviour
{
    private int score = 1;
    public Text text;
    public int noChildren = 0;
    public int lastChildcount = 1;
    public Color Opacity;
    private void FixedUpdate()
    {
        noChildren = transform.childCount;
        if (transform.childCount == 9)
        {
            for (int i = 1; i < transform.childCount; i++)
            {
                Destroy(this.gameObject.transform.GetChild(i).gameObject);
            }
            Opacity.a += 0.10f;
            //this.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Opacity;
            lastChildcount = 1;
        }

        
    }

    private void Update()
    {
        //if (transform.childCount > lastChildcount)
        //{
        //    int temp = transform.childCount - 1;
        //    this.gameObject.transform.GetChild(temp).gameObject.transform.rotation = Quaternion.identity;

        //    this.gameObject.transform.GetChild(temp).gameObject.transform.position = new Vector3(Mathf.Round(this.gameObject.transform.GetChild(temp).gameObject.transform.position.x), Mathf.Round(this.gameObject.transform.GetChild(temp).gameObject.transform.position.y), 0.0f);

        //    //lastChildcount = transform.childCount;
        //}
    }

    public void AddScore()
    {
        score += 1;
        text.text = score.ToString();
    }
}
