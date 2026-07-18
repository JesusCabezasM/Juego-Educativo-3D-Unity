using UnityEngine;

public class ActivadorPregunta : MonoBehaviour
{
    public GameObject panelAActivar; 

    // Usamos OnCollisionEnter porque ahora el piso es sólido
    private void OnCollisionEnter(Collision collision)
    {
        // Si el jugador pisa o choca con este tablero...
        if (collision.gameObject.name == "Jugador")
        {
            panelAActivar.SetActive(true); // Encendemos el panel
        }
    }
}