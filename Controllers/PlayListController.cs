using CRUDMongo.Domain.Entities;
using CRUDMongoDB.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRUDMongo.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayListController : ControllerBase
{
    private readonly MongoDBService _service;

    public PlayListController(MongoDBService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<List<Playlist>> Get()
    {
        return await _service.GetAsync();
    }
    [HttpPost]
    public async Task<IActionResult> Post(
            [FromBody] Playlist playlist)
    {
        await _service.CreateAsync(playlist);
        return CreatedAtAction(nameof(Get)
        , new
        {
            id = playlist.Id
        }, playlist);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AddToPlaylist(string id,
        [FromBody] string movieId)
    {
        await _service.AddToPlaylistAsync(id,movieId);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

}