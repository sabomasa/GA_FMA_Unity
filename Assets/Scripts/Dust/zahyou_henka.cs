using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zahyou_henka : MonoBehaviour
{
    //Random.InitState(10);
    public int random_seed=0;
    float value, value2;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(random_seed);
        value2 = Random.Range(0.1f, 1.0f);
        value = Random.Range(0.1f, 1.0f);
        Debug.Log("value:" + value + "  value2:" + value2);
    }

    // Update is called once per frame
    void Update()
    {
        float sin = value*Mathf.Sin(Time.time*value2);
        Transform myTransform = this.transform;

        Vector3 pos = myTransform.position;
        pos.z = sin;

        myTransform.position = pos;
    }
}
