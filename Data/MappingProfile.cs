
using AutoMapper;
using my_book_store_v1.Data.DTOs;
using my_book_store_v1.Data.Models;

namespace my_book_store_v1.Data;
public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Test, TestDTO>()
            .ForMember(des => des.TestId, src => src.MapFrom(src => src.Id))
            .ForMember(des => des.IsTax, src => src.MapFrom(src => src.Salary >= 3000))
            .ReverseMap();

        CreateMap<ApiUser, userDTO>().ReverseMap();







    }
}
