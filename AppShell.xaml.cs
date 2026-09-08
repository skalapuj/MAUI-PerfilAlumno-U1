using PerfilSolMAUI.Views;

namespace PerfilSolMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Agrego la ruta hacia la pagina de detalle para poder usar GoToAsync("detallePerfil")
            Routing.RegisterRoute("detallePerfil", typeof(DetallePage));
        }
    }
}
