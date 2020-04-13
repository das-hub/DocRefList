using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DocRefList.Models.Entities
{
    public class Employee : IdentityUser
    {
        public string FullName { get; set; }

        public ICollection<Familiarization> Familiarizations { get; set; }

        public Employee() : base()
        {
            Familiarizations = new List<Familiarization>();
        }

        public override string ToString()
        {
            return !string.IsNullOrEmpty(FullName) ? FullName : base.ToString();
        }
    }
}
