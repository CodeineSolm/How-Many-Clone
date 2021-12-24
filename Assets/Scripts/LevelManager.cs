using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressBar;
    [SerializeField] private float _loadSpeed;

    private float _sceneLoaded = 0.9f;
    private float _progressTarget;

    public async void LoadScene(int sceneIndex)
    {
        _progressBar.fillAmount = 0;
        _progressTarget = 0;
        var scene = SceneManager.LoadSceneAsync(sceneIndex);
        scene.allowSceneActivation = false;
        _loaderCanvas.SetActive(true);

        do
        {
            _progressTarget = scene.progress;
        } while (scene.progress < _sceneLoaded);

        await Task.Delay(1000);
        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);
    }

    private void Awake()
    {
        _loaderCanvas.SetActive(false);

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _progressTarget, _loadSpeed * Time.deltaTime);
    }
}
