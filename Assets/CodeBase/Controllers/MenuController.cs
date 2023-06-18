using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.Controllers
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Button _gallaryButton;

        public void GoToGallary()
        {
            SceneManager.LoadScene("Loading");
        }
    }
}