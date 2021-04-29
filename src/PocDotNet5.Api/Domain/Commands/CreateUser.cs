namespace PocDotNet5.Api.Domain.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Entities;
    using MediatR;
    using Repositories;

    public record CreateUser : IRequest<int>
    {
        public CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
        }
        
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public DateTime DateOfBirth { get; }
    }

    public class CreateUserHandler : IRequestHandler<CreateUser, int>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<int> Handle(CreateUser command, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(command);
            await _userRepository.AddAsync(user);
            return user.Id;
        }
    }
}