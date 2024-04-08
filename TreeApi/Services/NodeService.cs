using Microsoft.EntityFrameworkCore;
using TreeApp.DAL;
using TreeApp.DTO;
using TreeApp.Interfaces;
using TreeApp.Models;

namespace TreeApp.Services;

public class NodeService(TreeDbContext dbContext) : INodeService
{
    public async Task<List<NodeDto>> GetNodes(bool isReversed)
    {
        var nodes = await dbContext.Nodes
            .Include(n => n.Childrens)
            .ToListAsync();

        var rootNodes = nodes.Where(node => node.ParentNodeId == null).ToList();

        var rootNodesDto = rootNodes
            .Select(rootNode => MapNodeToDto(rootNode, isReversed))
            .ToList();

        return isReversed ? rootNodesDto.OrderByDescending(node => node.Name).ToList() : rootNodesDto.OrderBy(node => node.Name).ToList();
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

        node.Name = editNodeDto.NewName ?? node.Name;
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
    
    private NodeDto MapNodeToDto(Node node, bool isReversed)
    {
        var nodeDto = new NodeDto
        {
            Id = node.Id,
            Name = node.Name,
            ParentNodeId = node.ParentNodeId,
            Childrens = isReversed ? node.Childrens.Select(childNode => MapNodeToDto(childNode, isReversed)).OrderByDescending(child => child.Name).ToList() :
                node.Childrens.Select(childNode => MapNodeToDto(childNode, isReversed)).OrderBy(child => child.Name).ToList()
        };

        return nodeDto;
    }

    private async Task<Node> GetParentNode(Guid id)
        => await dbContext.Nodes.SingleOrDefaultAsync(n => n.Id == id);

    private async Task<Node> GetNodeByName(string name)
        => await dbContext.Nodes.SingleOrDefaultAsync(n => n.Name == name);
}