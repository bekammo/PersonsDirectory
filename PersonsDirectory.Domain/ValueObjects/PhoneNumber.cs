using PersonsDirectory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDirectory.Domain.ValueObjects
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public PhoneNumberType Type { get; set; } 
        public string Number { get; set; }
        public int PersonId { get; set; }
    }
}
