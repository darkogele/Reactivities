using Application.Activities.DTOs;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Profile = Application.Profiles.Profile;

namespace Application.Activities
{
    public static class Details
    {
        public record Query(Guid Id) : IRequest<Result<ActivityDto>>;

        public class Handler : IRequestHandler<Query, Result<ActivityDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<ActivityDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                //var activity = await _context.Activities
                //    .Include(x => x.Attendees)
                //    .FirstOrDefaultAsync(x =>x.Id == request.Id, cancellationToken);

                //var peski = await _context.Activities.Select(x =>
                //new ActivityDto
                //{
                //    Id = x.Id,
                //    City = x.City,
                //    Category = x.Category,
                //    Date = x.Date,
                //    Description = x.Description,
                //    Title = x.Title,
                //    Venue = x.Venue,
                //    HostUserName = x.Attendees.FirstOrDefault(a => a.IsHost).AppUser.UserName,
                //    Attendees = x.Attendees.Select(p => new Profile
                //    {
                //        Bio = p.AppUser.Bio,
                //        DisplayName = p.AppUser.DisplayName,
                //        UserName = p.AppUser.DisplayName
                //    }).ToList()
                //}).FirstOrDefaultAsync(a => a.Id.Equals(request.Id), cancellationToken);

                var activity = await _context.Activities
                    .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                return Result<ActivityDto>.Success(activity);
            }
        }
    }
}