using CQRSMediator.Domain.Entities;
using MediatR;

namespace CQRSMediator.Application.Queries
{
    public class GetCustomerByIdQuery : IRequest<Customer>
    {
        public int Id { get; set; }
    }
}
