using MediatR;

namespace CQRSMediator.Application.Commands
{
    public class DeleteCustomerByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
