using System;
using System.Collections.Generic;
using System.Text;
using BackendDotNet.Domain.Entities;

namespace BackendDotNet.Application.Abstractions.Persistence;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default);
    void Add(Category category);
}
