using BackendDotNet.Domain.Entities;    
using System;
using System.Collections.Generic;
using System.Text;

namespace BackendDotNet.Application.Abstractions.Persistence;

public interface IContactRepository
{
    void Add(Contact contact);
}