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
using Dapper;
using System.Data.SqlClient;
using System.Configuration;

namespace CRUD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginBTN_Click(object sender, RoutedEventArgs e)
        {
            string Mypassword = TB_Password.Password;
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(Mypassword);

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            var log = con.QueryAsync<Login>("select * from TB_M_Login where email = @email",
                      new { email = TB_Email.Text, password = hashPassword }).Result.SingleOrDefault();
            var verify = BCrypt.Net.BCrypt.Verify(Mypassword, log.password);
            
            if (log != null)
            {
                home show = new home(log.role);
                show.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Invalid Email/Password");
            }
        }
    }


}

