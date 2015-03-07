using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEditorInternal;

public class InputAsistant : MonoBehaviour
{
    public GameObject Sphere;
    public GameObject GhostSphere;
    public List<GameObject> SphereCloneList;
    public float desPoint { get; set; }

    internal global::ExtendinWave GhostSphereClone1 { get; set; }

    internal global::WavingRing GhostSphereClone2 { get; set; }
    // Use this for initialization
    void Start()
	{
	   SphereCloneList=new List<GameObject>();
	    desPoint = -8;
	   GhostSphereClone1=new ExtendinWave((GameObject) Instantiate(GhostSphere, new Vector3(desPoint, 0, 10), new Quaternion()));
       GhostSphereClone1.gameObject.transform.localScale = Vector3.zero;
       GhostSphereClone2 = new WavingRing((GameObject)Instantiate(GhostSphere, new Vector3(desPoint, 0, 10), new Quaternion()));
      
       

	    for (int i = 1; i <= 5; i++)
	    {
            SphereCloneList.Add((GameObject)Instantiate(Sphere, new Vector3(12*i+desPoint, 0, 10), new Quaternion()));
	    } 
	}

    // Update is called once per frame
    void FixedUpdate()
    {

        bool destroyFlag = false;
        foreach (var itr in SphereCloneList)
        {
            itr.transform.position -= new Vector3(0.1f, 0, 0);

            float x = itr.transform.position.x;
            float xx = (1 - Math.Abs(x - desPoint)) * 0.1f;


            if (x < 0.5f + desPoint && x > desPoint)
            {
                itr.transform.localScale -= new Vector3(xx, xx, xx);
            }
            if (x < desPoint)
            {
                itr.transform.localScale = Vector3.zero;
                destroyFlag = true;
            }
        }
        GhostSphereClone1.update(destroyFlag, 0.1f);
        GhostSphereClone2.update( -0.002f);

        if (destroyFlag)
        {
            Destroy(SphereCloneList.First());
            SphereCloneList.Remove(SphereCloneList.First());
            SphereCloneList.Add((GameObject)Instantiate(Sphere, new Vector3(60 + desPoint, 0, 10), new Quaternion()));
            GhostSphereClone1.startFlag = true;
        }
    }


}

internal class ExtendinWave
{
    public GameObject gameObject { get; set; }

    private float sum;
    public bool startFlag { get; set; }

    public ExtendinWave(GameObject go)
    {
        this.gameObject = go;
        sum = 0;
        startFlag = false;
    }

    public void extend(float f)
    {
        gameObject.transform.localScale += new Vector3(f, f, f);
        sum += f;
    }

    public void minify()
    {
        gameObject.transform.localScale -= new Vector3(sum, sum, sum);
        sum = 0;
    }

    public void update(bool destroyFlag, float f)
    {
        if (!startFlag) return;
        if (destroyFlag)
        {
            minify();
        }
        else
        {
            extend(f);
        }
    }


}
public class WavingRing
{
    public GameObject gameObject { get; set; }
    private Vector3 defaltScale { get; set; }
    private bool extendFlag;
    public WavingRing(GameObject go)
    {

        this.gameObject = go;
        this.defaltScale = go.transform.localScale;
    }

    public void update(float f)
    {
        if (gameObject.transform.localScale.x / defaltScale.x > 1.3f)
        {
            extendFlag = true;
        }
        if (defaltScale.x / gameObject.transform.localScale.x > 1.3f)
        {
            extendFlag = false;
        }
        if (extendFlag)
        {
            gameObject.transform.localScale += new Vector3(f, f, f);
        }
        else
        {
            gameObject.transform.localScale -= new Vector3(f, f, f);
        }
    }

}