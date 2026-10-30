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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Koldyaev_Autoservice
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Service _currentServise = new Service();

        public AddEditPage(Service SelectedService)
        {
            InitializeComponent();
            if(SelectedService != null)
            {
                _currentServise = SelectedService;
            }
            DataContext = _currentServise;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if(string.IsNullOrWhiteSpace(_currentServise.Title))
            {
                errors.AppendLine("Укажите название услуги");
            }
            if (_currentServise.Cost == 0)
            {
                errors.AppendLine("Укажите стоимость услуги");
            }
            if(_currentServise.DurationInSeconds > 240)
            {
                errors.AppendLine("Длительность не может быть больше 240 минут");
            }
            if (_currentServise.Discount < 0 || _currentServise.Discount > 100) 
            {
                errors.AppendLine("Укажите скидку от 0 до 100");
            }
            if (_currentServise.DurationInSeconds == 0)
            {
                errors.AppendLine("Укажите длительность услуги");
            }
            
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var allServices = Kollaev_АвтосервисEntities.GetContext().Service.ToList();
            allServices = allServices.Where(p => p.Title == _currentServise.Title).ToList();

            if (allServices.Count == 0)
            {

                if (_currentServise.ID == 0)
                {
                    Kollaev_АвтосервисEntities.GetContext().Service.Add(_currentServise);

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
            else
            {
                MessageBox.Show("Уже существует такая услуга");
            }
        }
    }
}
