using Microsoft.EntityFrameworkCore;
using TreeApp.DAL;
using TreeApp.DTO;
using TreeApp.Interfaces;
using TreeApp.Models;

namespace TreeApp.Services;

public class NodeService(TreeDbContext dbContext) : INodeService
{
    public async Task<List<NodeDto>> GetNodes()
    {
        var nodes = await dbContext.Nodes
            .Include(n=>n.Childrens)
            .ToListAsync();

        var rootNodes = nodes.Where(node => node.ParentNodeId == null).ToList();

        var rootNodesDto = rootNodes.Select(rootNode => MapNodeToDto(rootNode)).ToList();

        return rootNodesDto;
    }

    public async Task AddNode(AddNodeDto addNodeDto)
    {
        var parentNode = await GetParentNode(addNodeDto.ParentNodeId);

        if (parentNode is null)
            throw new Exception("node is null");
        
        var newNode = new Node
        {
            Name = addNodeDto.Name,
            ParentNodeId = parentNode.Id
        };

        dbContext.Nodes.Add(newNode);
        parentNode.Childrens.Add(newNode);
        await dbContext.SaveChangesAsync();
    }
    
    public async Task EditNode(EditNodeDto editNodeDto)
    {
        var node = await GetParentNode(editNodeDto.Id);

        if (node is null)
            throw new Exception("node is null");

        node.Name = editNodeDto.Name ?? node.Name;
        node.ParentNodeId = editNodeDto.ParentNodeId ?? node.ParentNodeId;

        dbContext.Nodes.Update(node);
        await dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteNode(string name)
    {
        var node = await GetNodeByName(name);

        if (node is null)
            throw new Exception("node is not found");

        dbContext.Nodes.Remove(node);
        await dbContext.SaveChangesAsync();
    }
    
    private NodeDto MapNodeToDto(Node node)
    {
        var nodeDto = new NodeDto
        {
            Id = node.Id,
            Name = node.Name,
            ParentNodeId = node.ParentNodeId,
            Childrens = node.Childrens.Select(childNode => MapNodeToDto(childNode)).ToList()
        };

        return nodeDto;
    }

    private async Task<Node> GetParentNode(Guid id)
        => await dbContext.Nodes.SingleOrDefaultAsync(n => n.Id == id);

    private async Task<Node> GetNodeByName(string name)
        => await dbContext.Nodes.SingleOrDefaultAsync(n => n.Name == name);
}