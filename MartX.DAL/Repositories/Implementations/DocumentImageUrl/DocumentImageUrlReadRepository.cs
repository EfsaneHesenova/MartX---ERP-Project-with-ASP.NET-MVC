using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models;
using MartX.DAL.Contexts;
using MartX.DAL.Repositories.Abstractions;

namespace MartX.DAL.Repositories.Implementations;

public class DocumentImageUrlReadRepository : ReadRepository<DocumentImageUrl>, IDocumentImageUrlReadRepository
{
    public DocumentImageUrlReadRepository(AppDbContext context) : base(context)
    {
    }
}
