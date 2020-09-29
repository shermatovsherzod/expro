using System;
using System.Collections.Generic;
using System.Text;
using Expro.Models;

namespace Expro.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        ApplicationDbContext Get();
    }
}
