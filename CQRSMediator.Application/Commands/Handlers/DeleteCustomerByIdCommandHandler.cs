using CQRSMediator.Application.Services.Notifications;
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

            if (customer is null)
            {
                await _mediator.Publish(new ErrorNotification
                {
                    Error = "Customer not Found",
                    Stack = "Customer is null"
                }, cancellationToken);
                return default;
            }
            else
            {
                await _mediator.Publish(new CustomerActionNotification
                {
                    Name = customer.Name,
                    Email = customer.Email,
                    Action = ActionNotification.Deleted
                }, cancellationToken);
                _context.Remove(customer);

                return customer.Id;
            }
        }
    }
}
