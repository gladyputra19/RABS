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
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace CRUD
{
    /// <summary>
    /// Interaction logic for insert.xaml
    /// </summary>
    public partial class insert : Window
    {

        public insert()
        {
            InitializeComponent();
        }

        private async void InsertBTN_Click(object sender, RoutedEventArgs e)
        {
            Configuration configuration;
            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            var affectedRow = await  con.ExecuteAsync("exec SP_Insert_Employee @JobTitle_Id,@Department_Id,@Major_Id,@Name,@PhoneNumber,@Address,@BirthPlace,@BirthDate,@NIK,@Email,@Religion_Id,@NPWP,@Bachelor_Id,@University,@JoinDate", new {
                JobTitle_Id = cboJob.SelectedValue,
                Department_Id = cboDepartment.SelectedValue,
                Major_Id = cboMajor.SelectedValue,
                Name = BoxName.Text,
                PhoneNumber = BoxPhone.Text,
                Address = boxAddress.Text,
                BirthPlace = BoxBirth.Text,
                BirthDate = boxBirthDate.SelectedDate,
                NIK = BoxNIK.Text,
                Email = BoxEmail.Text,
                Religion_Id = BoxReligion.Text,
                NPWP = BoxNPWP.Text,
                Bachelor_Id = cboBachelor.SelectedValue,
                University = BoxUniversity.Text,
                JoinDate = BoxJoin.Text
            });
            MessageBox.Show("Data Successfully Inserted");
            //if(log != null){
            //    
            //}
            //else
            //{
            //    MessageBox.Show("Failed to Insert Data");
            //}
        }
    }
}
