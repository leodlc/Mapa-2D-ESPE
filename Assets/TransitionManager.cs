using System.Collections;
using System.Collections.Generic;
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
    public const string SCENE_NAME_MAIN_MENU = "Inicio";
    public const string SCENE_NAME_GAME = "SampleScene";

    public Slider progressSlider;
    public TextMeshProUGUI progressLabel;
    public TextMeshProUGUI transitionInformationLabel;
    [Multiline]
    public string[] gameInformation = new string[0];
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
        StartCoroutine(LoadCoroutine(sceneName));
    }

    IEnumerator LoadCoroutine(string sceneName)
    {
        m_Anim.SetBool(HashShowAnim, true);
        if (transitionInformationLabel != null)
            transitionInformationLabel.text = gameInformation[Random.Range(0, gameInformation.Length)];
        UpdateProgressValue(0);

        yield return new WaitForSeconds(0.5f);
        var sceneAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while (!sceneAsync.isDone)
        {
            UpdateProgressValue(sceneAsync.progress);
            yield return null;
        }

        UpdateProgressValue(1);
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
