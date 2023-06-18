using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSession : MonoBehaviour
{
    public RawImage Image { get; set; }

    private void Awake()
    {
        if (IsSessionExit())
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    private bool IsSessionExit()
    {
        var sessions = FindObjectsOfType<ImageSession>();
        foreach (var session in sessions)
        {
            if (session != this)
            {
                return true;
            }
        }

        return false;
    }
}
