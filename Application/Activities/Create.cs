using System;
using System.Threading.Tasks;
using MediatR;
using Persistence;
using System.Threading;
using Domain;
using AutoMapper;
using Application.Models;

namespace Application.Activities
{
    public class Create
    {
        public record Command(CreateActivityDto createActivityDto) : IRequest<Guid>;

        internal class Handler : IRequestHandler<Command, Guid>
        {
            readonly DataContext _context;
            readonly IMapper _mapper;
            
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            async Task<Guid> IRequestHandler<Command, Guid>.Handle(Command request, CancellationToken ct)
            {
                var activityToCreate = _mapper.Map<Activity>(request.createActivityDto);
                _context.Activities.Add(activityToCreate);
                await _context.SaveChangesAsync(ct);
                return activityToCreate.Id;
            }
        }
    }
}