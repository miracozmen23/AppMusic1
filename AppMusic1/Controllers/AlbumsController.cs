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
    public class AlbumsController : ControllerBase
    {
        private readonly RepositoryContext _context;
        private readonly IMapper _mapper;

        public AlbumsController(RepositoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<Album>>> GetAllAlbums([FromQuery]RequestParameters reqParameters)
        {
            var query = _context.Albums
            .Include(s => s.Singers)
            .AsQueryable();

            switch (reqParameters.OrderBy?.ToLower())
            {
                case "id":
                    query = reqParameters.OrderByDescending ? query.OrderByDescending(s => s.Id) : query.OrderBy(s => s.Id);
                    break;
                case "name":
                    query = reqParameters.OrderByDescending ? query.OrderByDescending(s => s.Name) : query.OrderBy(s => s.Name);
                    break;
                default:
                    query = query.OrderBy(s => s.Id);
                    break;
            }

            var albums = await query
                .Skip((reqParameters.PageNumber - 1) * reqParameters.PageSize)
                .Take(reqParameters.PageSize)
                .ToListAsync();

            return Ok(albums);
        }
        [Authorize(Roles = "admin,StandartUser")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Album>> GetOneAlbum(int id)
        {
            var album = await _context.Albums
                .Include(s => s.Singers)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (album is null)
                throw new NotFoundException($"Album with id {id} could not found.");

            return Ok(album);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<AlbumDto>> CreateOneAlbum([FromBody]AlbumDto albumDto)
        {
            var album = _mapper.Map<Album>(albumDto);
            _context.Albums.Add(album);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllAlbums), new { id = album.Id }, albumDto);
        }
        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneAlbum([FromRoute(Name ="id")]int id,
            [FromBody] AlbumDto albumDto)
        {
            if (id != albumDto.Id)
            {
                throw new BadRequestException("Album ID mismatch.");
            }
            var album = _mapper.Map<Album>(albumDto);
            _context.Entry(album).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneAlbum([FromRoute(Name = "id")] int id)
        {
            var album = await _context.Albums.FindAsync(id);

            if(album is null)
                throw new NotFoundException($"Song with id {id} could not found.");

            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
