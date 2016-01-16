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
            context.SetValue(Valid, false);

            var loan = context.GetValue(this.Loan);

            if(loan.CreditRating > 0)
            {
                context.SetValue(Valid, true);
            }

            if(loan.DownPaymentAmount > 0)
            {
                context.SetValue(Valid, true);
            }

            if(loan.LoanAmount > 0)
            {
                context.SetValue(Valid, true);
            }

        }
    }
}
