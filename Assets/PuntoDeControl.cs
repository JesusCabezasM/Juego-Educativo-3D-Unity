using UnityEngine;

public class PuntoDeControl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Usamos FindFirstObjectByType en lugar de FindObjectOfType para quitar el aviso amarillo
            GameManager gm = Object.FindFirstObjectByType<GameManager>();

            if (gm != null)
            {
                gm.UltimoCheckpoint = this.transform.position;
                Debug.Log("Checkpoint guardado correctamente.");
            }
            else
            {
                Debug.LogError("¡NO EXISTE NINGÚN OBJETO CON EL SCRIPT GAMEMANAGER EN LA ESCENA!");
            }
        }
    }
}