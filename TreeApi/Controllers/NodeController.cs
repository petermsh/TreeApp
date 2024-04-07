using Microsoft.AspNetCore.Mvc;
using TreeApp.DTO;
using TreeApp.Interfaces;

namespace TreeApp.Controllers;

[ApiController]
[Route("[controller]")]
public class NodeController(INodeService nodeService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<NodeDto>>> GetNodes()
    {
        var nodes = await nodeService.GetNodes();

        return Ok(nodes);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddNode(AddNodeDto addNodeDto)
    {
        await nodeService.AddNode(addNodeDto);

        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> EditNode(EditNodeDto editNodeDto)
    {
        await nodeService.EditNode(editNodeDto);

        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteNode(string name)
    {
        await nodeService.DeleteNode(name);

        return Ok();
    }
}