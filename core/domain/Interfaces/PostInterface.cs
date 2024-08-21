using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using core.domain.Entities;

namespace core.domain.Interfaces {
	public interface PostInterface<Entity> {
		Entity post(Entity entity);
		Entity GetById(int id);
		IEnumerable<Entity> GetAll();
		Task<Entity> GetByIdAsync(int id);
    	Task<Entity> PostAsync(Entity entity);
    	Task<IEnumerable<Entity>> GetAllAsync();
    	Task<IEnumerable<Entity>> GetPostsByAuthorsAsync(IEnumerable<string> authorUsernames);
	}
}
