using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sqo;
using Sqo.Attributes;
using System.ComponentModel.DataAnnotations;
using BrokerTools.Data;

namespace BrokerTools.Data.Model
{
    public class Advisor : DbDomainObject
    {
        public int OID { get; set; }

        [Required(ErrorMessage="Advisor Name is required")]
        public string Name { get; set; }
    }
}