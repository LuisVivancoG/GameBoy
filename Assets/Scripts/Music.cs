using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Music : MonoBehaviour
{
    private static Music _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(base.gameObject);
            AudioSource song = GetComponent<AudioSource>();
            song.Play();
        }
        else
        {
            Destroy(base.gameObject);
        }
    }
}
