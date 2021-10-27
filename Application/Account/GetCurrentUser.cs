using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Account.DTOs;
using Application.Core;
using Application.Services;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Account
{
    public static class GetCurrentUser
    {
        public record Query(ClaimsPrincipal User) : IRequest<Result<UserDto>>;

        public class Handler : IRequestHandler<Query, Result<UserDto>>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly TokenService _tokenService;
            private readonly IMapper _mapper;

            public Handler(UserManager<AppUser> userManager, TokenService tokenService, IMapper mapper)
            {
                _userManager = userManager;
                _tokenService = tokenService;
                _mapper = mapper;
            }

            public async Task<Result<UserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.User.FindFirstValue(ClaimTypes.Email));

                var userDto = _mapper.Map<UserDto>(user);

                userDto.Token = await _tokenService.CreateTokenAsync(user);

                return Result<UserDto>.Success(userDto);
            }
        }
    }
}