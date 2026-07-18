using UnityEngine;

[DisallowMultipleComponent]
public class ZonaCaida : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Transform jugador = DetectarJugador(other);

        if (jugador == null || GestorPreguntas.Instancia == null)
        {
            return;
        }

        GestorPreguntas.Instancia.RegistrarCaida(jugador);
    }

    private Transform DetectarJugador(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            return other.transform.root;
        }

        Transform raiz = other.transform.root;
        return raiz.CompareTag("Player") ? raiz : null;
    }
}
