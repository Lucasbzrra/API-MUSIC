using API_MUSIC.Data.Dtos;
using API_MUSIC.Models;
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

        CreateMap<CreateAndressDto, Address>();
        CreateMap<Address, ReadAndressDto>();
        CreateMap<UpdateAddressDto,Address>();


        CreateMap<CreateMusicDto, Music>();
        CreateMap<UpdateMusicDto, Music>();
        CreateMap<Music, ReadMusicDto>();
        CreateMap<Music, UpdateMusicDto>();
       

        CreateMap<CreateUserDto, User>();

        CreateMap<CreateAdminDto, Administrator>();
    }

}
