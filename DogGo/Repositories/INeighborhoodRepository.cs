using DogGo.Models;
using System;
using System.Collections.Generic;

namespace DogGo.Repositories
{
				public interface INeighborhoodRepository
				{
								List<Neighborhood> GetAll();
								Neighborhood GetNeighborhoodById(int id);
				}
}
