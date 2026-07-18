JUEGO EDUCATIVO DE PLATAFORMAS - INSTALACIÓN RÁPIDA

ARCHIVOS
- PreguntaDatos.cs
- GestorPreguntas.cs
- ZonaPregunta.cs
- ZonaRespuesta.cs
- ZonaCaida.cs
- PlataformaMovil.cs

1. COPIAR SCRIPTS
Crea la carpeta Assets/Scripts/Educativo y coloca dentro los seis archivos .cs.
Espera a que Unity termine de compilar y corrige cualquier error de Console antes de continuar.

2. PREPARAR TEXTMESHPRO
Ve a Window > TextMeshPro > Import TMP Essential Resources.
Crea un Canvas para el HUD y agrega tres textos:
- Intentos
- Puntaje
- Mensaje

3. CONFIGURAR EL GESTOR
Selecciona el objeto GameManager.
Agrega el componente GestorPreguntas.
Arrastra los tres textos del HUD a sus campos.
Crea un Empty llamado PuntoInicioGlobal y colócalo donde comienza el jugador.
Arrástralo al campo Punto Inicio Global.
El gestor incluye 20 preguntas básicas si el banco queda vacío.

4. ETIQUETAR AL JUGADOR
Selecciona el objeto principal del personaje Jammo_Player.
En Tag, selecciona Player.
Si no existe, créalo desde Add Tag.

5. PLATAFORMA VERDE DE PREGUNTA
No conviertas el collider sólido de la plataforma en Trigger.
Crea un hijo vacío llamado TriggerPregunta.
Agrega Box Collider, marca Is Trigger y colócalo un poco sobre la superficie.
Agrega ZonaPregunta al hijo TriggerPregunta.

Crea tres textos 3D con GameObject > 3D Object > Text - TextMeshPro:
- TextoPregunta, sobre la plataforma verde.
- TextoRespuestaIzquierda, sobre la plataforma ploma izquierda.
- TextoRespuestaDerecha, sobre la plataforma ploma derecha.

6. PLATAFORMAS PLOMAS DE RESPUESTA
En cada plataforma ploma crea un hijo vacío:
- TriggerRespuestaIzquierda
- TriggerRespuestaDerecha

Agrega Box Collider y marca Is Trigger.
Agrega ZonaRespuesta a cada hijo.
En ZonaPregunta, arrastra ambos objetos a:
- Zona Respuesta Izquierda
- Zona Respuesta Derecha

También arrastra los tres textos TMP a ZonaPregunta.

7. PUNTO DE REINTENTO
Crea un Empty sobre la plataforma verde, ligeramente elevado.
Nómbralo PuntoReintento.
Arrástralo al campo Punto Reintento de ZonaPregunta.

8. ÍNDICE DE PREGUNTA
- Usa -1 para tomar la siguiente pregunta automáticamente.
- Usa 0 a 19 para fijar una pregunta concreta.
Para aprendizaje progresivo, asigna primero 0 a 4, luego 5 a 9 y después 10 a 19.

9. ZONA DE CAÍDA
Crea un Cube grande debajo de todo el mapa.
Desactiva Mesh Renderer.
En Box Collider activa Is Trigger.
Agrega ZonaCaida.
Al caer, se resta una vida y el jugador reaparece.

10. PLATAFORMAS MÓVILES
Agrega PlataformaMovil a la plataforma.
Ajusta Desplazamiento y Velocidad.
Si usa Rigidbody, activa Is Kinematic o deja que el script lo haga.

11. AUDIO
En GestorPreguntas asigna:
- Fuente Audio
- Sonido Correcto
- Sonido Incorrecto
El sistema usa PlayOneShot y no interrumpe la música de fondo.

12. PRUEBA MÍNIMA
- Pisar verde: aparecen pregunta y opciones.
- Elegir correcta: suma 10 puntos y permite continuar.
- Elegir incorrecta: resta un intento, muestra pista y regresa a verde.
- Fallar 3 veces: recarga la escena desde el inicio.
- Caer: resta una vida y reaparece.

IMPORTANTE
La escena debe estar agregada en File > Build Profiles > Scene List para que el reinicio funcione correctamente.
