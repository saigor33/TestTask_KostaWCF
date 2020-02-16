using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using TestTaskKost_WCF.TestDB_WCFTableAdapters;

namespace TestTaskKost_WCF
{
    public class DataBase_Kosta : IDataBase_Kosta
    {

        /// <summary>
        /// Получить список сотрудников по условию совпадения Имени, Фамили и отчества по начальным символам
        /// </summary>
        /// <param name="firstNameSearch"></param>
        /// <param name="lastNameSearch"></param>
        /// <param name="patronymucSearch"></param>
        /// <returns></returns>
        public List<Employee> GetEmployees(string firstNameSearch, string lastNameSearch, string patronymucSearch)
        {
            DataTable table = DatabaseQueries.GetRowsEmployee();
            List<Employee> rows = new List<Employee>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                Employee employee = new Employee
                {
                    ID = (int)table.Rows[i]["ID"],
                    FirstName = table.Rows[i]["FirstName"].ToString(),
                    LastName = table.Rows[i]["LastName"].ToString(),
                    Patronumic = table.Rows[i]["Patronumic"].ToString(),
                    BirchDay = (DateTime)table.Rows[i]["BirchDay"]
                };
                bool statusAdd = true;

                if (employee.FirstName != firstNameSearch && firstNameSearch != "")
                    statusAdd = false;

                if (employee.LastName != lastNameSearch && lastNameSearch != "")
                    statusAdd = false;

                if (!CheckConditionPatronumic(patronymucSearch, employee.Patronumic))
                    statusAdd = false;

                if(statusAdd)
                rows.Add(employee);
            }
            return rows; 
        }

        /// <summary>
        /// Метод сравнения текста по налачу (слово начинается с введённых символов)
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="strValue"></param>
        /// <returns>Результат сравнение (true - слово начинается с введённого выражения)</returns>
        private bool CheckConditionPatronumic(string Condition, string strValue)
        {
            Regex regexPatronumic = new Regex($@"^{Condition}\w*");

            if (regexPatronumic.IsMatch(strValue))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Добавить сотрудника в базу данных
        /// </summary>
        /// <param name="firstNameSearch"></param>
        /// <param name="lastNameSearch"></param>
        /// <param name="patronymuc"></param>
        /// <param name="biarchDay"></param>
        /// <returns>Сообщение-результат добавления</returns>
        public string SetEmployees(string firstNameSearch, string lastNameSearch, string patronymuc, DateTime biarchDay)
        {
            string textMessage = DatabaseQueries.AddEmployeeInDatebase(firstNameSearch, lastNameSearch, patronymuc, biarchDay);
            return textMessage;
        }
    }
}
