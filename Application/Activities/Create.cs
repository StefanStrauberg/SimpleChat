using System.Threading.Tasks;
using MediatR;
using Persistence;
using System.Threading;
using Domain;

namespace Application.Activities
{
    public class Create
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

            async Task<Unit> IRequestHandler<Command, Unit>.Handle(Command request,
                                                                   CancellationToken cancellationToken)
            {
                _context.Activities.Add(request.Activity);
                await _context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}