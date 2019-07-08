using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertStringToInt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int number(string text)
    {
        int value = int.Parse(text);
        return value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
