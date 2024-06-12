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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AppMusic1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SingersController : ControllerBase
    {
        private readonly RepositoryContext _context;
        private readonly IMapper _mapper;

        public SingersController(RepositoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<Singer>>> GetAllSingers([FromQuery] RequestParameters reqParameters)
        {
            var query = _context.Singers
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

            var singers = await query
                .Skip((reqParameters.PageNumber - 1) * reqParameters.PageSize)
                .Take(reqParameters.PageSize)
                .ToListAsync();

            return Ok(singers);
        }
        [Authorize(Roles = "admin,StandartUser")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Singer>> GetOneSinger([FromRoute(Name = "id")] int id)
        {
            var singer = await _context.Singers.FindAsync(id);
            if (singer is null)
                throw new NotFoundException($"Singer with id {id} could not found.");

            return singer;
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<SingerDto>> CreateOneSinger([FromBody] SingerDto singerDto)
        {
            var singer = _mapper.Map<Singer>(singerDto);
            _context.Singers.Add(singer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllSingers), new { id = singer.Id }, singerDto);
        }
        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneSinger([FromRoute(Name = "id")] int id,
            [FromBody] SingerDto singerDto)
        {
            if (id != singerDto.Id)
            {
                throw new BadRequestException("Singer ID mismatch.");
            }
            var artist = _mapper.Map<Singer>(singerDto);
            _context.Entry(artist).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneSinger([FromRoute(Name ="id")]int id)
        {
            var singer = await _context.Singers.FindAsync(id);
            if(singer is null)
                throw new NotFoundException($"Singer with id {id} could not found.");

            _context.Singers.Remove(singer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
