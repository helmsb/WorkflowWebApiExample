using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowWebApiExample.Models;

namespace WorkflowWebApiExample.Tests.UnitTests
{
    [TestClass]
    public class When_Validating_Loan_Is_Complete
    {
        private Loan _loan;
        private bool? _result;

        [TestInitialize]
        public void TestInit()
        {
            _result = new bool?();

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
            var validateLoanIsCompleteActivity = new WorkflowWebApiExample.CodeActivities.ValidateLoanIsComplete();
            var workflowInvoker = new WorkflowInvoker(validateLoanIsCompleteActivity);
            var InputArguments = new Dictionary<string, object>();
            InputArguments.Add("Loan", _loan);
            var resultDictionary = workflowInvoker.Invoke(InputArguments);
            _result = (bool)resultDictionary["Valid"];
        }

        [TestMethod]
        public void Should_return_false_for_valid_if_loan_amount_is_0()
        {
            _loan.LoanAmount = 0;

            Because();

            Assert.IsFalse(_result.Value, "Expected to receive false when the amount was 0 but _result was true.");
        }

        [TestMethod]
        public void Should_return_true_for_valid_if_loan_amount_is_greater_than_0()
        {
            _loan.LoanAmount = 100;

            Because();

            Assert.IsTrue(_result.Value, "Expected to receive true when the amount was greater than 0 but _result was false.");
        }

        [TestMethod]
        public void Should_return_false_if_credit_rating_is_0()
        {
            _loan.CreditRating = 0;

            Because();

            Assert.IsFalse(_result.Value, "Expected to receive false when the credit rating was 0 but _result was true.");
        }

        [TestMethod]
        public void Should_return_true_if_credit_rating_is_greater_than_0()
        {
            _loan.CreditRating = 700;

            Because();

            Assert.IsTrue(_result.Value, "Expected to receive true when the credit rating was greater than 0 but _result was true.");
        }

        [TestMethod]
        public void Should_return_false_if_down_payment_is_0()
        {
            _loan.DownPaymentAmount = 0;

            Because();

            Assert.IsFalse(_result.Value, "Expected to receive false when the down payment was 0 but _result was true.");
        }

        [TestMethod]
        public void Should_return_true_if_down_payment_is_greater_than_0()
        {
            _loan.DownPaymentAmount = 20;

            Because();

            Assert.IsTrue(_result.Value, "Expected to receive true when the down payment was 0 but _result was false.");
        }
    }
}
