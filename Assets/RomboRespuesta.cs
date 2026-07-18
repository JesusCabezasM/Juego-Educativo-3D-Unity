using UnityEngine;

public class RomboRespuesta : MonoBehaviour
{
    public GameObject panelMensaje; // Aquí arrastraremos el panel de Felicidades o Error

    // Si el rombo es un objeto sólido (choca)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Jugador")
        {
            MostrarMensaje();
        }
    }

    // Si el rombo es atravesable (Is Trigger)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Jugador")
        {
            MostrarMensaje();
        }
    }

    private void MostrarMensaje()
    {
        if (panelMensaje != null)
        {
            panelMensaje.SetActive(true); // Enciende el panel
        }
        
        Time.timeScale = 0f; // ¡ESTO CONGELA EL JUEGO!
    }
}