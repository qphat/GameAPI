using GameAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideogameController : ControllerBase
{

    private readonly VideoGameDBContext? _context = context;
    private static VideoGameDBContext? context;

    public VideogameController(VideoGameDBContext? context)
    {
        _context = context;
    }

    private static List<VideoGame> _videoGames = new List<VideoGame>
    {
        new VideoGame { Id = 1, Title = "Halo", Platform = "Xbox", Developer = "Bungie", Publisher = "Microsoft" },
        new VideoGame { Id = 2, Title = "The Legend of Zelda: Breath of the Wild", Platform = "Nintendo Switch", Developer = "Nintendo", Publisher = "Nintendo" },
        new VideoGame { Id = 3, Title = "Final Fantasy VII", Platform = "PlayStation", Developer = "Square", Publisher = "Square" }
    };
    
    [HttpGet]
    public async Task<ActionResult<List<VideoGame>>> Get()
    {
        return Ok(await _context.VideoGames.ToListAsync());
    }
    
    [HttpGet("{id:int}")]
    public ActionResult<VideoGame> GetVideoGameById(int id)
    {
        var videoGame = _videoGames.FirstOrDefault(vg => vg.Id == id);
        if (videoGame == null)
        {
            return NotFound();
        }
        return Ok(videoGame);
    }
    
    [HttpPost]
    public ActionResult<VideoGame> Post(VideoGame videoGame)
    {
        videoGame.Id = _videoGames.Max(vg => vg.Id) + 1;
        _videoGames.Add(videoGame);
        return CreatedAtAction(nameof(GetVideoGameById), new { id = videoGame.Id }, videoGame);
    }
    
    [HttpPut("{id:int}")]
    public ActionResult<VideoGame> Put(int id, VideoGame videoGame)
    {
        var existingVideoGame = _videoGames.FirstOrDefault(vg => vg.Id == id);
        if (existingVideoGame == null)
        {
            return NotFound();
        }
        existingVideoGame.Title = videoGame.Title;
        existingVideoGame.Platform = videoGame.Platform;
        existingVideoGame.Developer = videoGame.Developer;
        existingVideoGame.Publisher = videoGame.Publisher;
        return Ok(existingVideoGame);
    }
    
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var videoGame = _videoGames.FirstOrDefault(vg => vg.Id == id);
        if (videoGame == null)
        {
            return NotFound();
        }
        _videoGames.Remove(videoGame);
        return NoContent();
    }

}