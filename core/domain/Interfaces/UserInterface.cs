using System;
using System.Collections.Generic;
using System.Text;

namespace core.domain.Interfaces {
	public interface UserInterface<Entity> {
		Entity post(Entity entity);
		
	}
}
