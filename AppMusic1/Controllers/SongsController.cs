using AppMusic1.Dtos;
using AppMusic1.Exceptions;
using AppMusic1.Models;
using AppMusic1.Repositories;
using AppMusic1.RequestFeatures;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppMusic1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly RepositoryContext _context;
        private readonly IMapper _mapper;


        public SongsController(RepositoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<Song>>> GetAllSongs([FromQuery] SongParameters songParameters)
        {
            if(!songParameters.ValidDurationRange)
                throw new DurationOutOfRangeBadRequestException();

            var query = _context.Songs
            .Include(a => a.Album)
            .ThenInclude(s => s.Singers)
            .AsQueryable();

            if (songParameters.MinDuration > 0)
            {
                query = query.Where(s => s.Duration >= songParameters.MinDuration);
            }

            if (songParameters.MaxDuration > 0)
            {
                query = query.Where(s => s.Duration <= songParameters.MaxDuration);
            }

            switch (songParameters.OrderBy?.ToLower())
            {
                case "id":
                    query = songParameters.OrderByDescending ? query.OrderByDescending(s => s.Id) : query.OrderBy(s => s.Id);
                    break;
                case "name":
                    query = songParameters.OrderByDescending ? query.OrderByDescending(s => s.Name) : query.OrderBy(s => s.Name);
                    break;
                case "duration":
                    query = songParameters.OrderByDescending ? query.OrderByDescending(s => s.Duration) : query.OrderBy(s => s.Duration);
                    break;
                default:
                    query = query.OrderBy(s => s.Id); 
                    break;
            }

            var songs = await query
                .Skip((songParameters.PageNumber - 1) * songParameters.PageSize)
                .Take(songParameters.PageSize)
                .ToListAsync();

            return Ok(songs);
        }
        [Authorize(Roles = "admin,StandartUser")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Song>> GetOneSong([FromRoute(Name ="id")] int id)
        {
            var song = await _context
                .Songs
                .Include (a=>a.Album)
                .ThenInclude(s=> s.Singers)
                .SingleOrDefaultAsync(s=>s.Id == id);
               
            if (song is null)
                throw new NotFoundException($"Song with id {id} could not found.");

            return song;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<SongDto>> CreateOneSong([FromBody] SongDto songDto)
        {
            var song = _mapper.Map<Song>(songDto);
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllSongs), new { id = song.Id }, songDto);
        }
        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneSong([FromRoute(Name = "id")] int id ,
            [FromBody]SongDto songDto)
        {
            if (id != songDto.Id)
            {
                throw new BadRequestException("Song ID mismatch.");
            }
            var song = _mapper.Map<Song>(songDto);
            _context.Entry(song).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();

        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneSong([FromRoute(Name ="id")] int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if(song is null)
                throw new NotFoundException($"Song with id {id} could not found.");

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent(); 
        }
    }
    }

