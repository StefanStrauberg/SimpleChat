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
    public class Details
    {
        public record Query(Guid Id) : IRequest<ActivityDto>;

        internal class Handler : IRequestHandler<Query, ActivityDto>
        {
            readonly DataContext _context;
            readonly IMapper _mapper;
            
            public Handler(DataContext context,
                           IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            async Task<ActivityDto> IRequestHandler<Query, ActivityDto>.Handle(Query request,
                                                                         CancellationToken cancellationToken)
            {
                var activity = await _context.Activities
                                             .AsNoTracking()
                                             .FirstOrDefaultAsync(x => x.Id == request.Id,
                                                                  cancellationToken);
                var activityDto = _mapper.Map<ActivityDto>(activity);
                return activityDto;
            }
        }
    }
}