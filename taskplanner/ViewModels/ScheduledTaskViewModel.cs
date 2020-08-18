using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskplanner.Models;

namespace taskplanner.ViewModels
{
    public class ScheduledTaskViewModel : IDisposable
    {
        public int Id { get; set; }

        public bool Done { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime Deadline { get; set; }

        public int? ParentId { get; set; }

        public List<ScheduledTaskViewModel> ChildTask { get; set; }

        public ScheduledTaskViewModel(ScheduledTask obj)
        {
            Id = obj.Id;
            Done = obj.Done;
            Name = obj.Name;
            Text = obj.Text;
            Deadline = obj.Deadline;
            ParentId = obj.ParentId;
        }

        public void Dispose()
        {
            
        }
    }
    
}
