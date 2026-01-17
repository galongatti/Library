using Library.Model.DTO;
using Library.Model.Entities;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;


[ApiController]
[Route("api/lends")]
[Authorize(Roles = "InternalUser")]
public class LendController(ILendService lendService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateLend model)
    {
        Lend lend = await lendService.CreateLendAsync(model);
        ReadLend read = ReadLend.FromLend(lend);
        return CreatedAtAction(nameof(Get), new { id = read.Id }, read);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<Lend> lends = await lendService.GetLendsAsync();
        List<ReadLend> read = ReadLend.FromLends(lends);
        return Ok(read);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        Lend lend = await lendService.GetLendByIdAsync(id);
        ReadLend read = ReadLend.FromLend(lend);
        return Ok(read);
    }

    [HttpPut("{id:int}/approve")]
    public async Task<IActionResult> Approve([FromRoute] int id, [FromBody] ApproveLend model)
    {
        bool ok = await lendService.ApproveLendAsync(id, model.ExpectedReturnDate);
        if (ok) return Ok();
        return BadRequest();
    }

    [HttpGet("{id:int}/items")]
    public async Task<IActionResult> GetItems([FromRoute] int id)
    {
        List<LendItem> items = await lendService.GetItemsByLendIdAsync(id);
        List<ReadLendItem> readLendItems = ReadLendItem.FromLendsItens(items);
        return Ok(readLendItems);
    }

    [HttpPost("{id:int}/items")]
    public async Task<IActionResult> AddItem([FromRoute] int id, [FromBody] AddLendItemModel model)
    {
        LendItem item = await lendService.AddItemAsync(id, model);
        return CreatedAtAction(nameof(GetItems), new { id }, item);
    }

    [HttpDelete("{lendId:int}/items/{itemId:int}")]
    public async Task<IActionResult> RemoveItem([FromRoute] int lendId, [FromRoute] int itemId)
    {
        bool ok = await lendService.RemoveItemAsync(lendId, itemId);
        if (ok) return Ok();
        return BadRequest();
    }

    [HttpPut("{id:int}/return")]
    public async Task<IActionResult> Return([FromRoute] int id)
    {
        bool ok = await lendService.ReturnLendAsync(id);
        if (ok) return Ok();
        return BadRequest();
    }

    [HttpPut("{id:int}/cancel")]
    public async Task<IActionResult> Cancel([FromRoute] int id)
    {
        bool ok = await lendService.CancelLendAsync(id);
        if (ok) return Ok();
        return BadRequest();
    }
}
