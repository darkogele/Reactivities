using Application.Account.DTOs;
using Application.Core;
using Application.Services;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Account
{
    public static class Login
    {
        public record Query(LoginDto LoginDto) : IRequest<Result<UserDto>>;

        public class Handler : IRequestHandler<Query, Result<UserDto>>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly IMapper _mapper;
            private readonly TokenService _tokenService;

            public Handler(
                UserManager<AppUser> userManager,
                SignInManager<AppUser> signInManager,
                IMapper mapper,
                TokenService tokenService)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _mapper = mapper;
                _tokenService = tokenService;
            }

            public async Task<Result<UserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.LoginDto.Email);

                if (user == null) return Result<UserDto>.Failure("Unauthorized", HttpStatusCode.Unauthorized);

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.LoginDto.Password, false);

                if (!result.Succeeded)
                    return Result<UserDto>.Failure("Invalid Password Unauthorized", HttpStatusCode.Unauthorized);

                var userDto = _mapper.Map<UserDto>(user);

                userDto.Token = await _tokenService.CreateTokenAsync(user);

                return Result<UserDto>.Success(userDto);
            }
        }
    }
}