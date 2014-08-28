using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterLikeApp.Models;
namespace TwitterLikeApp.Data
{
    public interface IUowData:IDisposable
    {
        IRepository<Message> Messages { get; }

        IRepository<Tag> Tags { get; }

        IRepository<ApplicationUser> Users { get; }

        int SaveChanges();
    }
}
