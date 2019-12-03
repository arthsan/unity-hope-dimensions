using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPLayer : MonoBehaviour
{
    private void Awake()
    {
        int numMusicPLayers = FindObjectsOfType<MusicPLayer>().Length;
        if (numMusicPLayers > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    { 

    }
}
