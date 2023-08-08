using API_MUSIC.Controllers.Models;
using API_MUSIC.Data.Dtos;
using AutoMapper;

namespace API_MUSIC.Profiles
{
    public class MusicAndArtistProfile: Profile
    {
        public MusicAndArtistProfile()
        {
            //criação do AutoMapper para realizar a conversão de DTO para classe ARTIST
            CreateMap<CreateArtistDto, Artist>();
            CreateMap<UpdateArtistDto, Artist>();
            CreateMap<Artist,UpdateArtistDto>();
            CreateMap<Artist, ReadOnlyArtist>();
            //criação do AutoMapper para realizar a conversão de DTO para classe MUSIC
            CreateMap<CreateMusicDto, Music>();
            CreateMap<UpdateMusicDto, Music>();
            CreateMap<Music, ReadMusicDto>();
            CreateMap<Music, UpdateMusicDto>();
        }
    }
}
