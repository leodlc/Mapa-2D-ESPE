using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image LoadingBarFill;

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
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
    }
}
