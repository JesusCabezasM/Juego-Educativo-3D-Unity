# Juego-Educativo-3D-Unity
Juego educativo de plataformas 3D desarrollado en Unity para reforzar conocimientos básicos de estudiantes de primaria.


# Juego Educativo de Plataformas 3D

## Descripción

Juego educativo de plataformas 3D desarrollado en Unity para reforzar conocimientos básicos en estudiantes de educación primaria.

El jugador controla un personaje que debe avanzar mediante saltos entre plataformas. Al llegar a una plataforma verde, se presenta una pregunta educativa con dos posibles respuestas ubicadas en plataformas laterales.

## Objetivo del proyecto

Desarrollar una experiencia educativa interactiva que permita reforzar contenidos básicos mediante gamificación, aprendizaje basado en retos y retroalimentación inmediata.

## Tipo de solución

El prototipo corresponde a una experiencia de realidad virtual de escritorio desarrollada en un entorno tridimensional.

La interacción se realiza mediante una computadora, teclado y pantalla. En esta versión no se implementaron funciones de realidad aumentada ni compatibilidad directa con visores de realidad virtual.

## Usuario objetivo

El juego está dirigido principalmente a estudiantes de educación primaria que requieren reforzar contenidos básicos de forma dinámica e interactiva.

Puede utilizarse en:

- Aulas de innovación.
- Laboratorios de cómputo.
- Sesiones de reforzamiento escolar.
- Actividades educativas desde el hogar.

## Funcionalidades implementadas

- Movimiento y salto del personaje.
- Escenario tridimensional con plataformas.
- Tres retos educativos.
- Preguntas con dos alternativas.
- Sistema de tres intentos.
- Pistas después de una respuesta incorrecta.
- Diez puntos por cada respuesta correcta.
- Puntos de reaparición.
- Interfaz de puntaje e intentos.
- Textos desarrollados con TextMeshPro.
- Mensaje final de felicitación.
- Música y efectos visuales.
- Evaluación de rendimiento mediante Unity Profiler.
- Ejecutable para Windows.

## Metodología educativa

El juego utiliza los siguientes enfoques:

- Gamificación educativa.
- Aprendizaje basado en retos.
- Retroalimentación inmediata.
- Dificultad progresiva.
- Refuerzo positivo.

Cuando el estudiante responde incorrectamente, pierde un intento, recibe una pista y regresa al punto de reintento. Cuando responde correctamente, obtiene puntos y puede continuar al siguiente reto.

## Herramientas utilizadas

### Software

- Unity 3D.
- Visual Studio.
- C#.
- TextMeshPro.
- Unity Profiler.
- GitHub y GitHub Desktop.

### Hardware

- Computadora con sistema operativo Windows.
- Teclado y pantalla.

## Estructura del repositorio

- `Assets`: escenas, scripts, modelos, materiales, audios y recursos del juego.
- `Packages`: paquetes y dependencias utilizadas por Unity.
- `ProjectSettings`: configuración general del proyecto.

## Scripts principales

- `GestorPreguntas.cs`
- `PreguntaDatos.cs`
- `ZonaPregunta.cs`
- `ZonaRespuesta.cs`
- `ZonaCaida.cs`
- `PlataformaMovil.cs`
- `FinalJuego.cs`

## Cómo abrir el proyecto

1. Descargar o clonar este repositorio.
2. Abrir Unity Hub.
3. Seleccionar `Add project from disk`.
4. Elegir la carpeta descargada.
5. Abrir el proyecto con una versión compatible de Unity 6.
6. Esperar a que Unity reconstruya la carpeta `Library`.
7. Abrir la escena principal ubicada en `Assets/Scenes/SampleScene`.
8. Presionar `Play` para ejecutar el juego.

## Controles

- Movimiento: teclas configuradas en el controlador del personaje.
- Salto: tecla configurada dentro del proyecto.
- Cerrar el ejecutable: `Alt + F4`.

## Assets utilizados

Los recursos externos empleados en el proyecto provienen principalmente de paquetes gratuitos disponibles en Unity Asset Store.

| Asset o paquete | Uso dentro del proyecto | Origen |
|---|---|---|
| Jammo Character | Personaje principal | Unity Asset Store |
| Fantasy Skybox FREE | Cielo y ambientación | Unity Asset Store |
| Alpine Audio | Música y efectos de sonido | Unity Asset Store |
| Isle of Assets | Vegetación y objetos 3D | Unity Asset Store |
| Turntable Platforms | Plataformas del escenario | Unity Asset Store |

Se recomienda agregar posteriormente el enlace exacto y el nombre del desarrollador de cada paquete utilizado.

## Video de demostración

Video configurado como oculto en YouTube:

https://youtu.be/HyzvivSZQ98

## Build ejecutable

Versión ejecutable para Windows e instrucciones de ejecución:

https://drive.google.com/file/d/1NFJB2k9XWxXgd4NkJQCyXlQTCKrmS7Pa/view?usp=sharing

## Estado del proyecto

El prototipo cuenta con un flujo funcional que incluye:

1. Inicio del juego.
2. Movimiento del personaje.
3. Activación de preguntas.
4. Selección de respuestas.
5. Puntaje e intentos.
6. Pistas y reaparición.
7. Avance entre plataformas.
8. Mensaje final de felicitación.

## Limitaciones

- No se implementó realidad aumentada.
- No se integró un visor de realidad virtual.
- El prototipo se ejecuta actualmente en una computadora con Windows.
- El banco de preguntas puede ampliarse en futuras versiones.
- La experiencia todavía puede recibir mejoras de accesibilidad y optimización visual.

## Mejoras futuras

- Agregar más niveles y preguntas.
- Incorporar diferentes áreas educativas.
- Implementar selección de dificultad.
- Registrar el progreso del estudiante.
- Incorporar compatibilidad con visor VR.
- Agregar narración de preguntas.
- Mejorar accesibilidad, colores y tamaño de textos.
- Incluir un menú principal y opciones de configuración.

## Integrantes

- Colocar nombre del integrante 1.
- Colocar nombre del integrante 2.
- Colocar nombre del integrante 3.
- Colocar nombre del integrante 4.

## Curso

Realidad Virtual y Aumentada.

## Universidad

Universidad Autónoma del Perú.
