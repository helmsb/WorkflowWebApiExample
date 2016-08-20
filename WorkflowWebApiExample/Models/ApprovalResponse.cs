using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkflowWebApiExample.Models
{
    public class ApprovalResponse
    {
        public bool Approved { get; set; }
        public decimal MonthlyPayment { get; set; }
        public decimal Rate { get; set; }
    }
}