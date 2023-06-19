using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase
{
    public class SelectableImage : MonoBehaviour
    {
        private const string SceneName = "View";

        private RawImage _selectedImage;
        private ImageSession _session;

        private void Awake()
        {
            _selectedImage = GetComponent<RawImage>();
            _session = FindObjectOfType<ImageSession>();
        }

        public void SelectImage()
        {
            _session.Image = _selectedImage;
            SceneManager.LoadScene(SceneName);
        }

        public void SetTexture(Texture2D texture)
        {
            _selectedImage.texture = texture;
        }
    }
}