using System;
using Cumulative01.Models;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

//This is the controller that will handle the requests from the teacher page.
namespace Cumulative01.Controllers
{
    //This is the route that will be used to access the controller.
    public class TeacherPageController : Controller
    {

        //this is the private variable that will hold the connection to the API controller.
        private readonly TeacherAPIController _api;


        //This is the constructor that will assign the connection to the API controller to the private variable.
        public TeacherPageController(TeacherAPIController api)
        {
            _api = api;
        }

        //This is the method that will be called when the user wants to see the list of teachers.

        /// <summary>
        /// Returns a list of Teachers to be displayed on the page
        /// </summary>
        /// <example>
        /// GET TeacherPage/List -> [{"TeacherId":1,"TeacherFirstName":"Alexendar","TeacherLastName":"Doe",...},..]
        /// </example>
        /// <returns>
        /// A list of Teacher objects containing ID, Name, EmployeeID, HireDate, and Salary
        /// </returns>

        public IActionResult List(string SearchKey)
        {

            //This is the list of teachers that will hold the teachers instances in as objects in the list.
            List<Teacher> Teach = _api.ListTeacherNames(SearchKey);
            //This is the view that will be returned to the user with the list of teachers.
            return View(Teach);
        }

        //This is the method that will be called when the user wants to see the details of a teacher by its ID.

        /// <summary>
        /// Returns details of a specific teacher based on the given ID
        /// </summary>
        /// <example>
        /// GET TeacherPage/Show/1 -> {"TeacherId":1,"TeacherFirstName":"Alexendar","TeacherLastName":"Doe",...}
        /// </example>
        /// <param name="Id">The ID of the teacher</param>
        /// <returns>
        /// A Teacher object containing ID, Name, EmployeeID, HireDate, and Salary
        /// </returns>

        public IActionResult Show(int Id)
        {
            //This is the Teacher Class in the model that will hold the teacher instance in as an object.
            Teacher teach1 = _api.FindTeacher(Id);
            //This is the view that will be returned to the user with the details of the teacher.
            return View(teach1);
        }
        /// <summary>
        /// Adds a new teacher using form data submitted via POST
        /// </summary>
        /// <param name="TeacherFname">Teacher's first name</param>
        /// <param name="TeacherLname">Teacher's last name</param>
        /// <param name="EmployeeID">Employee number</param>
        /// <param name="HireDate">Date the teacher was hired</param>
        /// <param name="Salary">Teacher's salary (as a string to convert to double)</param>
        /// <returns>
        /// Redirects to the List view after successful addition
        /// </returns>

        [HttpPost]
        public IActionResult AddATeacher(
            string TeacherFname, 
            String TeacherLname, 
            String EmployeeID, 
            DateTime HireDate, 
            string Salary
            )

            
        {


            Teacher teacher = new Teacher();
            teacher.TeacherFirstName = TeacherFname;
            teacher.TeacherLastName = TeacherLname;
            teacher.EmployeeID = EmployeeID;
            teacher.HireDate = HireDate;
            double Sal = Convert.ToDouble(Salary);
            teacher.Salary = Sal;
            _api.addATeacher(teacher);


            return RedirectToAction("List");
        }

        /// <summary>
        /// Deletes a teacher by ID and redirects to the list view
        /// </summary>
        /// <param name="ID">The ID of the teacher to delete</param>
        /// <returns>
        /// Redirects to the List view
        /// </returns>

        [HttpPost]
        [Route(template:"/DeleteIsConfirmed/{ID}")]
        public IActionResult Delete(int ID)
        {
            _api.DeleteATeacher(ID);

            return RedirectToAction("List");
        }

        /// <summary>
        /// Shows a confirmation view before deleting a teacher
        /// </summary>
        /// <param name="ID">The ID of the teacher to confirm deletion</param>
        /// <returns>
        /// A view showing teacher details before confirming deletion
        /// </returns>

