using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkflowWebApiExample.Models;

namespace WorkflowWebApiExample.Tests.UnitTests
{
    [TestClass]
    public class When_calling_checkloaneligibility
    {
        private Loan _loan;
        private ApprovalResponse _result;

        [TestInitialize]
        public void TestInit()
        {
            _result = new ApprovalResponse();

            _loan = new Loan
            {
                LoanAmount = 100,
                DownPaymentAmount = 20,
                CreditRating = 700,
                HasCollateral = true
            };
        }

        private void Because()
        {
            var controller = new WorkflowWebApiExample.Controllers.LoanController();
           _result = controller.CheckLoanEligibility(_loan);
        }



        [TestMethod]
        public void Should_invoke_checkloaneligibiltiy_workflow()
        {
            Because();
            Assert.IsTrue(_result.Approved, "CheckLoanEligibility Workflow was not invoked");
        }
    }
}
