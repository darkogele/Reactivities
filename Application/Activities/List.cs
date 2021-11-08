using Application.Activities.DTOs;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Profile = Application.Profiles.Profile;

namespace Application.Activities
{
    public static class List
    {
        public record Query : IRequest<Result<List<ActivityDto>>>;

        public class Handler : IRequestHandler<Query, Result<List<ActivityDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<ActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.Activities
                    .Select(x => new ActivityDto
                    {
                        Id = x.Id,
                        Category = x.Category,
                        City = x.City,
                        Date = x.Date,
                        Description = x.Description,
                        Title = x.Title,
                        Venue = x.Venue,
                        HostUserName = x.Attendees.FirstOrDefault(x => x.IsHost).AppUser.UserName,
                        Attendees = x.Attendees.Select(p => new Profile
                        {
                            Bio = p.AppUser.Bio,
                            DisplayName = p.AppUser.DisplayName,
                            UserName = p.AppUser.UserName
                        }).ToList()
                    }).ToListAsync(cancellationToken);

                var activities = await _context.Activities
                   .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider)
                   .ToListAsync(cancellationToken);

                return Result<List<ActivityDto>>.Success(activities);
            }
        }
    }
}