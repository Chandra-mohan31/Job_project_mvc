using Microsoft.AspNetCore.Mvc;

namespace Job_Project.Controllers
{
    public class ViewJobsController : Controller
    {
        public IActionResult ViewJobs()
        {
            return View("ViewJobs");
        }
    }
}
