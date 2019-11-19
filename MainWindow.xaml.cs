using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using DemoCrudMVVM.ViewModel;


namespace DemoCrudMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FormViewModel fvm;
        readonly SqlConnection con = new SqlConnection(@"Server=GSG1PD-FT0610;Database=DemoCrud;Trusted_Connection=true");
        static int PK_ID;

        public MainWindow()
        {
            InitializeComponent();
            
            //FillData();
        }
        //private void Edit_btn(object sender, RoutedEventArgs e)
        //{
        //    var id1 = (DataRowView)UsersDetails.SelectedItem;
        //    PK_ID = Convert.ToInt32(id1.Row["id"].ToString());
        //    txtname.Text = id1.Row["name"].ToString().Trim();
        //    txtaddress.Text = id1.Row["address"].ToString().Trim();
        //    txtphone.Text = id1.Row["phone"].ToString().Trim();
        //    btnsubmit.Content = "Update";
        //}
        //private void Delete_btn(object sender, RoutedEventArgs e)
        //{
        //    var id1 = (DataRowView)UsersDetails.SelectedItem;
        //    string query = "delete from users where id='" + Convert.ToInt32(id1.Row["id"].ToString()) + "'";
        //    SqlCommand cmd = SqlCommandOperation(query);
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //    FillData();
        //}

        //private void Btnsubmit_Click(object sender, RoutedEventArgs e)
        //{
        //    Regex phoneNumpattern = new Regex(@"\+[0-9]{2}\s+[6-9]{1}[0-9]{9}");

        //    if (btnsubmit.Content.ToString() == "Update")
        //    {
        //        if (phoneNumpattern.IsMatch(txtphone.Text))
        //        {
        //            try
        //            {
        //                string query = "update users set name=@name,address=@address,phone=@phone where id='" + PK_ID + "'";
        //                SqlCommand cmd = SqlCommandOperation(query);
        //                cmd.Parameters.AddWithValue("@name", txtname.Text);
        //                cmd.Parameters.AddWithValue("@address", txtaddress.Text);
        //                cmd.Parameters.AddWithValue("@phone", txtphone.Text);
        //                btnsubmit.Content = "Save";
        //                cmd.ExecuteNonQuery();
        //                con.Close();
        //                FillData();
        //                Clearcontrol();
        //            }

        //            catch (Exception)
        //            {
        //                MessageBox.Show("Error occured during insertion");
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Invalid phone number");
        //        }
        //    }
        //    else
        //    {
        //        if (phoneNumpattern.IsMatch(txtphone.Text))
        //        {
        //            try
        //            {
        //                string query = "insert into users(name,address,phone) values(@name,@address,@phone)";
        //                SqlCommand cmd = SqlCommandOperation(query);
        //                cmd.Parameters.AddWithValue("@name", txtname.Text);
        //                cmd.Parameters.AddWithValue("@address", txtaddress.Text);
        //                cmd.Parameters.AddWithValue("@phone", txtphone.Text);
        //                cmd.ExecuteNonQuery();
        //                con.Close();

        //                FillData();
        //                Clearcontrol();
        //            }
        //            catch (Exception)
        //            {
        //                MessageBox.Show("Error occured during insertion");
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Invalid phone number");
        //        }
        //    }
        //}
        //private void CancelClick(object sender, RoutedEventArgs e)
        //{
        //    Clearcontrol();
        //}
        //private void FillData()
        //{
        //    string query = "Select * from users";
        //    SqlCommand cmd = SqlCommandOperation(query);
        //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    adp.Fill(dt);
        //    con.Close();
        //    UsersDetails.ItemsSource = dt.DefaultView;
        //}
        //private void Clearcontrol()
        //{
        //    txtname.Text = string.Empty;
        //    txtaddress.Text = string.Empty;
        //    txtphone.Text = string.Empty;
        //}
        private SqlCommand SqlCommandOperation(string query)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            return cmd;
        }
    }
}
