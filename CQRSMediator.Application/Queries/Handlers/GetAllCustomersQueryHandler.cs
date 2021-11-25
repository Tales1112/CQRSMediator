using CQRSMediator.Domain.Entities;
using CQRSMediator.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSMediator.Application.Queries
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<Customer>>
    {
        private readonly ICustomerRepository _context;
        private readonly IMediator _mediator;
        public GetAllCustomersQueryHandler(ICustomerRepository context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<IEnumerable<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customerList = await _context.GetAll();

            if (customerList is null) return default;

            return customerList;
        }
    }
}
