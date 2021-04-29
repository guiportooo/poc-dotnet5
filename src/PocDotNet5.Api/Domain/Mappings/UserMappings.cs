namespace PocDotNet5.Api.Domain.Mappings
{
    using AutoMapper;
    using Commands;
    using Entities;
    using Queries;

    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<CreateUserCommand, User>();
            CreateMap<User, UserQueryResult>();
        } 
    }
}