using CQRSMediator.Application.Services.Notifications;
using CQRSMediator.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSMediator.Application.Commands.Handlers
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, int>
    {
        private readonly ICustomerRepository _context;
        private readonly IMediator _mediator;

        public UpdateCustomerCommandHandler(ICustomerRepository context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<int> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.GetById(request.Id);

            if (customer == null)
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
                customer.Name = request.Name;
                customer.Email = request.Email;

                _context.Update(customer);

                await _mediator.Publish(new CustomerActionNotification
                {
                    Name = request.Name,
                    Email = request.Email,
                    Action = ActionNotification.Updated
                }, cancellationToken);

                return customer.Id;
            }
        }
    }
}
