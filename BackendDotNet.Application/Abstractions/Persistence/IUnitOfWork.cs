using System;
using System.Collections.Generic;
using System.Text;
namespace BackendDotNet.Application.Abstractions.Persistence;

public interface IUnitOfWork
{
    IContactRepository Contacts { get; }
    ICategoryRepository Categories { get; }
    IProductRepository Products { get; }

    Task<int> SaveChangesAsync(CancellationToken ct = default);
}