using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Graphql.Common.Model
{
    public class FilterDonation
    {
        public string category {get;set;}
        public DateTime? from {get;set;}
        public DateTime? to {get;set;}
        public bool? isCancelled { get; set; }
    }
}
