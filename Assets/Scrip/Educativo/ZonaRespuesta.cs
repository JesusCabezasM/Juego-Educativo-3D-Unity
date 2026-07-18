using UnityEngine;

[DisallowMultipleComponent]
public class ZonaRespuesta : MonoBehaviour
{
    [SerializeField] private ZonaPregunta zonaPregunta;

    [SerializeField, Range(0, 1)]
    [Tooltip("0 = izquierda, 1 = derecha")]
    private int indiceOpcion;

    public void Configurar(ZonaPregunta nuevaZona, int nuevoIndice)
    {
        zonaPregunta = nuevaZona;
        indiceOpcion = Mathf.Clamp(nuevoIndice, 0, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform jugador = DetectarJugador(other);

        if (jugador == null ||
            zonaPregunta == null ||
            GestorPreguntas.Instancia == null)
        {
            return;
        }

        GestorPreguntas.Instancia.EvaluarRespuesta(
            zonaPregunta,
            indiceOpcion,
            jugador);
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
