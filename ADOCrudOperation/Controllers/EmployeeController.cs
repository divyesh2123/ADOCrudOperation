using ADOCrudOperation.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADOCrudOperation.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            EmpRepository empRepository = new EmpRepository();

            var data = empRepository.GetAllEmployee();

            return View(data);
        }
    }
}