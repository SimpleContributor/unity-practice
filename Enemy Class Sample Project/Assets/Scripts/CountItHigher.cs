using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountItHigher : MonoBehaviour
{
    [SerializeField]
    private int _num = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(nextNum);
    }

    public int nextNum
    {
        get
        {
            _num++;
            return (_num);
        }
    }

    public int currentNum
    {
        get { return (_num); }
        set { _num = value; }
    }
}
