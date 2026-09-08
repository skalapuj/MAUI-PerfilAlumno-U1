# Aplicación Perfil de Alumno - Navegación y Arquitectura MVVM

Esta aplicación fue desarrollada con .NET MAUI bajo el patrón de diseño MVVM (Model-View-ViewModel). La solución centraliza la navegación y la lógica de validación íntegramente dentro de los ViewModels, desacoplando la interfaz gráfica del flujo de datos y navegación de la aplicación.  
## 1. Arquitectura y Componentes del Sistema
- Modelo (Models/UserProfile.cs): Representa la entidad de datos pura (Nombre, Edad, Descripción, ImagenUrl).  
- Vistas (Views/):
    - MainPage.xaml: Formulario interactivo de edición conectado a su ViewModel mediante Data Binding bidireccional (TwoWay).  
    - DetallePage.xaml: Pantalla de destino que muestra la tarjeta con la información confirmada y el botón para retornar.  
- ViewModels (ViewModels/):
    - ProfileViewModel.cs: Contiene las propiedades observables (INotifyPropertyChanged), la lógica de validación de campos, la emisión de notificaciones visuales y el comando asíncrono que dispara la navegación.  
    - DetalleViewModel.cs: Utiliza anotaciones [QueryProperty] para recibir los parámetros enviados por la URL de navegación de Shell y transformarlos en propiedades enlazables para la vista de destino.  

## 2. Flujo de Navegación
El siguiente diagrama detalla la interacción entre capas, desde la entrada del usuario hasta la llegada a la página de destino y el retorno:
```
               [ VISTA: MainPage.xaml ]
                          │
       (Usuario presiona "Guardar y Ver Perfil")
                          │
                          ▼
            [ VIEWMODEL: ProfileViewModel ]
                          │
             ¿Validaciones correctas?
             - Nombre != vacío
             - Edad > 18 (numérico)
                    │           │
                 [ NO ]       [ SÍ ]
                    │           │
                    │           ├─► Notificación Visual de Éxito
                    │           │   (DisplayAlertAsync)
                    ▼           │
           Notificación Error   ▼
          (DisplayAlertAsync)   Navegación Asíncrona con Parámetros URI:
                                Shell.Current.GoToAsync("detallePerfil?...")
                                                        │
 ┌──────────────────────────────────────────────────────┘
 │
 ▼
[ REGISTRO DE RUTAS: AppShell.xaml.cs ]
 │  Routing.RegisterRoute("detallePerfil", typeof(DetallePage))
 │
 ▼
[ VIEWMODEL RECEPTOR: DetalleViewModel ]
 │  Captura de parámetros vía [QueryProperty]:
 │  - NombreParam  ──► NombreRecibido
 │  - EdadParam    ──► EdadRecibida
 │  - DescParam    ──► DescripcionRecibida
 │  - ImagenParam  ──► ImagenRecibida
 │
 ▼
[ VISTA DESTINO: DetallePage.xaml ]
 │  Muestra la tarjeta de perfil con los datos recibidos
 │
 └─► (Usuario presiona "Volver a Editar")
            │
            ▼
     Comando Volver: Shell.Current.GoToAsync("..")
            │
            ▼
   Regreso a MainPage.xaml
```

## 3. Buenas Prácticas Implementadas
1. Centralización en ViewModels: Las llamadas a Shell.Current.GoToAsync() se ejecutan dentro del comando GuardarPerfilCommand en ProfileViewModel.cs, garantizando que el archivo code-behind (.xaml.cs) permanezca libre de lógica de navegación y de negocio.  
2. Validación previa de parámetros: Se corrobora que los datos cumplan las condiciones requeridas (cadena no vacía y formato numérico válido) antes de construir la ruta de navegación.  
3. Codificación de URIs: El uso de Uri.EscapeDataString y Uri.UnescapeDataString previene errores de sintaxis causados por espacios, saltos de línea o caracteres especiales al pasar argumentos mediante cadenas de consulta en Shell.  
4. Notificaciones visuales: Se implementaron diálogos modales asíncronos nativos (DisplayAlertAsync) para informar al usuario sobre fallos de validación o el éxito de la operación antes de navegar.  
5. Navegación jerárquica con Shell: Implementación de la ruta relativa "//" o ".." para desapilar la página de detalle y regresar de forma estándar al formulario original.  