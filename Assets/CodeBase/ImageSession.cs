using UnityEngine;
using UnityEngine.UI;

namespace CodeBase
{
    public class ImageSession : MonoBehaviour
    {
        public RawImage Image { get; set; }

        private void Awake()
        {
            if (IsSessionExit())
            {
                Destroy(gameObject);
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
}