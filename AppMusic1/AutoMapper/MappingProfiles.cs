using AppMusic1.Dtos;
using AppMusic1.Models;
using AutoMapper;

namespace AppMusic1.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Singer, SingerDto>().ReverseMap();
            CreateMap<Album, AlbumDto>().ReverseMap();
            CreateMap<Song, SongDto>().ReverseMap();
        }
    }
}
