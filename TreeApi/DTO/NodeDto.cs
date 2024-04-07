using TreeApp.Models;

namespace TreeApp.DTO;

public class NodeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? ParentNodeId { get; set; }
    public List<NodeDto> Childrens { get; set; }
}