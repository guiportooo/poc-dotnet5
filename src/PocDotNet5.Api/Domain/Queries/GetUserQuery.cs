namespace PocDotNet5.Api.Domain.Queries
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using Repositories;

    public record UserQueryResult(string FirstName, 
        string LastName, 
        string FullName, 
        string Email,
        DateTime DateOfBirth, 
        bool Active);

    public record GetUserQuery : IRequest<UserQueryResult?>
    {
        public GetUserQuery(int id) => Id = id;

        public int Id { get; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserQueryResult?>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserQueryResult?> Handle(GetUserQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(query.Id);
            return _mapper.Map<UserQueryResult>(user);
        }
    }
}