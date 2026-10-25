using BackendDotNet.Application.Abstractions.Persistence;
using BackendDotNet.Infrastructure.Persistence;

namespace BackendDotNet.Infrastructure;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public UnitOfWork(
        ApplicationDbContext context,
        IContactRepository contacts,
        ICategoryRepository categories,
        IProductRepository products)
    {
        _context = context;
        Contacts = contacts;
        Categories = categories;
        Products = products;
    }

    public IContactRepository Contacts { get; }
    public ICategoryRepository Categories { get; }
    public IProductRepository Products { get; }

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => _context.SaveChangesAsync(ct);
}