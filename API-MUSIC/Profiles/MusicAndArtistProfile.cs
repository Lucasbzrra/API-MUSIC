using API_MUSIC.Controllers.Models;
using API_MUSIC.Data.Dtos;
using AutoMapper;

namespace API_MUSIC.Profiles;

public class MusicAndArtistProfile: Profile
{
    public MusicAndArtistProfile()
    {
        //criação do AutoMapper para realizar a conversão de DTO para classe ARTIST
        CreateMap<CreateArtistDto, Artist>();
        CreateMap<UpdateArtistDto, Artist>();
        CreateMap<Artist,UpdateArtistDto>();
        CreateMap<Artist, ReadOnlyArtist>().ForMember(destino => destino.ReadMusics, opt => opt.MapFrom(origem => origem.Musics)).ForMember(teste=>teste.ReadAddress, opt=>opt.MapFrom(ok => ok.Address));
        // Limite a profundidade
        // CreateMap<Artist, ReadOnlyArtist>().IncludeMembers(src => src).ForMember(destino => destino.ReadMusics, opt => opt.MapFrom(origem => origem.Musics));
        //.ForMember(destino => destino.ReadMusics, opt => opt.MapFrom(origem => origem.Musics));
        //criação do AutoMapper para realizar a conversão de DTO para classe MUSIC
        CreateMap<CreateAndressDto, Address>();
        CreateMap<Address, ReadAndressDto>();
        CreateMap<UpdateAddressDto,Address>();


        CreateMap<CreateMusicDto, Music>();
        CreateMap<UpdateMusicDto, Music>();
        CreateMap<Music, ReadMusicDto>();
        CreateMap<Music, UpdateMusicDto>();
    }

}
