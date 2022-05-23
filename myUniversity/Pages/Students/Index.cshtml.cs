using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace myUniversity.Pages.Students
{
    public class IndexModel : PageModel
    {
        public List<StudentInfo> listStudents = new List<StudentInfo>();
        public void OnGet()
        {
            try
            {
                string connetcionString = "Data Source=.\\SQLExpress;Initial Catalog=university;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connetcionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM students";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentInfo studInf = new StudentInfo();
                                studInf.id = "" + reader.GetInt32(0);
                                studInf.lastname = reader.GetString(1);
                                studInf.firstname = reader.GetString(2);
                                studInf.thirdname = reader.GetString(3);
                                studInf.gruppa = reader.GetString(4);
                                studInf.birthDate = reader.GetDateTime(5).ToString("dd.MM.yyyy");

                                listStudents.Add(studInf);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class StudentInfo
    {
        public string id;
        public string lastname;
        public string firstname;
        public string thirdname;
        public string gruppa;
        public string birthDate;
    }
}