        [HttpGet]
        [Route(template: "/TeacherPage/DeleteCon/{ID}")]
        public IActionResult DeleteConfirmResult(int ID)
        {

            Teacher teacher = _api.FindTeacher(ID);


            return View(teacher);
        }
        /// <summary>
        /// Adds a new teacher (alternative route)
        /// </summary>
        /// <param name="TeacherFname">Teacher's first name</param>
        /// <param name="TeacherLname">Teacher's last name</param>
        /// <param name="EmployeeID">Employee number</param>
        /// <param name="HireDate">Date the teacher was hired</param>
        /// <param name="Salary">Teacher's salary (as a string to convert to double)</param>
        /// <returns>
        /// Redirects to the List view
        /// </returns>
        
        [HttpPost]
        [Route(template:"/Add")]
        public IActionResult New(string TeacherFname, string TeacherLname, string EmployeeID, DateTime HireDate, string Salary)
        {

            Teacher teacher = new Teacher();
            teacher.TeacherFirstName = TeacherFname;
            teacher.TeacherLastName = TeacherLname;
            teacher.EmployeeID = EmployeeID;
            teacher.HireDate = HireDate;
            double NewSalary = Convert.ToDouble(Salary);
            teacher.Salary = NewSalary;

            _api.addATeacher(teacher);

            return RedirectToAction("List");
        }

        /// <summary>
        /// Returns the form view for adding a new teacher
        /// </summary>
        /// <returns>
        /// A View for entering new teacher information
        /// </returns>

        [HttpGet]
        [Route(template: "/New")]

        public IActionResult New()
        {
            return View();
        }

        /// <summary>
        /// Displays the update form populated with an existing teacher's details.
        /// </summary>
        /// <param name="ID">The ID of the teacher whose details need to be updated.</param>
        /// <returns>
        /// A view that displays the teacher's information in an editable form.
        /// </returns>
        /// <remarks>
        /// This method retrieves a teacher object by ID using the internal API and passes it to the view
        /// so that the user can update the teacher's information.
        /// </remarks>
        /// <example>
        /// GET /TeacherPage/UpdateTeacher/5
        /// </example>


        [HttpGet]
        [Route(template: "/TeacherPage/UpdateTeacher/{ID}")]
        public IActionResult UpdateTeacher(int ID)
        {
            Teacher teacher = _api.FindTeacher(ID);
            return View(teacher);
        }


        /// <summary>
        /// Handles the submission of the updated teacher details from the form and applies changes to the database.
        /// </summary>
        /// <param name="ID">The ID of the teacher being updated.</param>
        /// <param name="TeacherFname">The updated first name of the teacher.</param>
        /// <param name="TeacherLname">The updated last name of the teacher.</param>
        /// <param name="EmployeeID">The updated employee ID of the teacher.</param>
        /// <param name="HireDate">The updated hire date of the teacher.</param>
        /// <param name="Salary">The updated salary of the teacher (string format, converted to double).</param>
        /// <returns>
        /// A redirect to the "Show" view after successfully updating the teacher's information.
        /// </returns>
        /// <remarks>
        /// This method creates a new `Teacher` object with the updated values received from a form.
        /// It converts the salary from string to double before updating the record using the internal API method.
        /// </remarks>
        /// <example>
        /// POST /TeacherPage/UpdateTeacherAPIRoute/5
        /// Form Data:
        /// TeacherFname=Alice
        /// TeacherLname=Smith
        /// EmployeeID=e987654
        /// HireDate=2023-09-01
        /// Salary=60000
        /// </example>



        [HttpPost]
        [Route("/TeacherPage/UpdateTeacherAPIRoute/{ID}")]

        public IActionResult UpdateTeacherAPIRoute(int ID, [FromForm] string TeacherFname, string TeacherLname, string EmployeeID, DateTime HireDate, string Salary)
        {

            Teacher teacher = new Teacher();
            teacher.TeacherFirstName = TeacherFname;
            teacher.TeacherLastName = TeacherLname;
            teacher.EmployeeID = EmployeeID;
            teacher.HireDate = HireDate;
            double NewSalary = Convert.ToDouble(Salary);
            teacher.Salary = NewSalary;




            _api.UpdateAPI(ID, teacher);

            return RedirectToAction("Show", new { Id = ID});
        }

     
    }

}
