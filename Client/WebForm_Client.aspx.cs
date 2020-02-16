using Client.ServiceDatabase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestTaskKost_WCF;


namespace Client
{
    public partial class WebForm_Client : System.Web.UI.Page
    {
        private enum TypeFild { OnlyLatter, LatterDash, LatterDashSpace, Numbers, LatterDashSpaceNumbers }


        protected void btnLoad_Click(object sender, EventArgs e)
        {
            lblResualt.Text = "Поиск запущен...";

            DataTable resultSelect = LoadDataEmployeeFromDatabase();
            lblResualt.Text = $"Найдено записей: {resultSelect.Rows.Count}";
            tableEmployee.DataSource = resultSelect;
            tableEmployee.DataBind();
        }

        /// <summary>
        /// Загрузить данные сотрудников из базы данных
        /// </summary>
        /// <returns></returns>
        private DataTable LoadDataEmployeeFromDatabase()
        {
            var client = new ServiceDatabase.DataBase_KostaClient("BasicHttpBinding_IDataBase_Kosta");

            DataTable table = new DataTable();

            string searchFirstName = FormatedString(tbxFirstName.Text);
            string searchLastName = FormatedString(tbxLastName.Text);
            string searchPatromumic = FormatedString(tbxPatronumic.Text);

            var employees = client.GetEmployees(searchFirstName, searchLastName, searchPatromumic);
            table.Columns.Add("Имя");
            table.Columns.Add("Фамилия");
            table.Columns.Add("Отчество");
            table.Columns.Add("День рождение");
            table.Columns.Add("Возраст");

            for (int i = 0; i < employees.Length; i++)
            {
                Employee employee = employees[i] as Employee;
                if (employee != null)
                {
                    if (employee.BirchDay != null)
                    {
                        table.Rows.Add(employee.FirstName, employee.LastName, employee.Patronumic,
                            employee.BirchDay.ToString("dd:MM:yyyy"), $"{ YearsEmloyee(employee.BirchDay)} лет");
                    }
                    else
                    {
                        table.Rows.Add(employee.FirstName, employee.LastName, employee.Patronumic);
                    }
                }
            }
            client.Close();

            return table;
        }

        /// <summary>
        /// Расчтё возвраста сотрудника по дате рождения
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        private int YearsEmloyee(DateTime dateOfBirth)
        {
            DateTime crntDateTime = DateTime.Now;
            int years = crntDateTime.Year - dateOfBirth.Year;

            DateTime crntDate = new DateTime(1, crntDateTime.Month, crntDateTime.Day);
            DateTime birchDate = new DateTime(1, dateOfBirth.Month, dateOfBirth.Day);

            TimeSpan timeSpan = crntDate - birchDate;
            if (timeSpan.Days < 0)
                years--;

            return years;
        }

        protected void btnInsertEmploye_Click(object sender, EventArgs e)
        {
            string firstNameEmployee = tbxSetFirstName.Text;
            if (!CheckText(firstNameEmployee, TypeFild.OnlyLatter, false))
            {
                lblStatusInsert.Text = $"В поле {lblSetFirstNameText.Text} присутсвуют посторонние символы. Данные не отправлены";
                return;
            }
            firstNameEmployee=FormatedString(firstNameEmployee);

            string lastNameEmployee = tbxSetLastName.Text;
            if (!CheckText(lastNameEmployee, TypeFild.LatterDash, false))
            {
                lblStatusInsert.Text = $"В поле {lblSetLastNameText.Text} присутсвуют посторонние символы. Данные не отправлены";
                return;
            }
            lastNameEmployee = FormatedString(lastNameEmployee);


            string patronumicEmployee = tbxSetPatronumic.Text;
            if (!CheckText(patronumicEmployee, TypeFild.OnlyLatter, true))
            {
                lblStatusInsert.Text = $"В поле {lblSetPatronumic.Text} присутсвуют посторонние символы. Данные не отправлены";
                return;
            }
            patronumicEmployee = FormatedString(patronumicEmployee);

            //Нужна проверка на случай, когда дата не выбрана (по умолчанию идёт 0001 год, который mssql не воспринимает)
            DateTime birchDay = CalendarBirchDay.SelectedDate;
            if(birchDay > DateTime.Now)
            {
                lblStatusInsert.Text = $"Дата дня рождения не может быть больше текущей даты. Данные не отправлены";
                return;
            }

            var client = new ServiceDatabase.DataBase_KostaClient("BasicHttpBinding_IDataBase_Kosta");

            string resultInsert = client.SetEmployees(firstNameEmployee, lastNameEmployee, patronumicEmployee, birchDay);
            lblStatusInsert.Text = resultInsert;
            client.Close();
            btnLoad_Click(this, new EventArgs());
        }

        /// <summary>
        /// Приведение строки к общему формату (первый символ заглавный)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string FormatedString(string str)
        {
            string[] woords = str.Split('-');

            for (int i = 0; i < woords.Length; i++)
            {
                if (woords[i].Length > 1)
                    woords[i] = woords[i].Substring(0, 1).ToUpper() + woords[i].Substring(1, woords[i].Length - 1).ToLower();
                else
                    woords[i] = woords[i].ToUpper();
            }
            return string.Join("-", woords);
        }


        /// <summary>
        /// Проверка текста на соответсвие разрешённым символам
        /// </summary>
        /// <param name="text"></param>
        /// <returns>true - текст соотвествуют требуемым символам</returns>
        private bool CheckText(string text, TypeFild typeFild, bool statusEmpty)
        {
            Regex regex;
            switch (typeFild)
            {
                case TypeFild.LatterDash:
                    {
                        regex = new Regex(@"^[a-zA-Zа-яА-Я-]*$");
                        break;
                    }
                case TypeFild.LatterDashSpace:
                    {
                        regex = new Regex(@"^[a-zA-Zа-яА-Я- ]*$");
                        break;
                    }
                case TypeFild.OnlyLatter:
                    {
                        regex = new Regex(@"^[a-zA-Zа-яА-Я]*$");
                        break;
                    }
                case TypeFild.Numbers:
                    {
                        regex = new Regex(@"^[0-9]*$");
                        break;
                    }
                case TypeFild.LatterDashSpaceNumbers:
                    {
                        regex = new Regex(@"^[a-zA-Zа-яА-Я0-9- ]*$");
                        break;
                    }
                default:
                    {
                        regex = new Regex("*");
                        break;
                    }
            }

            if (text.Length == 0)
            {
                if (statusEmpty)
                    return true;
                else
                    return false;
            }


            if (regex.IsMatch(text))
                return true;
            else
                return false;
        }
    }
}