using CQRSMediator.Domain.Entities;
using CQRSMediator.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSMediator.Application.Queries.Handlers
{
    public class GetAllCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
    {
        private readonly ICustomerRepository _context;
        private readonly IMediator _mediator;
        public GetAllCustomerByIdQueryHandler(ICustomerRepository context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public  async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _context.GetById(request.Id);

            if (customer == null) return default;

            return customer;
        }
    }
}
