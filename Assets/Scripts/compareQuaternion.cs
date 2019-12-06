using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class compareQuaternion : MonoBehaviour
{
    public Transform chara1, chara2;                               //比較するキャラ格納
    //Dictionary<string, GameObject> objDic = new Dictionary<string, GameObject>();   //オブジェクトの
    //List<Transform> transList = new List<Transform>();                              //各部位のtransformコレクションをリストで取っておく
    List<Transform> charaList = new List<Transform>();                              //比較する2体のキャラが格納される
    Dictionary<string, Transform> chara1_dic = new Dictionary<string, Transform>();
    Dictionary<string, Transform> chara2_dic = new Dictionary<string, Transform>();

    List<string> tagList = new List<string>()
    {
        "RightShoulder", "RightArm" , "RightForeArm", "RightHand",
        "LeftShoulder", "LeftArm", "LeftForeArm", "LeftHand",
        "Spine1", "Head", "Hip",
        "RightUpLeg", "RightLeg", "RightFoot", "RightToeBase",
        "LeftUpLeg", "LeftLeg", "LeftFoot", "LeftToeBase",
    };         //体の部位を表すタグを入れておく

    Dictionary<string, int> w = new Dictionary<string, int>()     //部位ごとの重みを入れておく
    {
        {"RightShoulder", 1 },
        {"RightArm",  1},
        {"RightForeArm", 1 },
        {"RightHand", 0},
        {"LeftShoulder", 1 },
        {"LeftArm", 1 },
        {"LeftForeArm", 1 },
        {"LeftHand", 0 },
        {"Spine1", 1 },
        {"Head", 0 },
        {"RightUpLeg", 1 },
        {"RightLeg", 1 },
        {"RightFoot", 0 },
        {"RightToeBase", 0 },
        {"LeftUpLeg", 1 },
        {"LeftLeg", 1 },
        {"LeftFoot", 0 },
        {"LeftToeBase", 0 },
    };


    //ラジアンを角度に変換して返す
    float ToAngle(float radian)
    {
        return (float)(radian * 180 / Mathf.PI);
    }


    //obj2を最短経路で何度（度）回転させたらobj1の姿勢になるのか
    //単位クオータニオンの式 q=(x, y, z, w) = (vsinΘ, cosΘ)より
    float QuaternionDifferences(Transform obj1, Transform obj2, int weight)
    {
        Quaternion tmp = Quaternion.Inverse(obj2.localRotation) * obj1.localRotation;
        float ans = 2 * ToAngle(Mathf.Acos(tmp.w));
        if (float.IsNaN(ans))
            return 0.0f;
        else return ans;
    }


    //obj1とobj2のユークリッド距離の二乗を計算して返す
    float Euclid_distance_square(Transform obj1, Transform obj2)
    {
        Vector3 vec = obj1.position - obj2.position;
        return (vec.x * vec.x) + (vec.y * vec.y) + (vec.z * vec.z);
    }

    
    //d(pi, pj)の式
    float PositionDefferences(Dictionary<string, Transform> trans1, Dictionary<string, Transform> trans2)
    {
        //d(pi, pj)の最初の項
        float pos_d = Euclid_distance_square(trans1["Hip"], trans2["Hip"]);

        //d(pi, pj)の最後の項
        List<float> quaternions = new List<float>();
        foreach(string tag in w.Keys)
        {
            quaternions.Add(QuaternionDifferences(trans1[tag], trans2[tag], w[tag]));
        }

        //腰の位置も考慮するバージョン
        //return pos_d + quaternions.Sum();

        //腰の位置を考慮しない（姿勢だけを見る）バージョン
        return quaternions.Sum();
    }
    

    void MakeCharaTransDict()
    {
        charaList.Add(chara1);
        charaList.Add(chara2);
        foreach (Transform chara in charaList)
        {
            foreach (string tag in tagList)
            {
                var childObj = chara.GetComponentsInChildren<Transform>().Where(t => t.tag == tag);
                foreach (var item in childObj)
                {
                    if (chara.name == chara1.name)
                        chara1_dic.Add(tag, item.transform);
                    else
                        chara2_dic.Add(tag, item.transform);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeCharaTransDict();
        foreach (string key in chara1_dic.Keys)
        {
            Debug.Log(key + ": " + chara1_dic[key].name);
        }
    }


    // Update is called once per frame
    void Update()
    {
        float d = PositionDefferences(chara1_dic, chara2_dic);
        Debug.Log("d(pi, pj) :" + d);
    }
}
