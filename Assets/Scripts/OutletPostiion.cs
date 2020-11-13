using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OutletPostiion : MonoBehaviour
{
    float x;
    float y;
    float z;
    Vector3 pos;
    GameObject rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<GameObject>();
        x = 17.54636f;
        y = Random.Range(-25, 26);
        z = Random.Range(-25, 26);
        rb.transform.position = new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
