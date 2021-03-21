using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("showAdd", 5);
    }

   void showAdd()
    {
       // ApplovinMaxManager.Instance.ShowInterstatial();
        Destroy(this);
    }
}
