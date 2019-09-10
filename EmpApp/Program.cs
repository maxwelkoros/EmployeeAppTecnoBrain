using System;
using System.Collections.Generic;
using System.Linq;


namespace EmpApp
{

    public class EmployeeApp
    {
        private string data;
        private int employeeSalary;
        private Dictionary<string, string> employeeManagerRelationshipMap = new Dictionary<string, string>();
        private Dictionary<string, string> employeeSalaryMap = new Dictionary<string, string>();
        private Dictionary<string, List<string>> managerEmployeeMap = new Dictionary<string, List<string>>();
        public EmployeeApp(string data)
        {
            this.data = data;
            /*A list of a employees*/
            List<string> employeeList = new List<string>();
            /*A list of managers*/
            List<string> managersList = new List<string>();

            string []rows = data.Split('\n');
                foreach (string row in rows)
                {
                string[] column = row.Split(',');
                List<string> column_str = column.ToList();

                /*Salary amount is integer*/
                if (column[2] != null || column[2] != "")
                {
                    if (!int.TryParse(column[2], out employeeSalary))
                    {
                        throw new ValidationException("Salary value is not an integer");
                    }
                }
                employeeList.Add(column[0]);

                managersList.Add(column[1]);

                employeeManagerRelationshipMap.Add(column[0], column[1]);
                employeeSalaryMap.Add(column[0], column[2]);

            }

            /*Every manager is an employee validation check*/
            foreach (String manager in managersList)
            {
                if (!String.IsNullOrWhiteSpace(manager))
                {
                    if (!employeeList.Contains(manager)){
                        throw new ValidationException("manager is not an employee");
                    }
                }
            }

            /*ceo count check*/
            int ceoCount = 0;
            foreach (String manager in managersList)
           {
                
               if (String.IsNullOrWhiteSpace(manager))
               {
                    ceoCount += 1;
               }
           }
           if (ceoCount > 1)
            {
                throw new ValidationException("More than one CEO");
            }
            /*no employee reports to more than one manager*/
            HashSet<String> employeeSet = new HashSet<String>(employeeList);
            if(employeeSet.Count != employeeList.Count)
            {
                throw new ValidationException("more than one employee reports to more than one manager");
            }

            HashSet<String> managerSet = new HashSet<String>(managersList);

            managerEmployeeMap = new Dictionary<string, List<string>>();
            foreach (string manager in managerSet)
            {
                foreach(string employee in employeeSet)
                {
                    if (employeeManagerRelationshipMap.ContainsKey(employee)){
                        if(employeeManagerRelationshipMap[employee] == manager)
                        {
                            if(managerEmployeeMap.ContainsKey(manager))
                                {
                                managerEmployeeMap[manager].Add(employee);
                            }
                            else
                            {
                                managerEmployeeMap.Add(manager, new List<string> {employee});
                            }

                            
                        }
                    }
             
                }
            }
            if(managerEmployeeMap.ContainsKey(""))
            {
                managerEmployeeMap.Add("CEO", managerEmployeeMap[""]);
                managerEmployeeMap.Remove("");
            }
        }

        /*instance method to return salary budget*/
       public long budgetSalary(string manager)
        {
          long salarySum = 0;
        
            if (managerEmployeeMap.ContainsKey("CEO"))
            {
                if (manager == managerEmployeeMap["CEO"][0])
                {

                    foreach (string salary in employeeSalaryMap.Values)
                    {
                        long salaryValue = Convert.ToInt64(salary);
                        salarySum += salaryValue;
                    }
                }
                else
                {
                    if (managerEmployeeMap.ContainsKey(manager))
                    {
                        foreach (string employee in managerEmployeeMap[manager])
                        {
                            long salaryValue = Convert.ToInt64(employeeSalaryMap[employee]);
                            salarySum += salaryValue;

                        }
                        salarySum += Convert.ToInt64(employeeSalaryMap[manager]);
                    }
                    else
                    {
                        throw new ValidationException("key is missing in dictionary");
                    }
                }
            }
            else
            {
                throw new ValidationException("key is missing in dictionary");
            }
            return salarySum;
        }
       
        static void Main(string[] args)
        {

            EmployeeApp employeeApp = new EmployeeApp("4,,5000\n3,4,300\n2,3,200\n1,2,100");
            Console.WriteLine(employeeApp.budgetSalary("3"));

        }

    }
}
