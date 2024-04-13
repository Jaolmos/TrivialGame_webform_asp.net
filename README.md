# Aplicación de Juego de Trivial en ASP.NET WebForms

## Descripción
Esta aplicación web desarrollada en VB.NET proporciona un entorno interactivo donde los usuarios pueden participar en un juego de trivial. Los jugadores responden a 10 preguntas aleatorias. 

## Entorno de Desarrollo
- **Framework**: .NET Framework 4.7.2
- **Front-end**: Bootstrap 5 para un diseño responsivo y estéticamente agradable.
- **Back-end**: VB.NET como lenguaje de programación.
- **Patrón de diseño**: Utiliza el patrón Repository para gestionar las operaciones de datos relacionadas con las preguntas y respuestas del juego.

## Estructura de la Base de Datos
La base de datos de la aplicación de Trivial utiliza un diseño entidad-relación para organizar cómo se almacenan las preguntas y respuestas. A continuación se muestra el diagrama entidad-relación que ilustra la estructura de la base de datos:

![Diagrama Entidad-Relación de la Base de Datos Trivial](diagrama_entidad-relacion.png)

## Características
- **Interactividad**: Permite a los jugadores responder preguntas y ver instantáneamente si sus respuestas son correctas.
- **Gestión de preguntas**: Los administradores pueden agregar, editar y eliminar preguntas a través de un panel de administración.
- **Diseño responsivo**: Aprovecha Bootstrap para asegurar que la aplicación funcione bien en dispositivos de diferentes tamaños.

### Estructura de Archivos
La estructura de archivos de la aplicación se organiza de la siguiente manera para facilitar la mantenibilidad y la escalabilidad del proyecto:

- `App_Code/`:
  - `Models/`: Contiene los modelos de datos utilizados en la aplicación.
    - `Pregunta.vb`: Define la estructura de las preguntas.
    - `Respuesta.vb`: Define la estructura de las respuestas.
  - `Repositories/`: Implementa el patrón Repository para abstraer las operaciones de acceso a datos.
    - `IPreguntaRepositorio.vb`: Interfaz para las operaciones del repositorio de preguntas.
    - `PreguntaRepositorio.vb`: Implementación concreta del repositorio de preguntas.
  - `ConexionBD.vb`: Gestiona la conexión a la base de datos `TrivialBD.mdf`.

- `App_Data/`:
  - `TrivialBD.mdf`: Base de datos del proyecto que almacena toda la información relativa a preguntas y respuestas.

- `Content/`:
  - `bootstrap/`: Carpeta que contiene los archivos de Bootstrap necesarios para el diseño responsivo y estilizado de la aplicación.
  - `custom.css`: Hoja de estilos personalizados para adaptar los estilos visuales más allá de Bootstrap.

- `Pages/`:
  - `Default.aspx`: Página principal que muestra las preguntas del juego y recoge respuestas de los usuarios.
  - `Default.aspx.vb`: Codebehind de `Default.aspx` que contiene la lógica de la interfaz de usuario para manejar eventos como la respuesta a preguntas.

Esta estructura está diseñada para separar claramente la lógica de la presentación, la lógica de negocio y el acceso a datos, siguiendo las mejores prácticas de desarrollo de software.


## Configuración y Despliegue
1. Asegúrese de tener instalado el .NET Framework 4.8.
2. Clone el repositorio en su entorno local.
3. Abra el proyecto en Visual Studio.
4. Compile y ejecute la aplicación para asegurarse de que todas las dependencias están correctamente configuradas y que la base de datos es accesible.
5. Acceda a `Default.aspx` para comenzar a jugar.
