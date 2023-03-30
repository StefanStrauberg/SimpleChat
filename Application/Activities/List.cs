using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public record Query : IRequest<List<ActivityDto>>;

        internal class Handler : IRequestHandler<Query, List<ActivityDto>>
        {
            readonly DataContext _context;
            readonly IMapper _mapper;
            
            public Handler(DataContext context,
                           IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            async Task<List<ActivityDto>> IRequestHandler<Query, List<ActivityDto>>.Handle(Query request,
                                                                                     CancellationToken cancellationToken)
            {
                var activities =  await _context.Activities
                                                .AsNoTracking()
                                                .ToListAsync(cancellationToken);
                var activityDtos = _mapper.Map<List<ActivityDto>>(activities);
                return activityDtos;
            }
        }
    }
}