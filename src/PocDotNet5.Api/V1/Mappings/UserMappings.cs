namespace PocDotNet5.Api.V1.Mappings
{
    using AutoMapper;
    using Domain.Queries;
    using Schemas.Requests;
    using Schemas.Responses;

    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<CreateUser, Domain.Commands.CreateUser>();
            CreateMap<UserData, UserCreated>();
        }
    }
}