using CQRSMediator.Domain.Entities;
using CQRSMediator.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSMediator.Application.Commands.Handlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly ICustomerRepository _context;
        private readonly IMediator _mediator;

        public CreateCustomerCommandHandler(ICustomerRepository context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email
            };

            _context.Add(customer);

            return customer.Id;
        }
    }
}
