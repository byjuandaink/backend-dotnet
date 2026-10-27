using BackendDotNet.Application.Abstractions.Persistence;
using BackendDotNet.Domain.Entities;
using BackendDotNet.Infrastructure.Persistence;

namespace BackendDotNet.Infrastructure.Repositories;

internal sealed class ContactRepository : IContactRepository
{
    private readonly ApplicationDbContext _context;

    public ContactRepository(ApplicationDbContext context) => _context = context;

    public void Add(Contact contact) => _context.Contacts.Add(contact);
}