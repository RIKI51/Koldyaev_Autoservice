using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Koldyaev_Autoservice
{
    /// <summary>
    /// Логика взаимодействия для SingUpPage.xaml
    /// </summary>
    public partial class SingUpPage : Page
    {
        private Service _currentService = new Service();
        public SingUpPage(Service SelectedService)
        {
            InitializeComponent();
            if(SelectedService != null)
            this._currentService = SelectedService;

            DataContext = _currentService;

            var _currentClient = Kollaev_АвтосервисEntities.GetContext().Client.ToList();
            ComboClient.ItemsSource = _currentClient;
        }

        private ClientService _currentClientService = new ClientService();
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            
            if(ComboClient.SelectedItem == null)
            {
                errors.AppendLine("Укажите ФИО клиента");
            }
            if(StartDate.Text == "")
            {
                errors = errors.AppendLine("Укажите дату услуги");
            }
            if(TBStart.Text == "")
            {
                errors.AppendLine("Укажите время начала услуги");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            _currentClientService.ClientID = ComboClient.SelectedIndex + 1;
            _currentClientService.ServiceID = _currentService.ID;
            _currentClientService.StartTime = Convert.ToDateTime(StartDate.Text + " " + TBStart.Text);

            if (_currentClientService.ID == 0)
            {
                Kollaev_АвтосервисEntities.GetContext().ClientService.Add(_currentClientService);
            }

            try
            {
                Kollaev_АвтосервисEntities.GetContext().SaveChanges();
                MessageBox.Show("информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
