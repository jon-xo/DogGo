using DogGo.Models;
using System;
using System.Collections.Generic;

namespace DogGo.Repositories
{
				public interface IOwnerRepository
				{
								List<Owner> GetAllOwners();
								Owner GetOwnerById(int id);
								Owner AddOwner(Owner owner);
								Owner UpdateOwner(Owner owner);
								Owner DeleteOwner(Owner owner);
				}
}
