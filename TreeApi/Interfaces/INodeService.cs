using TreeApp.DTO;

namespace TreeApp.Interfaces;

public interface INodeService
{
    public Task<List<NodeDto>> GetNodes(bool isReversed);
    public Task AddNode(AddNodeDto addNodeDto);
    public Task EditNode(EditNodeDto editNodeDto);
    public Task DeleteNode(string name);
}