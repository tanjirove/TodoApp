using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.Entities;

namespace Todo.Application.Common.Contracts
{
    public interface IApplicationDbContext
    {
        DbSet<TodoItem> TodoItems { get; set; }
 
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
