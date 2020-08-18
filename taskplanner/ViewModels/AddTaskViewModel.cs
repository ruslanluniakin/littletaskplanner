using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace taskplanner.ViewModels
{
    public class AddTaskViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Deadline { get; set; }

        public int? ParentId { get; set; }
    }
}
