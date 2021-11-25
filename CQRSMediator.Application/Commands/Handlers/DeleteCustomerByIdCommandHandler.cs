using CQRSMediator.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSMediator.Application.Commands.Handlers
{
    public class DeleteCustomerByIdCommandHandler : IRequestHandler<DeleteCustomerByIdCommand, int>
    {
        private readonly ICustomerRepository _context;
        private readonly IMediator _mediator;

        public DeleteCustomerByIdCommandHandler(ICustomerRepository context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<int> Handle(DeleteCustomerByIdCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.GetById(request.Id);

            if (customer is null) return default;

            _context.Remove(customer);

            return customer.Id;
        }
    }
}
