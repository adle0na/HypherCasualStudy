using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSound_Wav : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private static BGSound_Wav instance = null;
    public static BGSound_Wav Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
