public class Todo
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsComplete { get; set; }
}