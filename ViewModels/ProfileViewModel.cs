using PerfilSolMAUI.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace PerfilSolMAUI.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private UserProfile _perfil;

        private string _nombreEditable = string.Empty;
        private string _edadEditable = string.Empty;
        private string _descripcionEditable = string.Empty;
        private string _imagenUrlEditable = string.Empty;

        private string _mensajeEstado = string.Empty;
        private Color _colorMensaje = Colors.Gray;

        public ProfileViewModel()
        {
            // Datos iniciales
            _perfil = new UserProfile
            {
                Nombre = "Sol Kalapuj",
                Edad = 28,
                Descripcion = "Estudiante de Programación.",
                ImagenUrl = "https://picsum.photos/200"
            };

            // Carga inicial en campos editables
            NombreEditable = _perfil.Nombre;
            EdadEditable = _perfil.Edad.ToString();
            DescripcionEditable = _perfil.Descripcion;
            ImagenUrlEditable = _perfil.ImagenUrl;

            // Accion del comando
            GuardarPerfilCommand = new Command(async () => await GuardarYNavegarAsync());
        }

        // Propiedades para mostrar en pantalla
        public string NombreDisplay => _perfil.Nombre;
        public string EdadDisplay => $"{_perfil.Edad} años";
        public string DescripcionDisplay => _perfil.Descripcion;
        public string ImagenUrlDisplay => _perfil.ImagenUrl;

        public string NombreEditable
        {
            get => _nombreEditable;
            set
            {
                if (_nombreEditable != value)
                {
                    _nombreEditable = value;
                    OnPropertyChanged();
                }
            }
        }

        public string EdadEditable
        {
            get => _edadEditable;
            set
            {
                if (_edadEditable != value)
                {
                    _edadEditable = value;
                    OnPropertyChanged();
                }
            }
        }

        public string DescripcionEditable
        {
            get => _descripcionEditable;
            set
            {
                if (_descripcionEditable != value)
                {
                    _descripcionEditable = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ImagenUrlEditable
        {
            get => _imagenUrlEditable;
            set
            {
                if (_imagenUrlEditable != value)
                {
                    _imagenUrlEditable = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MensajeEstado
        {
            get => _mensajeEstado;
            set
            {
                if (_mensajeEstado != value)
                {
                    _mensajeEstado = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color ColorMensaje
        {
            get => _colorMensaje;
            set
            {
                if (_colorMensaje != value)
                {
                    _colorMensaje = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand GuardarPerfilCommand { get; }

        private async Task GuardarYNavegarAsync()
        {
            if (Shell.Current == null)
                return;

            
            if (string.IsNullOrWhiteSpace(NombreEditable))
            {
                MensajeEstado = "El nombre no puede estar vacío.";
                ColorMensaje = Colors.Red;

                await Shell.Current.DisplayAlertAsync("Validación", MensajeEstado, "Aceptar");
                return;
            }

            if (!int.TryParse(EdadEditable, out int edadValida) || edadValida <= 18)
            {
                MensajeEstado = "Ingresá una edad válida mayor a 18 años.";
                ColorMensaje = Colors.Red;

                await Shell.Current.DisplayAlertAsync("Validación", MensajeEstado, "Aceptar");
                return;
            }

            // 2. Actualizacion del modelo interno
            _perfil.Nombre = NombreEditable.Trim();
            _perfil.Edad = edadValida;
            _perfil.Descripcion = DescripcionEditable.Trim();
            _perfil.ImagenUrl = string.IsNullOrWhiteSpace(ImagenUrlEditable)
                ? "https://picsum.photos/200"
                : ImagenUrlEditable.Trim();

            // Refrescar UI local
            OnPropertyChanged(nameof(NombreDisplay));
            OnPropertyChanged(nameof(EdadDisplay));
            OnPropertyChanged(nameof(DescripcionDisplay));
            OnPropertyChanged(nameof(ImagenUrlDisplay));

            MensajeEstado = "¡Perfil validado con éxito!";
            ColorMensaje = Colors.Green;

            // 3. Notificacion visual de confirmacion antes de navegar
            await Shell.Current.DisplayAlertAsync("Éxito", "Datos validados correctamente. Redirigiendo al detalle...", "Continuar");

            // 4. Navegacion centralizada via Shell con parametros codificados
            string nombreEscapado = Uri.EscapeDataString(_perfil.Nombre);
            string descEscapada = Uri.EscapeDataString(_perfil.Descripcion);
            string imagenEscapada = Uri.EscapeDataString(_perfil.ImagenUrl);

            string rutaCompleta = $"detallePerfil?NombreParam={nombreEscapado}&EdadParam={_perfil.Edad}&DescripcionParam={descEscapada}&ImagenParam={imagenEscapada}";

            await Shell.Current.GoToAsync(rutaCompleta);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}