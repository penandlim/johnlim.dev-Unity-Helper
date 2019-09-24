using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class JohnLimDevExporter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private string SplitInto4(Transform t, Vector3 origin)
    {
        Renderer rend = t.GetComponent<Renderer>();

        Vector3 pos = t.position - (rend.bounds.size.x / 4) * t.forward - (rend.bounds.size.y / 4) * t.right -
                      (rend.bounds.size.z / 4) * t.up;
        

        string s = "";
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    Vector3 newPos = pos;
                    newPos = newPos + i * (rend.bounds.size.x / 2) * t.forward + j * (rend.bounds.size.y / 2) * t.right +
                             k * (rend.bounds.size.z / 2) * t.up;

                    newPos = origin - newPos;
                    
                    s += "new THREE.Vector3(" + newPos.x + ", " + newPos.y + ", " + newPos.z + "), ";
                }
            }
        }

        return s;
    }

    private string SplitInto4Scale(Transform t)
    {
        string s = "";
        for (int i = 0; i < 8; i++)
        {
            s += "new THREE.Vector3(" + t.localScale.x / 2f + ", " + t.localScale.y / 2f + ", " + t.localScale.z / 2f + "), ";
        }

        return s;
    }
    
    private string SplitInto4Rot(Transform t)
    {
        
        string s = "";
        for (int i = 0; i < 8; i++)
        {
            s += "new THREE.Vector3(" + t.localEulerAngles.x * Mathf.Deg2Rad + ", " + t.localEulerAngles.y * Mathf.Deg2Rad + ", " + t.localEulerAngles.z * Mathf.Deg2Rad + "), ";
        }

        return s;
    }


    // Update is called once per frame
    void Update()
    {

    }

    
    [Button]
    public void PrintPosScaleRot()
    {
        string s = "[";
        
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);

            if (child.name.Contains("split"))
            {
                s += SplitInto4(child, transform.position);
            }
            else
            {
                var t = child.position;

                t = transform.position - t;
                
                s += "[" + -t.x + ", " + -t.y + ", " + t.z + "], ";
            }

            

        }

        s += "]";
        
        Debug.Log("Positions: " + s);
        
        
        
        s = "[";
        
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.name.Contains("split"))
            {
                s += SplitInto4Scale(child);
            }
            else
            {
                var t = transform.GetChild(i).localScale;
                s += "[" + t.x + ", " + t.y + ", " + t.z + "], ";
            }

        }

        s += "]";
        Debug.Log("Scale: " + s);
        
        s = "[";
        
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.name.Contains("split"))
            {
                s += SplitInto4Rot(child);
            }
            else
            {
                var t = transform.GetChild(i).eulerAngles;
                s += "[" + t.x * Mathf.Deg2Rad + ", " + t.y * Mathf.Deg2Rad + ", " + t.z * Mathf.Deg2Rad + "], ";
            }

        }

        s += "]";
        Debug.Log("Rotation: " + s);
    }
}
