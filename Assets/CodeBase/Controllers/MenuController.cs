using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.Controllers
{
    public class MenuController : MonoBehaviour
    {
        private const string TargetScene = "Loading";
        [SerializeField] private Button _gallaryButton;

        public void GoToGallary()
        {
            SceneManager.LoadScene(TargetScene);
        }
    }
}