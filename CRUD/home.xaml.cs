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
using System.Windows.Shapes;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;


namespace CRUD
{
    /// <summary>
    /// Interaction logic for home.xaml
    /// </summary>
    public partial class home : Window
    {
        public home(string RoleVal)
        {
            InitializeComponent();
            fillCbo();


            string role = RoleVal;
            if (role == "admin1")
            {
                manageData.Visibility = Visibility.Visible;
            }
            else
            {
                manageData.Visibility = Visibility.Collapsed;
            }
        }



        private async void fillCbo()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            var sJob = con.QueryAsync<Job>("exec SP_Select_Job").Result.ToList();
            cboJob.ItemsSource = sJob;

            var sDept = con.QueryAsync<Department>("exec SP_Select_DepartmentName").Result.ToList();
            cboDepartment.ItemsSource = sDept;

            var sMajor = con.QueryAsync<Major>("exec SP_Select_MajorName").Result.ToList();
            cboMajor.ItemsSource = sMajor;

            var sReligion = con.QueryAsync<Religion>("exec SP_Select_ReligionName").Result.ToList();
            cboReligion.ItemsSource = sReligion;

            var sBachelor = con.QueryAsync<Bachelor>("exec SP_Select_BachelorName").Result.ToList();
            cboBachelor.ItemsSource = sBachelor;

            var sDelete = con.QueryAsync<EmployeeVM>("Select name from TB_M_Employee").Result.ToList();
            cboDelete.ItemsSource = sDelete;

            var sDelUser = con.QueryAsync<Login>("Select email from TB_M_Login").Result.ToList();
            cboUser.ItemsSource = sDelUser;

            var sUpdateEmployee = con.QueryAsync<EmployeeVM>("Select Id from TB_M_Employee").Result.ToList();
            cboUpdate.ItemsSource = sUpdateEmployee;

            var aJob = con.QueryAsync<Job>("exec SP_Select_Job").Result.ToList();
            tboJob.ItemsSource = sJob;

            var aDept = con.QueryAsync<Department>("exec SP_Select_DepartmentName").Result.ToList();
            tboDepartment.ItemsSource = sDept;

            var aMajor = con.QueryAsync<Major>("exec SP_Select_MajorName").Result.ToList();
            tboMajor.ItemsSource = sMajor;

            var aReligion = con.QueryAsync<Religion>("exec SP_Select_ReligionName").Result.ToList();
            tboReligion.ItemsSource = sReligion;

            var aBachelor = con.QueryAsync<Bachelor>("exec SP_Select_BachelorName").Result.ToList();
            tboBachelor.ItemsSource = sBachelor;
        }

