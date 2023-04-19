using Job_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Job_Project.Controllers
{
    public class UpdateJobsController : Controller
    {
        IConfiguration configuration;
        public UpdateJobsController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult UpdateJobs(int id)
        {
            Console.WriteLine("update method id: " + id);

            string connString = configuration.GetConnectionString("jobsDB");
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"select * from jobs where job_id = {id}";
            var reader = cmd.ExecuteReader();
            Jobs job = new Jobs();
            while (reader.Read())
            {

                job.Id = (int)reader["Job_id"];
                job.Name = (string)reader["Job_name"];
                job.employeerName = (string)reader["job_employeer_name"];
                job.category = (string)reader["job_category"];
                job.Description = (string)reader["job_Description"];
            }
            ViewData["job"] = job;
            return View("UpdateJobs");
        }

        [HttpPost]
        public IActionResult UpdateJobs()
        {
            Console.WriteLine("create job method");
            Jobs job = new Jobs();
           
            job.Id = int.Parse(Request.Form["Id"]);
            job.Name = (string)Request.Form["Name"];
            job.category = (string)Request.Form["category"];
            job.employeerName = (string)Request.Form["Employeer Name"];
            job.Description = (string)Request.Form["Description"];
            Console.WriteLine("Inside Post");
            Console.WriteLine(job.employeerName);

            try
            {
                string connString = configuration.GetConnectionString("jobsDB");
                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                Console.WriteLine(job.Id);
                Console.WriteLine(job.Name);
                Console.WriteLine(job.Description);
                Console.WriteLine(job.employeerName);

                cmd.CommandText = $"UPDATE JOBS SET JOB_NAME = '{job.Name}',JOB_CATEGORY='{job.category}',JOB_EMPLOYEER_NAME='{job.employeerName}',JOB_DESCRIPTION='{job.Description}' where JOB_ID = {job.Id}";
                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine("rows affected" + rowsAffected);
                //Response.Redirect("/");
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("ViewJobs","ViewJobs");
        }

        public IActionResult UpdateConfirmed()
        {
            return View();
        }
    }
}
