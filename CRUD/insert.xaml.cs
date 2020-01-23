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

        private void InsertBTN_Click(object sender, RoutedEventArgs e)
        {
            Configuration configuration;
            //string.Format("{0:dd-MM-yyyy}", date);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            var log = con.QueryAsync<EmployeeVM>("Exec SP_Insert_Employee @Id,@JobName,@DepartmentName,@MajorName,@Name,@PhoneNumber,@BirthPlace,@BirthDate,@NIK,@Email," +
                "@Religion,@NPWP,@University,@JoinDate",
                      new {Id = boxId, JobName = BoxJob, DepartmentName = BoxDepartment, MajorName = BoxMajor, Name = BoxName,
                      PhoneNumber = BoxPhone, BirthPlace = BoxBirth, BirthDate = boxBirthDate, NIK = BoxNIK, Email = BoxEmail,
                      Religion = BoxReligion, NPWP = BoxNPWP, University = BoxUniversity, JoinDate = BoxJoin
                      });
            if(log != null){
                MessageBox.Show("Data Successfully Inserted");
            }
            else
            {
                MessageBox.Show("Failed to Insert Data");
            }
        }
    }
}
