using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WorkflowWebApiExample.Models;

namespace WorkflowWebApiExample.Controllers
{
    public class LoanController : ApiController
    {
       
        //Run workflow logic to see if the loan application is valid.
        public ApprovalResponse CheckLoanEligibility(Loan loan)
        {
            var checkLoanEligibility = new Workflows.CheckLoanEligibility();
            var workflow = new WorkflowInvoker(checkLoanEligibility);
            var InputArguments = new Dictionary<string, object>();
            InputArguments.Add("loan", loan);
            var resultDictionary = workflow.Invoke(InputArguments);
            var result = (ApprovalResponse)resultDictionary["ApprovalResponse"];

            return result;
        }


    }
}
