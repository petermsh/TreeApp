using Microsoft.AspNetCore.Mvc;
using TreeApp.DTO;
using TreeApp.Interfaces;

namespace TreeApp.Controllers;

[ApiController]
[Route("[controller]")]
public class NodeController(INodeService nodeService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<NodeDto>>> GetNodes(bool isReversed)
    {
        var nodes = await nodeService.GetNodes(isReversed);

        return Ok(nodes);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddNode(AddNodeDto addNodeDto)
    {
        await nodeService.AddNode(addNodeDto);

        return NoContent();
    }
    
    [HttpPut]
    public async Task<IActionResult> EditNode(EditNodeDto editNodeDto)
    {
        await nodeService.EditNode(editNodeDto);

        return NoContent();
    }
    
    [HttpDelete()]
    public async Task<IActionResult> DeleteNode(string name)
    {
        await nodeService.DeleteNode(name);

        return NoContent();
    }
}