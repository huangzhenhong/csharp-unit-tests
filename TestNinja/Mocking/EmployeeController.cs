using System.Data.Entity;

namespace TestNinja.Mocking
{
    public class EmployeeController
    {
        private IEmployeeStorage _employeeStorage;

        public EmployeeController(IEmployeeStorage employeeRepository)
        {
            _employeeStorage = employeeRepository ?? new EmployeeStorage();
        }

        public ActionResult DeleteEmployee(int id)
        {
            _employeeStorage.DeleteEmployee(id);

            return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult { }

    public class RedirectResult : ActionResult { }

    public class Employee
    {
    }
}