        public Configuration _configuration;
        public SqlConnection con;
        public SqlDataReader read;
        public SqlCommand cmd;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            insert obj = new insert();
            obj.Show();
        }

        private void EmployeeData_Loaded(object sender, RoutedEventArgs e)
        {

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            var show = con.QueryAsync<Employee>("Exec SP_Select_Employee").Result.ToList();
            var grid = sender as DataGrid;
            grid.ItemsSource = show;
            grid.Items.Refresh();
        }

        private async void InsertBTN_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            var affectedRow = await con.ExecuteAsync("exec SP_Insert_Employee @JobTitle_Id,@Department_Id,@Major_Id,@Name,@PhoneNumber,@Address,@BirthPlace,@BirthDate,@NIK,@Email,@Religion_Id,@NPWP,@Bachelor_Id,@University,@JoinDate", new
            {
                JobTitle_Id = cboJob.SelectedIndex,
                Department_Id = cboDepartment.SelectedIndex,
                Major_Id = cboMajor.SelectedIndex,
                Name = BoxName.Text,
                PhoneNumber = BoxPhone.Text,
                Address = boxAddress.Text,
                BirthPlace = BoxBirth.Text,
                BirthDate = Convert.ToDateTime(boxBirthDate.SelectedDate),
                NIK = BoxNIK.Text,
                Email = BoxEmail.Text,
                Religion_Id = cboReligion.SelectedIndex,
                NPWP = BoxNPWP.Text,
                Bachelor_Id = cboBachelor.SelectedIndex,
                University = BoxUniversity.Text,
                JoinDate = Convert.ToDateTime(BoxJoin.SelectedDate)
            });
            refreshEmployeeProfile();
            refreshEmployeeData();
            MessageBox.Show("Data Successfully Inserted");

        }

        private void refreshEmployeeData()
        {
            var refresh = con.QueryAsync<EmployeeVM>("Exec SP_Select_ALL_Employee").Result.ToList();
            EmployeeData.ItemsSource = refresh;
        }

        private void refreshEmployeeProfile()
        {
            var refresh = con.QueryAsync<EmployeeVM>("Exec SP_Select_ALL_Employee").Result.ToList();
            EmployeeProfile.ItemsSource = refresh;
        }
        private void refreshUserData()
        {
            var refresh = con.QueryAsync<Login>("select * from TB_M_Login").Result.ToList();
            userData.ItemsSource = refresh;
        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {
            EmployeeData.Visibility = Visibility.Collapsed;
            InserData.Visibility = Visibility.Visible;
            EmployeeGrid.Visibility = Visibility.Collapsed;
            stackDelete.Visibility = Visibility.Collapsed;
            userInput.Visibility = Visibility.Collapsed;
            userData.Visibility = Visibility.Collapsed;
            deleteUser.Visibility = Visibility.Collapsed;

        }

        private async void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            var affectedRow = await con.ExecuteAsync("exec SP_Delete_Data @Name", new { Name = cboDelete.SelectedValue });

        }

        private void DashboardBTN_Click(object sender, RoutedEventArgs e)
        {
            EmployeeData.Visibility = Visibility.Visible;
            InserData.Visibility = Visibility.Collapsed;
            EmployeeGrid.Visibility = Visibility.Collapsed;
            stackDelete.Visibility = Visibility.Collapsed;
            dataControl.Visibility = Visibility.Collapsed;
            userInput.Visibility = Visibility.Collapsed;
            userData.Visibility = Visibility.Collapsed;
            deleteUser.Visibility = Visibility.Collapsed;
        }

        private void EmployeeBTN_Click(object sender, RoutedEventArgs e)
        {
            EmployeeGrid.Visibility = Visibility.Visible;
            EmployeeData.Visibility = Visibility.Collapsed;
            InserData.Visibility = Visibility.Collapsed;
            stackDelete.Visibility = Visibility.Collapsed;
            dataControl.Visibility = Visibility.Collapsed;
            userInput.Visibility = Visibility.Collapsed;
            userData.Visibility = Visibility.Collapsed;
            deleteUser.Visibility = Visibility.Collapsed;

        }
        private void EmployeeProfile_Loaded(object sender, RoutedEventArgs e)
        {

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            var show = con.QueryAsync<EmployeeVM>("Exec SP_Select_ALL_Employee").Result.ToList();
            var grid = sender as DataGrid;
            grid.ItemsSource = show;

        }

        private void ManageData_Click(object sender, RoutedEventArgs e)
        {
            dataControl.Visibility = Visibility.Visible;
        }

        private void UpdateData_Click(object sender, RoutedEventArgs e)
        {
            cboUpdate.Visibility = Visibility.Visible;
            pickData.Visibility = Visibility.Visible;
            InserData.Visibility = Visibility.Collapsed;
            userInput.Visibility = Visibility.Collapsed;
            userData.Visibility = Visibility.Collapsed;
            deleteUser.Visibility = Visibility.Collapsed;
            stackDelete.Visibility = Visibility.Collapsed;
            EmployeeGrid.Visibility = Visibility.Collapsed;
            EmployeeData.Visibility = Visibility.Visible;

        }


        private void DeleteData_Click(object sender, RoutedEventArgs e)
        {
            stackDelete.Visibility = Visibility.Visible;
            EmployeeGrid.Visibility = Visibility.Collapsed;
            EmployeeData.Visibility = Visibility.Visible;
            InserData.Visibility = Visibility.Collapsed;
            userInput.Visibility = Visibility.Collapsed;
            userData.Visibility = Visibility.Collapsed;
            deleteUser.Visibility = Visibility.Collapsed;
            cboUpdate.Visibility = Visibility.Collapsed;
            pickData.Visibility = Visibility.Collapsed;
        }

        private void UserAdd_Click(object sender, RoutedEventArgs e)
        {
            userInput.Visibility = Visibility.Visible;
            userData.Visibility = Visibility.Visible;
            InserData.Visibility = Visibility.Collapsed;
            EmployeeGrid.Visibility = Visibility.Collapsed;
            stackDelete.Visibility = Visibility.Collapsed;
            deleteData.Visibility = Visibility.Collapsed;
            EmployeeData.Visibility = Visibility.Collapsed;
            deleteUser.Visibility = Visibility.Visible;
            cboUpdate.Visibility = Visibility.Collapsed;
            pickData.Visibility = Visibility.Collapsed;
        }

        private async void InsertUser_Click(object sender, RoutedEventArgs e)
        {

            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashToStoreInDatabase = BCrypt.Net.BCrypt.HashPassword(TboPassword.Text, salt);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            var affectedRow = await con.ExecuteAsync("exec SP_Add_User @email, @password,@role",
                new
                {
                    email = TboEmail.Text,
                    password = hashToStoreInDatabase,
                    role = TboRole.Text
                });
            refreshUserData();
            MessageBox.Show("Data Successfully Inserted");
        }

        private void UserData_Loaded(object sender, RoutedEventArgs e)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            var show = con.QueryAsync<Login>("Select * from TB_M_Login").Result.ToList();
            var grid = sender as DataGrid;
            grid.ItemsSource = show;
            refreshUserData();
        }

        private async void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            var affectedRow = await con.ExecuteAsync("exec SP_Delete_User @email", new { email = cboUser.SelectedValue });
        }

        private void PickData_Click(object sender, RoutedEventArgs e)
        {
            gridUpdate.Visibility = Visibility.Visible;
            userInput.Visibility = Visibility.Collapsed;
            userData.Visibility = Visibility.Collapsed;
            InserData.Visibility = Visibility.Collapsed;
            EmployeeGrid.Visibility = Visibility.Collapsed;
            stackDelete.Visibility = Visibility.Collapsed;
            deleteData.Visibility = Visibility.Collapsed;
            EmployeeData.Visibility = Visibility.Collapsed;
            deleteUser.Visibility = Visibility.Collapsed;

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            var show = con.QueryAsync<EmployeeVM>("Exec SP_Select_UpdateFile @Id", new
            {

            }).Result.ToList();

        }

        private async void UpdateBTN_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            var affectedRow = await con.ExecuteAsync("exec SP_Update_Data @JobTitle_Id,@Department_Id,@Major_Id,@Name,@PhoneNumber,@Address,@BirthPlace,@BirthDate,@NIK,@Email,@Religion_Id,@NPWP,@Bachelor_Id,@University,@JoinDate", new
            {
                JobTitle_Id = tboJob.SelectedIndex,
                Department_Id = tboDepartment.SelectedIndex,
                Major_Id = tboMajor.SelectedIndex,
                Name = tboName.Text,
                PhoneNumber = tboPhone.Text,
                Address = tboAddress.Text,
                BirthPlace = tboBirth.Text,
                BirthDate = Convert.ToDateTime(tboBirthDate.SelectedDate),
                NIK = tboNIK.Text,
                Email = tboEmail.Text,
                Religion_Id = tboReligion.SelectedIndex,
                NPWP = tboNPWP.Text,
                Bachelor_Id = tboBachelor.SelectedIndex,
                University = tboUniversity.Text,
                JoinDate = Convert.ToDateTime(tboJoin.SelectedDate)
            });
            MessageBox.Show("Data Successfully Updated ");
            refreshEmployeeProfile();
        }
    }
}

