using MediatR;

namespace CQRSMediator.Application.Commands
{
    public class UpdateCustomerCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
