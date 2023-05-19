using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;
using static To_Do_App.Program;

namespace To_Do_App
{
    internal class Task
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public PriorityLevel Priority { get; set; }

        public Task(string title, string description, DateTime dueDate, PriorityLevel priority)
        {

            Title = title;
            Description = description;
            DueDate = dueDate;
            Priority = priority;
            this.Id = TaskCounter.GetNextID();
            this.ID = Guid.NewGuid();
        }
    }
}
