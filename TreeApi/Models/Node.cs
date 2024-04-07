using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreeApp.Models;

public class Node
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    [ForeignKey("ParentNode")]
    public Guid? ParentNodeId { get; set; }
    public Node ParentNode { get; set; }
    public List<Node> Childrens { get; set; } = [];
}