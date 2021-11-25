using CQRSMediator.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace CQRSMediator.Application.Queries
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<Customer>>
    {
    }
}
