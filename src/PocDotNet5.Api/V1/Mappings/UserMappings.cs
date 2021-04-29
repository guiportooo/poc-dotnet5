namespace PocDotNet5.Api.V1.Mappings
{
    using AutoMapper;
    using Domain.Commands;
    using Domain.Queries;
    using Schemas.Requests;
    using Schemas.Responses;

    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<CreateUserRequest, CreateUserCommand>();
            CreateMap<UserQueryResult, UserCreatedResponse>();
        }
    }
}