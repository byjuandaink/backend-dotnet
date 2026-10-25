using BackendDotNet.Application.Abstractions.Persistence;
using BackendDotNet.Domain.Entities;
using BackendDotNet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendDotNet.Infrastructure.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context) => _context = context;

    public Task<Product?> GetByIdAsync(int id, CancellationToken ct = default)
        => _context.Products.FirstOrDefaultAsync(p => p.ProductId == id, ct);

    public void Add(Product product) => _context.Products.Add(product);

    public void Remove(Product product) => _context.Products.Remove(product);
}