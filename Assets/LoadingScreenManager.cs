using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance;
    public GameObject m_loadingScreenObject;
    public Slider ProgressBar;
    public float fillTime = 5f; // Tiempo en segundos para llenar la barra de progreso

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SwitchToScene(int id)
    {
        m_loadingScreenObject.SetActive(true);
        ProgressBar.value = 0;
        StartCoroutine(SwitchToSceneAsync(id));
    }

    IEnumerator SwitchToSceneAsync(int id)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(id);
        asyncLoad.allowSceneActivation = false;

        float elapsedTime = 0f;

        while (!asyncLoad.isDone)
        {
            // Actualiza la barra de progreso basado en el tiempo transcurrido
            elapsedTime += Time.deltaTime;
            ProgressBar.value = Mathf.Clamp01(elapsedTime / fillTime);

            // Permitir la activación de la escena solo después de que el progreso llegue al final
            if (elapsedTime >= fillTime)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

        yield return new WaitForSeconds(0.5f); // Espera medio segundo antes de desactivar la pantalla de carga
        m_loadingScreenObject.SetActive(false);
    }
}
