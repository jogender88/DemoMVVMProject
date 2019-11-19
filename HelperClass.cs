using DemoCrudMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using Dapper;

namespace DemoCrudMVVM
{
    class HelperClass
    {
        readonly SqlConnection con = new SqlConnection(@"Server=GSG1PD-FT0610;Database=DemoCrud;Trusted_Connection=true");
        private void Delete_btn(string id)
        {
            try
            {
                using (var con = new SqlConnection(@"Server=GSG1PD-FT0610;Database=DemoCrud;Trusted_Connection=true"))
                {
                    string query = "delete from users where id='" + id + "'";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured ");
            }
        }

        public List<UserDetails> Save(FormModel data)
        {
            Regex phoneNumpattern = new Regex(@"\+[0-9]{2}\s+[6-9]{1}[0-9]{9}");

            if (data.SubmitType == "Update")
            {
                if (phoneNumpattern.IsMatch(data.Phone))
                {
                    try
                    {
                        using (var con = new SqlConnection(@"Server=GSG1PD-FT0610;Database=DemoCrud;Trusted_Connection=true"))
                        {
                            string query = "update users set name=@name,address=@address,phone=@phone where id='" + data.Id + "'";
                            con.Open();
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@name", data.Name);
                            cmd.Parameters.AddWithValue("@address", data.Address);
                            cmd.Parameters.AddWithValue("@phone", data.Phone);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            return GetUser(data.Id);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error occured during insertion");
                        return GetUser(data.Id);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid phone number");
                    return GetUser(data.Id);
                }
            }
            else
            {
                if (phoneNumpattern.IsMatch(data.Phone))
                {
                    try
                    {
                        using(var con = new SqlConnection(@"Server=GSG1PD-FT0610;Database=DemoCrud;Trusted_Connection=true"))
                        {
                            string query = "insert into users(name,address,phone) values(@name,@address,@phone)";
                            con.Open();
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@name", data.Name);
                            cmd.Parameters.AddWithValue("@address", data.Address);
                            cmd.Parameters.AddWithValue("@phone", data.Phone);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            return GetUser(data.Id);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error occured during insertion");
                        con.Close();
                        return GetUser(data.Id);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid phone number");
                     con.Close();
                    return GetUser(data.Id);
                }
            }
        }
        public List<UserDetails> GetAllUsers()
        {
            string query = @"Select * from users";
            List<UserDetails> userDetails = new List<UserDetails>();
            try
            {
                using (var con = new SqlConnection(@"Server=GSG1PD-FT0610;Database=DemoCrud;Trusted_Connection=true"))
                {
                    con.Open();
                    userDetails = con.Query<UserDetails>(query).AsList();
                }
                return userDetails;
            }
            catch (Exception)
            {
                MessageBox.Show("error Occured");
                return userDetails;
            }
        }
        public List<UserDetails> GetUser(string Id)
        {
            string query;
            if (Id!=null)
            {
                 query= "select * from users where id = '" +Id + "'";
            }
            else { 
             query = @"SELECT TOP 1 * FROM  users ORDER BY id DESC";
            }

            List<UserDetails> userDetails = new List<UserDetails>();
            try
            {
                using (var con = new SqlConnection(@"Server=GSG1PD-FT0610;Database=DemoCrud;Trusted_Connection=true"))
                {
                    con.Open();
                    userDetails = con.Query<UserDetails>(query).AsList();
                }
                return userDetails;
            }
            catch (Exception)
            {
                MessageBox.Show("error Occured");
                return userDetails;
            }

        }
        public bool Delete(string id)
        {
            string query = "delete from users where id='" + id + "'";
            try { 
            using (var con = new SqlConnection(@"Server=GSG1PD-FT0610;Database=DemoCrud;Trusted_Connection=true"))
            {
                con.Open();
                con.Query(query);
                return true;
            }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
