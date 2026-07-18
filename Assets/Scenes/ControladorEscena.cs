using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorEscena : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verificamos que el nombre coincida exactamente con tu esfera
        if (other.gameObject.name == "Jugador")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}