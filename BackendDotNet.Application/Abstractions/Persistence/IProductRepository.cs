using System;
using System.Collections.Generic;
using System.Text;
using BackendDotNet.Domain.Entities;

namespace BackendDotNet.Application.Abstractions.Persistence;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id, CancellationToken ct = default);
    void Add(Product product);
    void Remove(Product product);
}