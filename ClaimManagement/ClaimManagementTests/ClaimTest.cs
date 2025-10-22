using Xunit;
using ClaimManagement;
using System;

namespace ClaimManagementTests
{
    public class ClaimTest
    {
        [Fact]
        public void CreateClaim_ShouldStoreCorrectValues()
        {
            var claim = new Claim
            {
                Lecturer = "John Doe",
                HoursWorked = 10,
                HourlyRate = 200,
                Comments = "Extra hours",
                Status = "Pending"
            };

            claim.CalculateTotal();

            Assert.Equal(2000, claim.CalculatedAmount);
            Assert.Equal("Pending", claim.Status);
        }

        [Fact]
        public void ApproveClaim_ShouldChangeStatusToApproved()
        {
            var claim = new Claim { Status = "Pending" };
            claim.Status = "Approved";
            Assert.Equal("Approved", claim.Status);
        }

        [Fact]
        public void RejectClaim_ShouldChangeStatusToRejected()
        {
            var claim = new Claim { Status = "Pending" };
            claim.Status = "Rejected";
            Assert.Equal("Rejected", claim.Status);
        }

        [Fact]
        public void Claim_WithNegativeHours_ShouldThrowError()
        {
            var claim = new Claim
            {
                Lecturer = "Jane Doe",
                HoursWorked = -5,
                HourlyRate = 100
            };

            Assert.Throws<ArgumentException>(() => claim.CalculateTotal());
        }

        [Fact]
        public void Claim_WithZeroHourlyRate_ShouldThrowError()
        {
            var claim = new Claim
            {
                Lecturer = "Jane Doe",
                HoursWorked = 8,
                HourlyRate = 0
            };

            Assert.Throws<ArgumentException>(() => claim.CalculateTotal());
        }

        [Fact]
        public void Claim_WithValidData_ShouldCalculateAmountCorrectly()
        {
            var claim = new Claim
            {
                Lecturer = "Sam Zwane",
                HoursWorked = 15,
                HourlyRate = 250
            };

            claim.CalculateTotal();
            Assert.Equal(3750, claim.CalculatedAmount);
        }
    }
}

// i got the code stucture from c# corner
//https://www.c-sharpcorner.com/article/getting-started-with-unit-testing-using-c-sharp-and-xunit/
//mukesh kumar
//https://www.c-sharpcorner.com/article/getting-started-with-unit-testing-using-c-sharp-and-xunit/

// i also consulted ai for implementation ideas and packages i should install(nuget packages)
// open ai
// https://openai.com