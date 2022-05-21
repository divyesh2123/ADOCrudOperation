using ADOCrudOperation.Models;
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


        public ActionResult CreateEmployee()
        {

            return View();
        }

        [HttpGet]
        public ActionResult EditEmployeeInfo(int employeeId)
        {
            EmpRepository empRepository = new EmpRepository();

            var d = empRepository.GetEmployeeByEmployeeId(employeeId);


            return View("CreateEmployee", d);
        }

        [HttpPost]

        public ActionResult CreateEmployee(EmpModel empModel)
        {
            EmpRepository empRepository = new EmpRepository();
          var emp =  empRepository.AddUpdateEmployee(empModel);


            if (emp)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult DeleteEmployee(int employeeId)
        {
            EmpRepository empRepository = new EmpRepository();

            empRepository.DeleteEmployee(employeeId);

            return RedirectToAction("Index");

        }

    }
}