using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Update
    {
        public class Command : IRequest<Unit>
        {
            public Activity Activity { get; set; }
        }

        internal class Handler : IRequestHandler<Command, Unit>
        {
            readonly DataContext _context;
            
            public Handler(DataContext context)
                => _context = context;

            async Task<Unit> IRequestHandler<Command, Unit>.Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Activities.Update(request.Activity);
                await _context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}