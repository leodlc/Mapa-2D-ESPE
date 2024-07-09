using UnityEngine;

public class menuPausa : MonoBehaviour
{
    public GameObject pausePanel;

    void Start()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            if (pausePanel != null)
            {
                pausePanel.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 1;
            if (pausePanel != null)
            {
                pausePanel.SetActive(false);
            }
        }
    }
}