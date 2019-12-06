using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class read_quaternion : MonoBehaviour
{
    public GameObject read_object1, read_object2;
    public GameObject ans_object, kakunin_object;
    Quaternion ans_rotation, kakunin_rotation, object1_rotation, object2_rotation;

    float norm_ans,x,y,z,w;

    float ToAngle(float radian)
    {
        return (float)(radian * 180 / Mathf.PI);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        object1_rotation = read_object1.transform.localRotation;
        object2_rotation = read_object2.transform.localRotation;
        ans_rotation = Quaternion.Inverse(object1_rotation) * object2_rotation;
        kakunin_rotation = ans_rotation * object1_rotation;
       
        Debug.Log("O1: " + object1_rotation.ToString("f7"));
        Debug.Log("O2: " + object2_rotation.ToString("f7"));
        Debug.Log("ans:" + ans_rotation.ToString("f7"));

        //Debug.Log("inv_O2: " + Quaternion.Inverse(object2_rotation));

        

        norm_ans = Mathf.Sqrt(ans_rotation.x * ans_rotation.x + ans_rotation.y * ans_rotation.y + ans_rotation.z * ans_rotation.z + ans_rotation.w * ans_rotation.w);
        Debug.Log("x:" + ans_rotation.x);
        Debug.Log("y:" + ans_rotation.y);
        Debug.Log("z:" + ans_rotation.z);
        Debug.Log("w:" + ans_rotation.w);

        float sita = ToAngle(Mathf.Acos(ans_rotation.w));

        Debug.Log("Θ: " + 2 * sita);
        Debug.Log("norm(ans): " + norm_ans.ToString("f10"));
        Debug.Log("norm(ans)**2: " + norm_ans * norm_ans);
        


        kakunin_object.transform.localRotation = kakunin_rotation;
        ans_object.transform.localRotation = ans_rotation;
    }
}
