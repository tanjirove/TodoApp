using System;

namespace Todo.Domain.Entities
{
    public class TodoItem
    {
        public TodoItem()
        {
            Id =  Guid.NewGuid();
            Done = false;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public bool Done { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
