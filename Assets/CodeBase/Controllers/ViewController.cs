using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.Controllers
{
    public class ViewController : MonoBehaviour
    {
        private const string TargetScene = "Gallery";

        [SerializeField] private RawImage _image;

        private ImageSession _session;

        private void Start()
        {
            _session = FindObjectOfType<ImageSession>();
            _image.texture = _session.Image.texture;
        }

        public void BackToGallary()
        {
            SceneManager.LoadScene(TargetScene);
        }
    }
}