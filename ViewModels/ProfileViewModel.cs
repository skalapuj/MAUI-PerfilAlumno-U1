using PerfilSolMAUI.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PerfilSolMAUI.ViewModels
{
    // Implementa INotifyPropertyChanged para avisar a la pantalla cuando los datos cambian
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

            // Campos editables
            NombreEditable = _perfil.Nombre;
            EdadEditable = _perfil.Edad.ToString();
            DescripcionEditable = _perfil.Descripcion;
            ImagenUrlEditable = _perfil.ImagenUrl;

            // Asociamos el botón con su método
            GuardarPerfilCommand = new Command(GuardarPerfil);
        }

        // Propiedades de lectura de la tarjeta superior
        public string NombreDisplay => _perfil.Nombre;
        public string EdadDisplay => $"{_perfil.Edad} años";
        public string DescripcionDisplay => _perfil.Descripcion;
        public string ImagenUrlDisplay => _perfil.ImagenUrl;

        // Propiedades enlazadas a los campos de texto (TwoWay)
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

        // Acción del botón Guardar
        public ICommand GuardarPerfilCommand { get; }

        private void GuardarPerfil()
        {
            // Nombre obligatorio
            if (string.IsNullOrWhiteSpace(NombreEditable))
            {
                MensajeEstado = "El nombre no puede estar vacío.";
                ColorMensaje = Colors.Red;
                return;
            }

            // Edad numérica y mayor a 18
            if (!int.TryParse(EdadEditable, out int edadValida) || edadValida <= 18)
            {
                MensajeEstado = "Ingresá una edad válida en números.";
                ColorMensaje = Colors.Red;
                return;
            }

            // Si pasa las validaciones, actualizamos el modelo
            _perfil.Nombre = NombreEditable.Trim();
            _perfil.Edad = edadValida;
            _perfil.Descripcion = DescripcionEditable.Trim();
            _perfil.ImagenUrl = string.IsNullOrWhiteSpace(ImagenUrlEditable)
                ? "https://picsum.photos/200"
                : ImagenUrlEditable.Trim();

            // Notificamos a la UI para que refresque la tarjeta
            OnPropertyChanged(nameof(NombreDisplay));
            OnPropertyChanged(nameof(EdadDisplay));
            OnPropertyChanged(nameof(DescripcionDisplay));
            OnPropertyChanged(nameof(ImagenUrlDisplay));

            MensajeEstado = "¡Perfil actualizado con éxito!";
            ColorMensaje = Colors.Green;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}