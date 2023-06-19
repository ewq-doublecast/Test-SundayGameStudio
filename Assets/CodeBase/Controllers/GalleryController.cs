using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace CodeBase.Controllers
{
    public class GalleryController : MonoBehaviour
    {
        private const string BaseUrl = "http://data.ikppbb.com/test-task-unity-data/pics/";
        private const int TotalImageCount = 66;
        private const string JpgPostfix = ".jpg";

        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _contentContainer;
        [SerializeField] private SelectableImage _imagePrefab;
        [SerializeField] private float _bottomThreshold = 0.05f;

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
            int columnCount = _contentContainer.GetComponent<GridLayoutGroup>().constraintCount;

            int maxVisibleImages = Mathf.CeilToInt((scrollViewHeight / imageHeight) * columnCount);

            for (int i = 0; i < maxVisibleImages; i++)
            {
                DownloadOneImage();
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
            SelectableImage image = Instantiate(_imagePrefab, _contentContainer);
            image.SetTexture(texture);
        }

        public void OnScrollValueChanged()
        {
            if (!_isLoadingImages && _scrollRect.verticalNormalizedPosition <= _bottomThreshold && _loadedImageCount < TotalImageCount)
            {
                for (int i = 0; i < 2; i++)
                {
                    DownloadOneImage();
                }
            }
        }

        public void DownloadOneImage()
        {
            string imageUrl = BaseUrl + (_loadedImageCount + 1) + JpgPostfix;
            StartCoroutine(DownloadImage(imageUrl));
            _loadedImageCount++;
        }
    }
}