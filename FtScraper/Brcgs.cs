using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brcgs
{
    public class BrcgsList
    {
        public int totalPages { get; set; }
        public Resultslist[] resultsList { get; set; }
    }

    public class Resultslist
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime expirationDate { get; set; }
        public string grade { get; set; }
        public Location location { get; set; }
        public object contacts { get; set; }
        public Auditdetails auditDetails { get; set; }
        public int auditId { get; set; }
    }

    public class Location
    {
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string addressLine3 { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string zipCode { get; set; }
        public string country { get; set; }
    }

    public class Auditdetails
    {
        public string issueDate { get; set; }
        public string expiryDate { get; set; }
        public string standard { get; set; }
    }



    public class BrcgsCompany
    {
        public string name { get; set; }
        public int id { get; set; }
        public Location location { get; set; }
        public Contact[] contacts { get; set; }
        public Auditdetail[] auditDetails { get; set; }
    }

    public class Contact
    {
        public string phoneNumber { get; set; }
        public string faxNumber { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string contactType { get; set; }
    }

    public class Auditdetail
    {
        public string standard { get; set; }
        public string[] categories { get; set; }
        public string scope { get; set; }
        public string certificationBody { get; set; }
        public string exclusion { get; set; }
        public string issueDate { get; set; }
        public string expiryDate { get; set; }
        public string grade { get; set; }
        public object[] additionalModules { get; set; }
    }

}
