using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Animator))]
public class TransitionManager : MonoBehaviour
{
    private static TransitionManager instance;
    public static TransitionManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Instantiate(Resources.Load<TransitionManager>("TransitionManager"));
                instance.Init();
            }

            return instance;
        }
    }

    public Slider progressSlider;
    public TextMeshProUGUI progressLabel;
    public TextMeshProUGUI transitionInformationLabel;
    [Multiline]
    public string[] gameInformation = new string[0];
    public GameObject loadingScreen;
    public Image LoadingBarFill;
    private Animator m_Anim;
    private int HashShowAnim = Animator.StringToHash("Show");

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Init();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Init()
    {
        m_Anim = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        m_Anim.SetBool(HashShowAnim, true);
        if (transitionInformationLabel != null)
            transitionInformationLabel.text = gameInformation[Random.Range(0, gameInformation.Length)];
        UpdateProgressValue(0);

        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false; // Evita que la escena se active inmediatamente

        float targetProgress = 0;
        float fillSpeed = 1.0f / 3.0f; // Tiempo en segundos para llenar la barra (aquí 3 segundos)

        while (!operation.isDone)
        {
            // Ajusta el progreso de la barra de carga
            targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

            // Llenado progresivo de la barra de carga
            while (LoadingBarFill.fillAmount < targetProgress)
            {
                LoadingBarFill.fillAmount += fillSpeed * Time.deltaTime;
                yield return null;
            }

            // Cuando la carga esté completa, espera un segundo antes de activar la escena
            if (operation.progress >= 0.9f)
            {
                LoadingBarFill.fillAmount = 1;
                yield return new WaitForSeconds(1f); // Espera de 1 segundo
                operation.allowSceneActivation = true; // Activa la escena
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.5f); // Espera medio segundo antes de desactivar la pantalla de carga
        loadingScreen.SetActive(false);
        m_Anim.SetBool(HashShowAnim, false);
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        m_Anim.SetBool(HashShowAnim, true);
        if (transitionInformationLabel != null)
            transitionInformationLabel.text = gameInformation[Random.Range(0, gameInformation.Length)];
        UpdateProgressValue(0);

        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        operation.allowSceneActivation = false; // Evita que la escena se active inmediatamente

        float targetProgress = 0;
        float fillSpeed = 1.0f / 3.0f; // Tiempo en segundos para llenar la barra (aquí 3 segundos)

        while (!operation.isDone)
        {
            // Ajusta el progreso de la barra de carga
            targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

            // Llenado progresivo de la barra de carga
            while (LoadingBarFill.fillAmount < targetProgress)
            {
                LoadingBarFill.fillAmount += fillSpeed * Time.deltaTime;
                yield return null;
            }

            // Cuando la carga esté completa, espera un segundo antes de activar la escena
            if (operation.progress >= 0.9f)
            {
                LoadingBarFill.fillAmount = 1;
                yield return new WaitForSeconds(1f); // Espera de 1 segundo
                operation.allowSceneActivation = true; // Activa la escena
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.5f); // Espera medio segundo antes de desactivar la pantalla de carga
        loadingScreen.SetActive(false);
        m_Anim.SetBool(HashShowAnim, false);
    }

    void UpdateProgressValue(float progressValue)
    {
        if (progressSlider != null)
            progressSlider.value = progressValue;
        if (progressLabel != null)
            progressLabel.text = $"{progressValue * 100}%";
    }
}
