using Job_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Job_Project.Controllers
{
    public class DeleteJobsController : Controller
    {
        IConfiguration configuration;
        public DeleteJobsController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public int idToDelete;
        public Jobs job;
        public IActionResult DeleteJobs(int id)
        {
            Console.WriteLine(id);
            idToDelete = id;
            string connString = configuration.GetConnectionString("jobsDB");
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"select * from jobs where job_id = {id}";
            var reader = cmd.ExecuteReader();
            job = new Jobs();
            while (reader.Read())
            {

                job.Id = (int)reader["Job_id"];
                job.Name = (string)reader["Job_name"];
                job.employeerName = (string)reader["job_employeer_name"];
                job.category = (string)reader["job_category"];
                job.Description = (string)reader["job_Description"];
            }
            ViewData["job"] = job;
            return View("DeleteJobs");
        }
        [HttpPost]
        public IActionResult DeleteJobs()
        {
            try
            {
                string connString = configuration.GetConnectionString("jobsDB");
                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                int delId = int.Parse(Request.Form["Id"]);
                Console.WriteLine(delId);
                cmd.CommandText = $"delete from jobs where JOB_ID = {delId}";
                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine("rows affected" + rowsAffected);
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View("DeleteConfirmed");
        }

        public IActionResult DeleteConfirmed()
        {



            return View();
        }
    }
}
