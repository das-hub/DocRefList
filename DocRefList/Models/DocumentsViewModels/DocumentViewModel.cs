using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DocRefList.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace DocRefList.Models.DocumentsViewModels
{
    public class DocumentViewModel
    {
        [DisplayName("Номер")]
        public string Number { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Дата")]
        public DateTime? Date { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Дата ознакомления обязательна для заполнения")]
        [DisplayName("Ознакомиться до")]
        [NotLessCurrentDate]
        public DateTime SeeBefore { get; set; } = DateTime.Today.AddDays(2);

        [Required(ErrorMessage = "Название документа обязательно для заполнения")]
        [DisplayName("Название")]
        public string Caption { get; set; }
        
        [DisplayName("Примечание")]
        public string Note { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
