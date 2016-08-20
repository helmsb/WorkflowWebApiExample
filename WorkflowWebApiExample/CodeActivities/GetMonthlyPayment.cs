using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using WorkflowWebApiExample.Models;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WorkflowWebApiExample.CodeActivities
{

    public sealed class GetMonthlyPayment : CodeActivity
    {
        // Define an activity input argument of type string
        [RequiredArgument]
        public InArgument<Loan> Loan { get; set; }
        [RequiredArgument]
        public InArgument<bool> Approval { get; set; }
        [RequiredArgument]
        public OutArgument<ApprovalResponse> ApprovalResponse { get; set;}

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            Loan loan = context.GetValue(this.Loan);
            bool approval = context.GetValue(this.Approval);
            var approvalResponse = new ApprovalResponse();

            approvalResponse.Approved = approval;

            var url = string.Format("http://www.zillow.com/webservice/GetMonthlyPayments.htm?zws-id=X1-ZWz19lyzenyz2j_6r0dc&output=json&price={0}&dollarsdown={1}", loan.LoanAmount,loan.DownPaymentAmount);
            var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
            using (var response = webrequest.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                JObject o = JObject.Parse(result);

                 approvalResponse.Rate = o["response"]["thirtyYearFixed"]["rate"].Value<decimal>();
                 approvalResponse.MonthlyPayment = o["response"]["thirtyYearFixed"]["monthlyPrincipalAndInterest"].Value<decimal>();

                
         
            }

            context.SetValue(ApprovalResponse, approvalResponse);
        }
    }
    }

