using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectableImage : MonoBehaviour
{
    public RawImage SelectedImage;
    private ImageSession _session;

    private void Start()
    {
        SelectedImage = GetComponent<RawImage>();
        _session = FindObjectOfType<ImageSession>();
    }

    public void SelectImage()
    {
        _session.Image = SelectedImage;
        SceneManager.LoadScene("View");
    }
}
