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
        public string Lecturer { get; set; } 
        public int ContractorID { get; set; }
        public int ContractID { get; set; }
        public DateTime ClaimMonth { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; } 
        public DateTime SubmissionDate { get; set; }
        public string Status { get; set; } = "Pending"; // default
        public decimal CalculatedAmount { get; set; }
        public string Comments { get; set; }
        public int? ApprovedByUserID { get; set; }

        
        public void CalculateTotal()
        {
            if (HoursWorked < 0)
                throw new ArgumentException("Hours worked cannot be negative.");
            if (HourlyRate <= 0)
                throw new ArgumentException("Hourly rate must be greater than zero.");

            CalculatedAmount = HoursWorked * HourlyRate;
        }
    }
}


//i got this code structure from stack overflow
//https://stackoverflow.com/questions/5096926/what-is-the-get-set-syntax-in-c
//one noa
//https://stackoverflow.com/users/3082718/one-noa
