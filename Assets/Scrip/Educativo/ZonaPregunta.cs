using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class ZonaPregunta : MonoBehaviour
{
    [Header("Selección de pregunta")]
    [SerializeField]
    [Tooltip("-1 usa la siguiente pregunta del banco. 0 a 19 elige una pregunta específica.")]
    private int indicePregunta = -1;

    [Header("Textos 3D")]
    [SerializeField] private TMP_Text textoPregunta;
    [SerializeField] private TMP_Text textoRespuestaIzquierda;
    [SerializeField] private TMP_Text textoRespuestaDerecha;

    [Header("Zonas de respuesta")]
    [SerializeField] private ZonaRespuesta zonaRespuestaIzquierda;
    [SerializeField] private ZonaRespuesta zonaRespuestaDerecha;

    [Header("Reaparición")]
    [SerializeField] private Transform puntoReintento;

    public int IndicePregunta => indicePregunta;
    public Transform PuntoReintento => puntoReintento;
    public bool Completada { get; private set; }

    private void Awake()
    {
        if (zonaRespuestaIzquierda != null)
        {
            zonaRespuestaIzquierda.Configurar(this, 0);
        }

        if (zonaRespuestaDerecha != null)
        {
            zonaRespuestaDerecha.Configurar(this, 1);
        }

        MostrarTextos(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform jugador = DetectarJugador(other);
        if (jugador == null || GestorPreguntas.Instancia == null)
        {
            return;
        }

        GestorPreguntas.Instancia.IniciarPregunta(this, jugador);
    }

    public void MostrarPregunta(PreguntaDatos pregunta)
    {
        if (pregunta == null)
        {
            return;
        }

        if (textoPregunta != null)
        {
            string categoria = string.IsNullOrWhiteSpace(pregunta.categoria)
                ? string.Empty
                : $"[{pregunta.categoria}]\n";

            textoPregunta.text = categoria + pregunta.enunciado;
        }

        if (textoRespuestaIzquierda != null)
        {
            textoRespuestaIzquierda.text = pregunta.opcionIzquierda;
        }

        if (textoRespuestaDerecha != null)
        {
            textoRespuestaDerecha.text = pregunta.opcionDerecha;
        }

        MostrarTextos(true);
    }

    public void MarcarCompletada()
    {
        Completada = true;

        Collider disparador = GetComponent<Collider>();
        if (disparador != null)
        {
            disparador.enabled = false;
        }
    }

    public void OcultarPregunta()
    {
        MostrarTextos(false);
    }

    private void MostrarTextos(bool mostrar)
    {
        if (textoPregunta != null)
        {
            textoPregunta.gameObject.SetActive(mostrar);
        }

        if (textoRespuestaIzquierda != null)
        {
            textoRespuestaIzquierda.gameObject.SetActive(mostrar);
        }

        if (textoRespuestaDerecha != null)
        {
            textoRespuestaDerecha.gameObject.SetActive(mostrar);
        }
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
