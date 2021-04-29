namespace PocDotNet5.Api.V2.Mappings
{
    using AutoMapper;
    using Domain.Entities;
    using Models.Requests;
    using Models.Responses;

    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<CreateUser, User>();
            CreateMap<User, UserCreated>();
        }
    }
}