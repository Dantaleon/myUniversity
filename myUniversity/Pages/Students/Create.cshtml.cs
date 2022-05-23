using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace myUniversity.Pages.Students
{
    public class CreateModel : PageModel
    {
        public StudentInfo studentInfo = new StudentInfo();

        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            studentInfo.lastname = Request.Form["lastName"];
            studentInfo.firstname = Request.Form["firstName"];
            studentInfo.thirdname = Request.Form["thirdName"];
            studentInfo.gruppa = Request.Form["gruppa"];
            studentInfo.birthDate = Request.Form["birthDate"];

            if (studentInfo.lastname.Length == 0 || studentInfo.firstname.Length == 0
                || studentInfo.thirdname.Length == 0 || studentInfo.gruppa.Length == 0
                || studentInfo.birthDate.Length == 0)
            {
                errorMessage = "Все поля должны быть заполнены";
                return;
            }

            // сохраняем данные в базу данных
            try
            {
                string connectionString = "Data Source=.\\SQLExpress;Initial Catalog=university;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO students " +
                                 "(lastName, firstName, thirdName, gruppa, birthDate) VALUES" +
                                 "(@lastName, @firstName, @thirdName, @gruppa, @birthDate);";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@lastName", studentInfo.lastname);
                        command.Parameters.AddWithValue("@firstName", studentInfo.firstname);
                        command.Parameters.AddWithValue("@thirdName", studentInfo.thirdname);
                        command.Parameters.AddWithValue("@gruppa", studentInfo.gruppa);
                        command.Parameters.AddWithValue("@birthDate", studentInfo.birthDate);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            // очищаем поля и заполняем сообщение об удачном добавлении
            studentInfo.lastname = "";
            studentInfo.firstname = "";
            studentInfo.thirdname = "";
            studentInfo.gruppa = "";
            studentInfo.birthDate = "";

            successMessage = "Студент успешно добавлен";

            Response.Redirect("/Students/Index");
        }
    }
}
