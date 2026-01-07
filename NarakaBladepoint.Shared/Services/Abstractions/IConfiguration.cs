using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarakaBladepoint.Shared.Services.Abstractions
{
    public interface IConfiguration
    {
        Task<bool> Save<T>(T entity);
        Task<bool> SaveAll<T>(IEnumerable<T> entities);
    }
}
