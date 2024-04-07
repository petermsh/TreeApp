namespace TreeApp.DTO;

public class AddNodeDto
{
    public string Name { get; set; }
    public Guid ParentNodeId { get; set; }
}