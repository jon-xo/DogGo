using System;
using System.Collections.Generic;

namespace DogGo.Models.ViewModels
{
				public class ProfileViewModel
				{
								public Owner Owner { get; set; }
								public List<Walker> Walkers { get; set; }
								public List<Dog> Dogs { get; set; }
								public Walker Walker { get; set; }
								public Neighborhood Hood { get; set; }
								public List<Walk> Walks { get; set; }
								public string WalkTotal { get; set; }
				}
}
