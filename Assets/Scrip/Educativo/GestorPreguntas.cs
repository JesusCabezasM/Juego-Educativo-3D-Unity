using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GestorPreguntas : MonoBehaviour
{
    public static GestorPreguntas Instancia { get; private set; }

    [Header("Banco de preguntas")]
    [SerializeField] private List<PreguntaDatos> bancoPreguntas = new List<PreguntaDatos>();

    [Header("HUD")]
    [SerializeField] private TMP_Text textoVidas;
    [SerializeField] private TMP_Text textoPuntaje;
    [SerializeField] private TMP_Text textoMensaje;

    [Header("Jugador y reinicio")]
    [SerializeField] private Transform puntoInicioGlobal;
    [SerializeField, Min(1)] private int vidasMaximas = 3;
    [SerializeField, Min(0)] private int puntosPorAcierto = 10;
    [SerializeField, Min(0.1f)] private float tiempoMensaje = 1.4f;

    [Header("Sonidos opcionales")]
    [SerializeField] private AudioSource fuenteAudio;
    [SerializeField] private AudioClip sonidoCorrecto;
    [SerializeField] private AudioClip sonidoIncorrecto;

    private ZonaPregunta zonaActual;
    private PreguntaDatos preguntaActual;
    private Transform jugador;
    private int vidasRestantes;
    private int puntaje;
    private int siguientePregunta;
    private bool procesandoRespuesta;

    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        Instancia = this;

        if (bancoPreguntas.Count == 0)
        {
            CargarBancoDeEjemplo();
        }

        vidasRestantes = vidasMaximas;
        ActualizarHUD();
        EscribirMensaje("Salta a la plataforma verde para comenzar.");
    }

    public void IniciarPregunta(ZonaPregunta zona, Transform jugadorDetectado)
    {
        if (zona == null || zona.Completada || procesandoRespuesta)
        {
            return;
        }

        // Impide activar otra pregunta mientras la actual no se resuelva.
        if (zonaActual != null && zonaActual != zona)
        {
            return;
        }

        // Evita reiniciar las vidas al volver a pisar la misma plataforma.
        if (zonaActual == zona && preguntaActual != null)
        {
            return;
        }

        if (bancoPreguntas.Count == 0)
        {
            Debug.LogError("No existen preguntas en el banco.");
            return;
        }

        jugador = ObtenerRaizJugador(jugadorDetectado);

        int indice = zona.IndicePregunta;
        if (indice < 0)
        {
            indice = siguientePregunta % bancoPreguntas.Count;
            siguientePregunta++;
        }

        if (indice >= bancoPreguntas.Count)
        {
            Debug.LogWarning(
                $"El índice {indice} no existe. Se usará un índice válido del banco.");
            indice %= bancoPreguntas.Count;
        }

        zonaActual = zona;
        preguntaActual = bancoPreguntas[indice];
        vidasRestantes = vidasMaximas;

        zonaActual.MostrarPregunta(preguntaActual);
        EscribirMensaje("Lee con calma y salta sobre la respuesta correcta.");
        ActualizarHUD();
    }

    public void EvaluarRespuesta(
        ZonaPregunta zona,
        int indiceElegido,
        Transform jugadorDetectado)
    {
        if (procesandoRespuesta ||
            zonaActual == null ||
            preguntaActual == null ||
            zona != zonaActual)
        {
            return;
        }

        jugador = ObtenerRaizJugador(jugadorDetectado);

        bool esCorrecta = indiceElegido == preguntaActual.indiceCorrecto;

        if (esCorrecta)
        {
            StartCoroutine(RespuestaCorrecta());
        }
        else
        {
            StartCoroutine(RespuestaIncorrecta());
        }
    }

    public void RegistrarCaida(Transform jugadorDetectado)
    {
        if (procesandoRespuesta)
        {
            return;
        }

        jugador = ObtenerRaizJugador(jugadorDetectado);
        StartCoroutine(ProcesarCaida());
    }

    private IEnumerator RespuestaCorrecta()
    {
        procesandoRespuesta = true;
        puntaje += puntosPorAcierto;

        Reproducir(sonidoCorrecto);
        zonaActual.MarcarCompletada();

        string explicacion = string.IsNullOrWhiteSpace(preguntaActual.explicacion)
            ? string.Empty
            : " " + preguntaActual.explicacion;

        EscribirMensaje("¡Muy bien! Respuesta correcta." + explicacion);
        ActualizarHUD();

        yield return new WaitForSeconds(tiempoMensaje);

        zonaActual.OcultarPregunta();
        zonaActual = null;
        preguntaActual = null;
        procesandoRespuesta = false;

        EscribirMensaje("Continúa hacia la siguiente plataforma verde.");
    }

    private IEnumerator RespuestaIncorrecta()
    {
        procesandoRespuesta = true;
        vidasRestantes--;

        Reproducir(sonidoIncorrecto);
        ActualizarHUD();

        if (vidasRestantes <= 0)
        {
            EscribirMensaje("Se terminaron los 3 intentos. Volvemos al inicio.");
            yield return new WaitForSeconds(tiempoMensaje);
            ReiniciarEscenaActual();
            yield break;
        }

        string pista = string.IsNullOrWhiteSpace(preguntaActual.pista)
            ? "Observa nuevamente las dos opciones."
            : preguntaActual.pista;

        EscribirMensaje(
            $"Aún tienes {vidasRestantes} intento(s). Pista: {pista}");

        yield return new WaitForSeconds(tiempoMensaje);

        Transform destino = zonaActual.PuntoReintento != null
            ? zonaActual.PuntoReintento
            : zonaActual.transform;

        TeletransportarJugador(destino);
        procesandoRespuesta = false;
    }

    private IEnumerator ProcesarCaida()
    {
        procesandoRespuesta = true;
        vidasRestantes--;

        Reproducir(sonidoIncorrecto);
        ActualizarHUD();

        if (vidasRestantes <= 0)
        {
            EscribirMensaje("Perdiste las 3 vidas. Volvemos al inicio.");
            yield return new WaitForSeconds(tiempoMensaje);
            ReiniciarEscenaActual();
            yield break;
        }

        EscribirMensaje($"Caíste. Te quedan {vidasRestantes} vida(s).");
        yield return new WaitForSeconds(0.8f);

        Transform destino = zonaActual != null && zonaActual.PuntoReintento != null
            ? zonaActual.PuntoReintento
            : puntoInicioGlobal;

        if (destino != null)
        {
            TeletransportarJugador(destino);
        }
        else
        {
            Debug.LogWarning("Asigna Punto Inicio Global en GestorPreguntas.");
        }

        procesandoRespuesta = false;
    }

