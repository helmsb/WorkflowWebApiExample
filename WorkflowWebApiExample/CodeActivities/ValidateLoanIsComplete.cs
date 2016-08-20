using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using WorkflowWebApiExample.Models;

namespace WorkflowWebApiExample.CodeActivities
{

    public sealed class ValidateLoanIsComplete : CodeActivity
    { 
        [RequiredArgument]
        public InArgument<Loan> Loan { get; set; }

        [RequiredArgument]
        public OutArgument<bool> Valid { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var isValid = new bool?();

            var loan = context.GetValue(this.Loan);

            if (!(loan.CreditRating > 0))
            {
                isValid = false;
            }

            if (!(loan.DownPaymentAmount > 0))
            {
                isValid = false;
            }

            if (!(loan.LoanAmount > 0))
            {
                isValid = false;
            }

            //If we didn't get set to false then we're valid.
            if(!isValid.HasValue)
            {
                isValid = true;
            }

            context.SetValue(Valid, isValid);

        }
    }
}