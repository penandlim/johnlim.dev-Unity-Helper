using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[ExecuteInEditMode]
public class JlTextureTo3D : MonoBehaviour
{
    public Texture2D texture2D;
    public GameObject baseObj;

    [Button]
    public void Generate3D()
    {
        foreach (Transform child in transform) {
            GameObject.DestroyImmediate(child.gameObject);
        }
        
        
        int width = texture2D.width;
        int height = texture2D.height;
        int k = 1;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Color c = texture2D.GetPixel(i, j);
//                Debug.Log(c, this);
//                Debug.Log(i + " " + j, this);
                if (c == Color.black)
                {
                    GameObject obj = Instantiate(baseObj, this.transform);
                    obj.name = k + " : [" + i + ", " + j + "]";
                    obj.transform.localPosition = new Vector3(i - width / 2f, j - height / 2f, 0);
                    obj.SetActive(true);
                    k++;
                }
            }
        }

    }
}
