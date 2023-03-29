
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
        public class Command : IRequest<Unit>
        {
            public Guid Id { get; set; }
        }

        internal class Handler : IRequestHandler<Command, Unit>
        {
            readonly DataContext _context;
            
            public Handler(DataContext context)
                => _context = context;
                
            async Task<Unit> IRequestHandler<Command, Unit>.Handle(Command request, CancellationToken cancellationToken)
            {
                var activityToDelete = await _context.Activities
                                                     .AsNoTracking()
                                                     .FirstOrDefaultAsync(x => x.Id == request.Id);
                // TODO if null
                _context.Activities.Remove(activityToDelete);
                await _context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}