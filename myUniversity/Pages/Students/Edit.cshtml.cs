using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace myUniversity.Pages.Students
{
    public class EditModel : PageModel
    {
        public StudentInfo studentInfo = new StudentInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
                string connectionString = "Data Source=.\\SQLExpress;Initial Catalog=university;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM students WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                studentInfo.id = "" + reader.GetInt32(0);
                                studentInfo.lastname = reader.GetString(1);
                                studentInfo.firstname = reader.GetString(2);
                                studentInfo.thirdname = reader.GetString(3);
                                studentInfo.gruppa = reader.GetString(4);
                                studentInfo.birthDate = reader.GetDateTime(5).ToString("dd.MM.yyyy");
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost()
        {
            studentInfo.id = Request.Form["id"];
            studentInfo.lastname = Request.Form["lastName"];
            studentInfo.firstname = Request.Form["firstName"];
            studentInfo.thirdname = Request.Form["thirdName"];
            studentInfo.gruppa = Request.Form["gruppa"];
            studentInfo.birthDate = Request.Form["birthDate"];

            if (studentInfo.id.Length == 0 || studentInfo.lastname.Length == 0 
                || studentInfo.firstname.Length == 0 || studentInfo.thirdname.Length == 0 
                || studentInfo.gruppa.Length == 0 || studentInfo.birthDate.Length == 0)
            {
                errorMessage = "Все поля должны быть заполнены";
                return;
            }

            try
            {
                string connectionString = "Data Source=.\\SQLExpress;Initial Catalog=university;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE students " +
                                 "SET lastName = @lastName, firstName = @firstName," +
                                 "thirdName = @thirdName, gruppa = @gruppa, birthDate = @birthDate " +
                                 "WHERE id = @id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@lastName", studentInfo.lastname);
                        command.Parameters.AddWithValue("@firstName", studentInfo.firstname);
                        command.Parameters.AddWithValue("@thirdName", studentInfo.thirdname);
                        command.Parameters.AddWithValue("@gruppa", studentInfo.gruppa);
                        command.Parameters.AddWithValue("@birthDate", studentInfo.birthDate);
                        command.Parameters.AddWithValue("@id", studentInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Students/Index");
        }
    }
}
