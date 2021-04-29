namespace PocDotNet5.Api.Domain.Queries
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using Repositories;

    public record UserData(int Id, 
        string FirstName, 
        string LastName, 
        string FullName, 
        string Email,
        DateTime DateOfBirth, 
        bool Active);

    public record GetUser : IRequest<UserData?>
    {
        public GetUser(int id) => Id = id;

        public int Id { get; }
    }

    public class GetUserHandler : IRequestHandler<GetUser, UserData?>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUserHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserData?> Handle(GetUser query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(query.Id);
            return _mapper.Map<UserData>(user);
        }
    }
}