﻿using Domain;
using MediatR;
using Persistance;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activitites
{
    public static class Details
    {
        public record Query(Guid Id) : IRequest<Activity>;

        public class Handler : IRequestHandler<Query, Activity>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Activity> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Activities.FindAsync(request.Id);
            }
        }
    }
}