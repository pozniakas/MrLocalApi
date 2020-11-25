using System;
using System.Threading.Tasks;

namespace MrLocal_Backend.Repositories.Interfaces
{
    interface IRepository
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Task<string> Delete(string id);
    }
}
