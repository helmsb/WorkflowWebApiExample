using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkflowWebApiExample.Models
{
    public class Loan
    {
        public decimal LoanAmount;

        public decimal DownPaymentAmount;

        public bool HasCollateral;

        public int CreditRating;
    }
}