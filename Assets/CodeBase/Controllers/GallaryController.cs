using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace CodeBase.Controllers
{
    public class GallaryController : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _contentContainer;
        [SerializeField] private GameObject _imagePrefab;
        [SerializeField] private float _bottomThreshold = 0.05f;

        private string baseUrl = "http://data.ikppbb.com/test-task-unity-data/pics/";

        private int _totalImageCount = 66;
        private int _loadedImageCount = 0;

        private bool _isLoadingImages = false;

        private void Start()
        {
            LoadInitialImages();
        }

        private void LoadInitialImages()
        {
            float imageHeight = _contentContainer.GetComponent<GridLayoutGroup>().cellSize.y;
            float scrollViewHeight = _scrollRect.GetComponent<RectTransform>().rect.height;
            int cloumnCount = _contentContainer.GetComponent<GridLayoutGroup>().constraintCount;

            int maxVisibleImages = Mathf.CeilToInt((scrollViewHeight / imageHeight) * cloumnCount);

            Debug.Log(maxVisibleImages);
            Debug.Log(scrollViewHeight);

            for (int i = 0; i < maxVisibleImages; i++)
            {
                string imageUrl = baseUrl + (i + 1) + ".jpg";
                StartCoroutine(DownloadImage(imageUrl));
                _loadedImageCount++;
            }
        }

        private IEnumerator DownloadImage(string imageUrl)
        {
            _isLoadingImages = true;

            UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                CreateImage(texture);
            }
            else
            {
                Debug.LogError($"Error downloading image: {www.error}");
            }

            _isLoadingImages = false;
        }

        private void CreateImage(Texture2D texture)
        {
            GameObject imageObject = Instantiate(_imagePrefab, _contentContainer);
            RawImage image = imageObject.GetComponent<RawImage>();
            image.texture = texture;
        }

        public void OnScrollValueChanged(Vector2 normalizedPosition)
        {
            if (!_isLoadingImages && _scrollRect.verticalNormalizedPosition <= _bottomThreshold && _loadedImageCount < _totalImageCount)
            {
                for (int i = 0; i < 2; i++)
                {
                    string imageUrl = baseUrl + (_loadedImageCount + 1) + ".jpg";
                    StartCoroutine(DownloadImage(imageUrl));
                    _loadedImageCount++;
                }
            }
        }
    }
}