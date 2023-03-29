using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<Activity> 
        {
            public Guid Id { get; set; }
        }

        internal class Handler : IRequestHandler<Query, Activity>
        {
            readonly DataContext _context;
            
            public Handler(DataContext context)
                => _context = context;

            async Task<Activity> IRequestHandler<Query, Activity>.Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Activities
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(x => x.Id == request.Id);
            }
        }
    }
}