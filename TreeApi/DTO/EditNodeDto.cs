namespace TreeApp.DTO;

public class EditNodeDto
{
    public string? Name { get; set; }
    public Guid? ParentNodeId { get; set; }
    public Guid Id { get; set; }
}