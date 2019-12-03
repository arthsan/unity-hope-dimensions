using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")] [SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX prefab")][SerializeField] GameObject deathFX;

    void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        deathFX.SetActive(true);
        Invoke("LoadLevelScene", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPLayerDeath");
    }

    private void LoadLevelScene()
    {
        SceneManager.LoadScene(1);
    }
}
