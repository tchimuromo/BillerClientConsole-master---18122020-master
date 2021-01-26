using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillerClientConsole.Dtos
{
    public class RegisteredOfficeDto
    {
        public string AppicationId { get; set; }
        public string OfficeId { get; set; }
        public string PhysicalAddress { get; set; }
        public string PostalAddress { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool Query { get; set; }
        public int HasQuery { get; set; }
        public string Comment { get; set; }
    }
}
