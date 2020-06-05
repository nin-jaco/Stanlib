using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sqo;
using Sqo.Attributes;
using System.ComponentModel.DataAnnotations;
using BrokerTools.Data;

namespace BrokerTools.Model
{
    public class FundConcept : DbDomainObject
    {
       
        public int OID { get; set; }

        private string ProductCode;

        /// <summary>
        /// Gets or sets the Product Code
        /// </summary>
        /// 
        [Required(ErrorMessage="{0} is required")]
        [UseVariable("ProductCode")]
        public string productcode
        {
            get
            {
                return this.ProductCode;
            }

            set
            {
                this.ProductCode = value;
                    NotifyPropertyChanged(() => productcode);
            }
        }

        private string FullName;

        /// <summary>
        /// Gets or sets the Product Code
        /// </summary>

        [Required(ErrorMessage = "{0} is required")]
        [UseVariable("FullName")]
        public string fullname
        {
            get
            {
                return this.FullName;
            }

            set
            {
                    this.FullName = value;
                    NotifyPropertyChanged(() => fullname);
            }
        }

        public object GetValue(System.Reflection.FieldInfo field)
        {
            return field.GetValue(this);
        }

        public void SetValue(System.Reflection.FieldInfo field, object value)
        {
            field.SetValue(this, value);
        }
    }
}