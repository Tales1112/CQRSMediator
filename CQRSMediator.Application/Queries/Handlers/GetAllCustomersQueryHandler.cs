using CQRSMediator.Application.Services.Notifications;
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

            if (customerList is null)
            {
                await _mediator.Publish(new ErrorNotification
                {
                    Error = "Customer not Found",
                    Stack = "Customer is null"
                }, cancellationToken);
                return default;
            }
            return customerList;
        }
    }
}
