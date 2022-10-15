using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Domain.Context;
public interface IApplicationDbContext
{

    DbSet<Discount> Discounts { get; set; }
    Task<int> SaveChangesAsync();
}

