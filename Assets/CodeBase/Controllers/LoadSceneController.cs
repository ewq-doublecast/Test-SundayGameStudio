using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneController : MonoBehaviour
{
    private const string TargetScene = "Gallery";

    [SerializeField] private Slider _progressBar;
    [SerializeField] private TMP_Text _progressText;
    [SerializeField] private float _loadingTime = 2f;

    private void Start()
    {
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _loadingTime)
        {
            float progress = elapsedTime / _loadingTime;
            _progressBar.value = progress;
            _progressText.text = $"{Mathf.RoundToInt(progress * 100)}%";

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(TargetScene);
    }
}
