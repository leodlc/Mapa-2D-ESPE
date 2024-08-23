using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class CapibaraController : MonoBehaviour
{
    public float speed = 2f;  // Velocidad de la capibara
    private Transform player; // Referencia al jugador (robot)
    private bool followingPlayer = false;

    private VideoPlayer videoPlayer;

    private void Start()
    {
        // Encuentra el objeto del jugador en la escena
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Encuentra el VideoPlayer en la escena
        videoPlayer = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
    }

    private void Update()
    {
        if (!followingPlayer)
        {
            // Movimiento simple de la capibara a lo largo del eje X
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            // Seguir al jugador
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        // Mueve la capibara hacia la posición del jugador
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Detén el movimiento de la capibara
            speed = 0f;

            // Inicia la reproducción del video
            StartCoroutine(PlayVideoAndFollowPlayer());
        }
    }

    private IEnumerator PlayVideoAndFollowPlayer()
    {
        // Reproduce el video
        videoPlayer.Play();

        // Espera a que el video termine
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        // Después de que el video termina, la capibara sigue al jugador
        followingPlayer = true;
        speed = 3f;  // Restablece la velocidad de la capibara
    }
}
