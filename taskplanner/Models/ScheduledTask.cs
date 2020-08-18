using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskplanner.Models
{
    public class ScheduledTask
    {
        public int Id { get; set; }

        public string IdUser { get; set; }

        public bool Done { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime Deadline { get; set; }

        public int? ParentId { get; set; }
    }
}
