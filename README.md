# Social Network API

## Descripción

Este proyecto es una API para una red social, construida con ASP.NET Core y MySQL. La API proporciona funcionalidades para gestionar usuarios, publicaciones y seguidores.

## Requisitos

- **MySQL**: Asegúrate de tener MySQL en ejecución antes de comenzar.
- **.NET SDK**: Necesitas tener el SDK de .NET instalado.

## Configuración

1. **Configurar la conexión a la base de datos**:
   - Abre el archivo `infrastructure/api/appsettings.json`.
   - Configura la cadena de conexión para que apunte a tu instancia de MySQL.

2. **Crear la base de datos**:
   - Primero, ejecuta el proyecto `infrastructure/data/data.csproj` para crear automáticamente la base de datos.
   - Puedes hacerlo desde la línea de comandos con:
     ```bash
     dotnet run --project infrastructure/data/data.csproj
     ```

3. **Iniciar el servidor backend**:
   - Ejecuta el proyecto `infrastructure/api/InfrastructureAPI.csproj` para iniciar el servidor backend.
   - Puedes hacerlo desde la línea de comandos con:
     ```bash
     dotnet run --project infrastructure/api/InfrastructureAPI.csproj
     ```

## Endpoints

### Publicaciones

- **Obtener una publicación por ID**
  - **`GET /api/post/{id}`**
  - **Descripción**: Obtiene una publicación específica por su ID.
  - **Respuesta**: `200 OK` con el objeto de publicación.

- **Crear una publicación**
  - **`POST /api/post`**
  - **Descripción**: Crea una nueva publicación.
  - **Cuerpo**: 
    ```json
    {
      "AuthorUsername": "string",
      "Content": "string"
    }
    ```
  - **Respuesta**: `200 OK` con los detalles de la publicación creada.

- **Obtener todas las publicaciones**
  - **`GET /api/post/getallpost`**
  - **Descripción**: Obtiene todas las publicaciones.
  - **Respuesta**: `200 OK` con una lista de publicaciones.

- **Obtener publicaciones por autor**
  - **`GET /api/post/by-author/{authorUsername}`**
  - **Descripción**: Obtiene todas las publicaciones de un autor específico.
  - **Respuesta**: `200 OK` con una lista de publicaciones.

- **Obtener publicaciones de usuarios seguidos**
  - **`GET /api/post/by-following/{username}`**
  - **Descripción**: Obtiene todas las publicaciones de los usuarios que sigue un usuario específico.
  - **Respuesta**: `200 OK` con una lista de publicaciones.

### Usuarios

- **Obtener un usuario por nombre de usuario**
  - **`GET /api/user/GetUser/{username}`**
  - **Descripción**: Obtiene un usuario específico por su nombre de usuario.
  - **Respuesta**: `200 OK` con el objeto de usuario.

- **Crear un nuevo usuario**
  - **`POST /api/user`**
  - **Descripción**: Crea un nuevo usuario.
  - **Cuerpo**:
    ```json
    {
      "Username": "string",
    }
    ```
  - **Respuesta**: `201 Created` con el objeto de usuario creado.

### Seguidores

- **Seguir a un usuario**
  - **`POST /api/follower/follow`**
  - **Descripción**: Permite a un usuario seguir a otro.
  - **Cuerpo**:
    ```json
    {
      "FollowerUsername": "string",
      "FollowedUsername": "string"
    }
    ```
  - **Respuesta**: `200 OK` con un mensaje de éxito.

- **Dejar de seguir a un usuario**
  - **`POST /api/follower/unfollow`**
  - **Descripción**: Permite a un usuario dejar de seguir a otro.
  - **Cuerpo**:
    ```json
    {
      "FollowerUsername": "string",
      "FollowedUsername": "string"
    }
    ```
  - **Respuesta**: `200 OK` con un mensaje de éxito.

- **Obtener seguidores de un usuario**
  - **`GET /api/follower/followers/{username}`**
  - **Descripción**: Obtiene la lista de seguidores de un usuario específico.
  - **Respuesta**: `200 OK` con una lista de seguidores.

- **Obtener usuarios seguidos por un usuario**
  - **`GET /api/follower/followed/{username}`**
  - **Descripción**: Obtiene la lista de usuarios que un usuario específico sigue.
  - **Respuesta**: `200 OK` con una lista de usuarios seguidos.

## Contribuciones

Si deseas contribuir a este proyecto, por favor, envía un pull request o abre un issue para discutir los cambios.

## Licencia

Este proyecto está licenciado bajo la [MIT License](LICENSE).

## Contacto

Para cualquier consulta o comentario, puedes contactarnos a través del [correo electrónico](mailto:andresmg0709@gmail.com).

