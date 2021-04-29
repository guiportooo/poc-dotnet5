namespace PocDotNet5.Api.Domain.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Entities;
    using MediatR;
    using Repositories;

    public record CreateUserCommand : IRequest<int>
    {
        public CreateUserCommand(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
        }

        public string FirstName { get;  protected set;}
        public string LastName { get;  protected set;}
        public string Email { get;  protected set;}
        public DateTime DateOfBirth { get; protected set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<int> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(command);
            await _userRepository.AddAsync(user);
            return user.Id;
        }
    }
}