using System;
using System.Collections.Generic;
using System.Text;

namespace Expro.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
