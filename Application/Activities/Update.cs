using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            readonly IMapper _mapper;
            
            public Handler(DataContext context,
                           IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            async Task<Unit> IRequestHandler<Command, Unit>.Handle(Command request,
                                                                   CancellationToken cancellationToken)
            {
                var activity = await _context.Activities
                                             .FirstOrDefaultAsync(x => x.Id == request.Activity
                                                                                      .Id);
                _mapper.Map(request.Activity, activity);
                await _context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}