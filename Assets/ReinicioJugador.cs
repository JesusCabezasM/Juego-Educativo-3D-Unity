using UnityEngine;
using UnityEngine.SceneManagement;

public class ReinicioJugador : MonoBehaviour
{
    public int vidas = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            vidas--;

            if (vidas > 0)
            {
                CharacterController cc = other.GetComponent<CharacterController>();
                
                // Desactivamos el controlador para moverlo sin errores
                if (cc != null) cc.enabled = false;

                // Verificamos que GameManager exista para evitar el NullReference
                if (GameManager.instance != null)
                {
                    other.transform.position = GameManager.instance.UltimoCheckpoint;
                }
                else
                {
                    Debug.LogError("¡Falta el objeto GameManager en la escena!");
                }

                // Reactivamos el controlador
                if (cc != null) cc.enabled = true;
            }
            else
            {
                // Reiniciamos la escena
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}