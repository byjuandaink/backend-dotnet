using BackendDotNet.Application.Abstractions.Persistence;
using BackendDotNet.Domain.Entities;
using BackendDotNet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendDotNet.Infrastructure.Repositories;

internal sealed class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context) => _context = context;

    public Task<Category?> GetByIdAsync(int id, CancellationToken ct = default)
        => _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id, ct);

    public Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default)
        => _context.Categories.AnyAsync(c => c.CategoryName == name, ct);

    public void Add(Category category) => _context.Categories.Add(category);
}