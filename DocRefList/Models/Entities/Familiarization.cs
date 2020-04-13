using System;
using DocRefList.Models.Abstraction;

namespace DocRefList.Models.Entities
{
    public class Familiarization : IEntity
    {
        public int Id { get; set; }
        public DocumentInfo Document { get; set; }
        public Employee Employee { get; set; }
        public DateTime DateTime { get; set; }
    }
}
