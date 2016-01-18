using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowWebApiExample.Models;

namespace WorkflowWebApiExample.Tests.IntegrationTests
{
    [TestClass]
    public class When_checking_loan_eligibility
    {
        private Loan _loan;
        private bool _result;

        [TestInitialize]
        public void TestInit()
        {
            _result = new bool();

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
            var checkLoanEligibilityWorkflow = new WorkflowWebApiExample.Workflows.CheckLoanEligibility();
            var workflowInvoker = new WorkflowInvoker(checkLoanEligibilityWorkflow);
            var InputArguments = new Dictionary<string, object>();
            InputArguments.Add("loan", _loan);
            var resultDictionary = workflowInvoker.Invoke(InputArguments);
            _result = (bool)resultDictionary["approved"];
        }

        [TestMethod]
        public void Should_return_false_when_credit_rating_is_less_then_650()
        {
            _loan.CreditRating = 600;
            Because();
            Assert.IsFalse(_result, "Expected false when credit rating is below 650.");
        }

        [TestMethod]
        public void Should_return_true_when_credit_rating_is_650_or_greater()
        {
            _loan.CreditRating = 650;
            Because();
            Assert.IsTrue(_result, "Expected true when credit rating is 650 or above.");
        }

        [TestMethod]
        public void Should_return_false_when_hascollateral_is_false()
        {
            _loan.HasCollateral = false;
            Because();
            Assert.IsFalse(_result, "Expected false when HasCollateral is false.");
        }

        [TestMethod]
        public void Should_return_true_when_hascollateral_is_true()
        {
            _loan.HasCollateral = true;
            Because();
            Assert.IsTrue(_result, "Expected true when HasCollateral is true.");
        }

        [TestMethod]
        public void Should_return_false_when_down_payment_is_less_than_20_percent()
        {
            _loan.DownPaymentAmount = 1;
            Because();
            Assert.IsFalse(_result, "Expected false when down payment is less than 20%.");
        }

        [TestMethod]
        public void Should_return_true_when_down_payment_is_greater_than_20_percent()
        {
            _loan.DownPaymentAmount = 20;
            Because();
            Assert.IsTrue(_result, "Expected true when down payment is greater than 20%.");
        }
    }
}