private void TeletransportarJugador(Transform destino)
{
    if (jugador == null || destino == null)
    {
        return;
    }

    CharacterController controlador =
        jugador.GetComponentInChildren<CharacterController>();

    Rigidbody cuerpo =
        jugador.GetComponentInChildren<Rigidbody>();

    if (controlador != null)
    {
        controlador.enabled = false;
    }

    if (cuerpo != null)
    {
        cuerpo.linearVelocity = Vector3.zero;
        cuerpo.angularVelocity = Vector3.zero;
    }

    // Mantiene al personaje de pie.
    // Solo conserva la rotación horizontal del punto de reintento.
    Quaternion rotacionVertical = Quaternion.Euler(
        0f,
        destino.eulerAngles.y,
        0f
    );

    jugador.SetPositionAndRotation(
        destino.position,
        rotacionVertical
    );

    if (controlador != null)
    {
        controlador.enabled = true;
    }

    Physics.SyncTransforms();
}
    private Transform ObtenerRaizJugador(Transform detectado)
    {
        if (detectado == null)
        {
            return jugador;
        }

        return detectado.root;
    }

    private void ReiniciarEscenaActual()
    {
        Scene escena = SceneManager.GetActiveScene();
        SceneManager.LoadScene(escena.buildIndex);
    }

    private void ActualizarHUD()
    {
        if (textoVidas != null)
        {
            textoVidas.text = $"Intentos: {vidasRestantes}/{vidasMaximas}";
        }

        if (textoPuntaje != null)
        {
            textoPuntaje.text = $"Puntaje: {puntaje}";
        }
    }

    private void EscribirMensaje(string mensaje)
    {
        if (textoMensaje != null)
        {
            textoMensaje.text = mensaje;
        }
    }

    private void Reproducir(AudioClip clip)
    {
        if (fuenteAudio != null && clip != null)
        {
            fuenteAudio.PlayOneShot(clip);
        }
    }

    private void CargarBancoDeEjemplo()
    {
        bancoPreguntas = new List<PreguntaDatos>
        {
            new PreguntaDatos(
                "Matemática", "¿Cuánto es 2 + 3?", "5", "6", 0,
                "Cuenta dos objetos y agrega tres más.",
                "Dos más tres es igual a cinco."),

            new PreguntaDatos(
                "Matemática", "¿Cuánto es 10 - 4?", "5", "6", 1,
                "Quita cuatro unidades al número diez.",
                "Diez menos cuatro es igual a seis."),

            new PreguntaDatos(
                "Matemática", "¿Cuánto es 4 + 4?", "8", "9", 0,
                "Puedes contar cuatro dos veces.",
                "Cuatro más cuatro es igual a ocho."),

            new PreguntaDatos(
                "Matemática", "¿Cuál es el doble de 3?", "5", "6", 1,
                "El doble significa sumar el mismo número dos veces.",
                "Tres más tres es igual a seis."),

            new PreguntaDatos(
                "Matemática", "¿Cuál es la mitad de 10?", "5", "4", 0,
                "Reparte diez objetos en dos grupos iguales.",
                "Cada grupo tiene cinco objetos."),

            new PreguntaDatos(
                "Comunicación", "¿Cuál de estas palabras es un animal?", "Perro", "Mesa", 0,
                "Piensa cuál de las dos palabras tiene vida.",
                "El perro es un animal."),

            new PreguntaDatos(
                "Comunicación", "¿Cuál es el plural de casa?", "Casa", "Casas", 1,
                "El plural indica más de una.",
                "Casas es el plural de casa."),

            new PreguntaDatos(
                "Comunicación", "¿Cuál es lo contrario de frío?", "Caliente", "Pequeño", 0,
                "Piensa en la temperatura opuesta.",
                "Caliente es lo contrario de frío."),

            new PreguntaDatos(
                "Comunicación", "¿Qué palabra empieza con la letra M?", "Sapo", "Mano", 1,
                "Pronuncia lentamente la primera letra.",
                "Mano empieza con la letra M."),

            new PreguntaDatos(
                "Comunicación", "Completa la oración: El sol ____.", "brilla", "come", 0,
                "Elige la acción que realiza el sol.",
                "El sol brilla y nos da luz."),

            new PreguntaDatos(
                "Ciencia", "¿En qué planeta vivimos?", "La Tierra", "La Luna", 0,
                "Es el planeta azul.",
                "Vivimos en el planeta Tierra."),

            new PreguntaDatos(
                "Ciencia", "¿Cómo se llama el agua congelada?", "Vapor", "Hielo", 1,
                "Aparece cuando el agua se enfría mucho.",
                "El agua congelada se convierte en hielo."),

            new PreguntaDatos(
                "Ciencia", "¿Qué órgano usamos para respirar?", "Pulmones", "Rodillas", 0,
                "Está dentro del pecho.",
                "Respiramos principalmente con los pulmones."),

            new PreguntaDatos(
                "Ciencia", "¿Qué animal tiene plumas?", "Pez", "Ave", 1,
                "Piensa en los animales que vuelan.",
                "Las aves tienen plumas."),

            new PreguntaDatos(
                "Ciencia", "¿Qué necesitan las plantas para crecer?", "Agua y luz", "Solo plástico", 0,
                "Las plantas reciben energía del sol.",
                "Las plantas necesitan agua y luz para crecer."),

            new PreguntaDatos(
                "Personal Social", "¿Cuál es la capital del Perú?", "Lima", "Cusco", 0,
                "Es la ciudad donde está la sede principal del Gobierno.",
                "Lima es la capital del Perú."),

            new PreguntaDatos(
                "Seguridad", "¿Qué hacemos cuando el semáforo está en rojo?", "Avanzamos", "Nos detenemos", 1,
                "El color rojo indica peligro o alto.",
                "Debemos detenernos cuando el semáforo está en rojo."),

            new PreguntaDatos(
                "Salud", "¿Cuál es una opción más saludable?", "Manzana", "Caramelo", 0,
                "Elige el alimento natural.",
                "La manzana aporta nutrientes y es una opción saludable."),

            new PreguntaDatos(
                "Salud", "¿Qué debemos hacer antes de comer?", "Lavarnos las manos", "Ensuciar la mesa", 0,
                "Ayuda a eliminar microbios.",
                "Lavarnos las manos ayuda a cuidar nuestra salud."),

            new PreguntaDatos(
                "Ambiente", "¿Qué debemos hacer con una hoja de papel usada?", "Reciclarla", "Tirarla al río", 0,
                "Piensa en la acción que protege la naturaleza.",
                "Reciclar papel ayuda a reducir residuos.")
        };
    }
}
