using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskKost_WCF.TestDB_WCFTableAdapters;

namespace TestTaskKost_WCF
{
    public class DatabaseQueries
    {
        private static TestDB_WCF _dB_Kosta;
        private static EmployeeTableAdapter _empoyeeTableAdapter = new EmployeeTableAdapter();

        public static TestDB_WCF TestDB_WCF
        {
            get
            {
                if(_dB_Kosta is null)
                {
                    _dB_Kosta = new TestDB_WCF();
                }
                return _dB_Kosta;
            }
            set
            {
                _dB_Kosta = value;
                UpdateTableEmpoyee();
            }
        }

        /// <summary>
        /// Получить таблицу сотрудников
        /// </summary>
        /// <returns></returns>
        public static DataTable GetRowsEmployee()
        {
            return _empoyeeTableAdapter.GetData();
        }

        /// <summary>
        /// Обновить данные таблицы "Сотрудники"
        /// </summary>
        public static void UpdateTableEmpoyee()
        {
            _empoyeeTableAdapter.Fill(TestDB_WCF.Employee);
        }


        /// <summary>
        /// Добавить нового сотрудника в базу данных.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="surName"></param>
        /// <param name="patronymic"></param>
        /// <param name="dateOfBirch"></param>
        /// <param name="docSeries"></param>
        /// <param name="docNumber"></param>
        /// <param name="position"></param>
        /// <param name="departamentID"></param>
        public static string AddEmployeeInDatebase(string firstNameSearch, string lastNameSearch, string patronymuc, DateTime biarchDay)
        {
            try
            {
                _empoyeeTableAdapter.Insert(
                    firstNameSearch,
                    lastNameSearch,
                    patronymuc,
                    biarchDay
                    );

                UpdateTableEmpoyee();
                return "Данные успешно добавлены!";
            }
            catch (Exception ex)
            {
                return "В процессе добавления данных произошла ошибка! Введены некорректные данные или сервер не доступен.";
            }
        }


    }
}
