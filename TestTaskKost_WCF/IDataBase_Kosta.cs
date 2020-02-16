using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace TestTaskKost_WCF
{
    [ServiceContract]
    public interface IDataBase_Kosta
    {
        [OperationContract]
        List<Employee> GetEmployees(string firstNameSearch, string lastNameSearch, string patronymucSearch);

        [OperationContract]
        string SetEmployees(string firstNameSearch, string lastNameSearch, string patronymuc, DateTime biarchDay);
    }

    [DataContract]
    public class Employee
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Patronumic { get; set; }
        [DataMember]
        public DateTime BirchDay { get; set; }
    }

}
