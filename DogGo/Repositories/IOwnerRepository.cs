using DogGo.Models;
using System;
using System.Collections.Generic;

namespace DogGo.Repositories
{
				public interface IOwnerRepository
				{
								List<Owner> GetAllOwners();
								Owner GetOwnerById(int id);
								Owner GetOwnerByEmail(string name);
								//Owner GetOwnerByDog(int dogId);
								void AddOwner(Owner owner);
								void UpdateOwner(Owner owner);
								void DeleteOwner(int ownerId);
				}
}
