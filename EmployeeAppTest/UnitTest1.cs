using EmpApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xunit.Sdk;


namespace EmployeeAppTest
{
    [TestClass]
    public class UnitTest1
    {
       
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void testEmployeeSalaryIsInteger()
        {
            EmployeeApp employeeApp = new EmployeeApp("4,,\n3,4,\n2,3,200\n1,2,700");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void testEveryManagerisAnEmployee()
        {
            EmployeeApp employeeApp = new EmployeeApp("4,,1000\n3,5,800\n2,4,200\n1,2,700");
            
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void testCeoCountCheck()
        {
            EmployeeApp employeeApp = new EmployeeApp("4,,1000\n3,,800\n2,,200\n1,,700");
   
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void testNoEmployeeWithMoreThanOneManager()
        {
            EmployeeApp employeeApp = new EmployeeApp("4,,1000\n3,4,800\n2,3,200\n1,2,700\n1,3,700");
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void testbudgetSalarymanagerEmployeeMapMissingKey()
        {
            EmployeeApp employeeApp = new EmployeeApp(",,1000\n3,4,800\n2,3,200\n1,2,700\n1,3,700");
            Assert.AreEqual(employeeApp.budgetSalary("4"), 0);
        }

        [TestMethod]
        public void testBudgetSalary()
        {
            EmployeeApp employeeApp = new EmployeeApp("4,,5000\n3,4,300\n2,3,200\n1,2,100");
            Console.WriteLine(employeeApp.budgetSalary("4"));
            Assert.AreEqual(employeeApp.budgetSalary("4"), 5600);
        }



    }
}
