using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject client;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("client");
        if (objs.Length == 0)
        {
            Instantiate(client);
        }
    }
}
