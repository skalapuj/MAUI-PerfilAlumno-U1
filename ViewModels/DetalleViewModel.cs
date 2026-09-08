using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace PerfilSolMAUI.ViewModels
{
    // QueryProperty para mapear los nombres de la URL
    [QueryProperty(nameof(NombreRecibido), "NombreParam")]
    [QueryProperty(nameof(EdadRecibida), "EdadParam")]
    [QueryProperty(nameof(DescripcionRecibida), "DescripcionParam")]
    [QueryProperty(nameof(ImagenRecibida), "ImagenParam")]
    public class DetalleViewModel : INotifyPropertyChanged
    {
        private string _nombreRecibido = string.Empty;
        private string _edadRecibida = string.Empty;
        private string _descripcionRecibida = string.Empty;
        private string _imagenRecibida = string.Empty;

        public DetalleViewModel()
        {
            // Comando para volver atras mediante Shell
            VolverCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        public string NombreRecibido
        {
            get => _nombreRecibido;
            set
            {
                _nombreRecibido = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
            }
        }

        public string EdadRecibida
        {
            get => _edadRecibida;
            set
            {
                _edadRecibida = $"{value} años";
                OnPropertyChanged();
            }
        }

        public string DescripcionRecibida
        {
            get => _descripcionRecibida;
            set
            {
                _descripcionRecibida = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
            }
        }

        public string ImagenRecibida
        {
            get => _imagenRecibida;
            set
            {
                _imagenRecibida = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
            }
        }

        public ICommand VolverCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
