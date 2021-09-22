using Domain;
using MediatR;
using Persistance;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activitites
{
    public static class Create
    {
        public record Command(Activity Activity) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _context.Activities.AddAsync(request.Activity);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
