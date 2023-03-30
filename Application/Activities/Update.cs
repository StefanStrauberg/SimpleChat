using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class Update
    {
        public record Command(Guid Id, UpdateActivityDto UpdateActivityDto) : IRequest<Unit>;

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
                var activityToUpdate = await _context.Activities
                                                     .FirstOrDefaultAsync(x => x.Id == request.Id,
                                                                          cancellationToken);
                // FIXME
                if (activityToUpdate is null)
                    return Unit.Value;
                _mapper.Map(request.UpdateActivityDto, activityToUpdate);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}