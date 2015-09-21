using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorocaba.Commons.Entity.ValidationAttributes {
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotBlank : ValidationAttribute {

        public NotBlank() : base(Strings.FieldCannotBeNull) {
        }

        public override bool IsValid(object value) {
            string stringValue = value as string;
            return (stringValue == null || stringValue.Trim().Length != 0);
        }
    }
}
