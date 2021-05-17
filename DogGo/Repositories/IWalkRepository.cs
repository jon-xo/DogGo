using DogGo.Models;
using System.Collections.Generic;

namespace DogGo.Repositories
{
				public interface IWalkRepository
				{
								List<Walk> GetAllWalks();
								List<Walk> GetWalksById(int id);
								string WalkTime(List<Walk> walks);
				}
}
