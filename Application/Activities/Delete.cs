
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public record Command(Guid Id) : IRequest<Unit>;

        internal class Handler : IRequestHandler<Command, Unit>
        {
            readonly DataContext _context;
            
            public Handler(DataContext context)
                => _context = context;
                
            async Task<Unit> IRequestHandler<Command, Unit>.Handle(Command request,
                                                                   CancellationToken cancellationToken)
            {
                var activityToDelete = await _context.Activities
                                                     .AsNoTracking()
                                                     .FirstOrDefaultAsync(x => x.Id == request.Id,
                                                                          cancellationToken);
                // FIXME
                if (activityToDelete is null)
                    return Unit.Value;
                _context.Activities.Remove(activityToDelete);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}