using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SqlClientOperations
{
    public class UserQueryProcessors : IUserQueryProcessors
    {
        private string _connectionString;
        public UserQueryProcessors(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
        }

        public List<User> Select()
        {
            var userList = new List<User>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Users", con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        userList.Add(new User()
                        {
                            Id = Int32.Parse(rdr[0].ToString()),
                            Name = rdr[1].ToString(),
                            Age = Int32.Parse(rdr[2].ToString())
                        });
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Log : There is a sql error. Error Message : " + ex.Message);
            }
            return userList;
        }

        public bool Delete(int Id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand("DELETE FROM Users WHERE Id=" + Id, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Log: There is a sql error from delete statement. Error: " + ex.Message);
                return false;
            }
            return true;
        }

        public bool Insert(User user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand("INSERT INTO Users(Id,Name,Age) VALUES (@Id,@Name,@Age)", con);
                    cmd.Parameters.AddWithValue("@Id", user.Id);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Age", user.Age);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Log: There is a sql error from insert statement. Error: " + ex.Message);
                return false;
            }
            return true;
        }

        public bool Update(int Id, User user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand("UPDATE Users SET Name=@Name,Age=@Age WHERE Id=" + Id, con);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Age", user.Age);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Log: There is a sql error from update statement. Error: " + ex.Message);
                return false;
            }
            return true;
        }
    }
}
