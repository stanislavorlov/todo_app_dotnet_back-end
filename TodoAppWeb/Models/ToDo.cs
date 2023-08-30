namespace TodoAppWeb.Models
{
    public class ToDo
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        // Add IsComplete property of boolean type
        public bool IsComplete { get; set; }
    }
}
