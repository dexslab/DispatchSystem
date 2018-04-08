using MahApps.Metro.Controls;
using Terminal.ViewModels;
using MahApps.Metro.Controls.Dialogs;

namespace Terminal.Views
{
    /// <summary>
    /// Interaction logic for PFMManager.xaml
    /// </summary>
    public partial class MainView : MetroWindow
    {
        private MainViewVM _pfmVM;

        public MainView()
        {
            InitializeComponent();
            _pfmVM = new MainViewVM(this, DialogCoordinator.Instance);
            DataContext = _pfmVM;
        }
    }
}
