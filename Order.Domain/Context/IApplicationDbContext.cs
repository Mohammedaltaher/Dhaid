using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Domain.Context;
public interface IApplicationDbContext
{

    DbSet<Order> Orders { get; set; }
    DbSet<OrderItem> OrderItems { get; set; }
    Task<int> SaveChangesAsync();
}

