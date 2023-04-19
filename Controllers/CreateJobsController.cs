using Job_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Job_Project.Controllers
{
    public class CreateJobsController : Controller
    {
        public List<Jobs> jobsList;
        IConfiguration configuration;
        public CreateJobsController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult CreateJobs()
        {
            return View("CreateJobs");
        }
        [HttpPost]
        public IActionResult CreateJobs(Jobs job)
        {
            Console.WriteLine("create job method");
            job = new Jobs();
            job.Name = (string)Request.Form["Name"];
            job.category = (string)Request.Form["category"];
            job.employeerName = (string)Request.Form["Employeer Name"];
            job.Description = (string)Request.Form["Description"];
            Console.WriteLine("Inside Post");
            try
            {
                string connString = configuration.GetConnectionString("jobsDB");
                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"insert into jobs(JOB_NAME,JOB_DESCRIPTION,JOB_CATEGORY,JOB_EMPLOYEER_NAME) values('{job.Name}','{job.Description}','{job.category}','{job.employeerName}')";
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("ViewJobs","ViewJobs");
        }

    }
}
