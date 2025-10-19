using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimManagement
{
    public class Claim
    {
        public int ClaimID { get; set; }
        public int ContractorID { get; set; } 
        public int ContractID { get; set; } 
        public DateTime ClaimMonth { get; set; }
        public decimal HoursWorked { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string Status { get; set; } // e.g., "Pending", "Approved", "Rejected"
        public decimal CalculatedAmount { get; set; }
        public string Comments { get; set; }
        public int? ApprovedByUserID { get; set; } 
    }
}

//i got this code structure from stack overflow
//https://stackoverflow.com/questions/5096926/what-is-the-get-set-syntax-in-c
//one noa
//https://stackoverflow.com/users/3082718/one-noa
