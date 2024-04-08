namespace TreeApp.DTO;

public class EditNodeDto
{
    public string? NewName { get; set; }
    public Guid? ParentNodeId { get; set; }
    public Guid Id { get; set; }
}