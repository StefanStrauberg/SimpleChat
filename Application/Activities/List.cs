using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<List<Activity>> {}

        internal class Handler : IRequestHandler<Query, List<Activity>>
        {
            readonly DataContext _context;
            
            public Handler(DataContext context)
                => _context = context;

            async Task<List<Activity>> IRequestHandler<Query, List<Activity>>.Handle(Query request,
                                                                                     CancellationToken cancellationToken)
            {
                return await _context.Activities
                                     .AsNoTracking()
                                     .ToListAsync();
            }
        }
    }
}