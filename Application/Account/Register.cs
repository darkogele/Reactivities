using Application.Account.DTOs;
using Application.Core;
using Application.Services;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Account
{
    public static class Register
    {
        public record Command(RegisterDto RegisterDto) : IRequest<Result<UserDto>>;

        public class Handler : IRequestHandler<Command, Result<UserDto>>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IMapper _mapper;
            private readonly TokenService _tokenService;

            public Handler(UserManager<AppUser> userManager, IMapper mapper, TokenService tokenService)
            {
                _userManager = userManager;
                _mapper = mapper;
                _tokenService = tokenService;
            }

            public async Task<Result<UserDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _userManager.Users.AnyAsync(x => x.Email == request.RegisterDto.Email, cancellationToken))
                {
                    var error = new Dictionary<string, string> { { "Email", "Email taken" } };

                    return Result<UserDto>.Failure("Email taken");
                }

                if (await _userManager.Users.AnyAsync(x => x.UserName == request.RegisterDto.UserName, cancellationToken))
                {
                    var error = new Dictionary<string, string> { { "Email", "Email taken" } };

                    return Result<UserDto>.Failure("UserName Taken");
                }

                var user = _mapper.Map<AppUser>(request.RegisterDto);

                var result = await _userManager.CreateAsync(user, request.RegisterDto.Password);

                if (!result.Succeeded) return Result<UserDto>.Failure("Problem registering user");

                var roleResult = await _userManager.AddToRoleAsync(user, RoleNames.ROLE_MEMBER);

                if (!roleResult.Succeeded)
                    return Result<UserDto>.Failure(roleResult.Errors.ToString());

                var userDto = _mapper.Map<UserDto>(user);

                userDto.Token = await _tokenService.CreateTokenAsync(user);

                return Result<UserDto>.Success(userDto);
            }
        }
    }
}