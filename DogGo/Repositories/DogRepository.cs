using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Repositories
{
				public class DogRepository : IDogRepository
				{
								private readonly IConfiguration _config;
								
								public DogRepository(IConfiguration config)
								{
												_config = config;
								}

								public SqlConnection Connection
								{
												get
												{
																return new SqlConnection
																(_config.GetConnectionString
																("DefaultConnection"));
												}
								}

								public void AddDog(Dog dog)
								{
												throw new NotImplementedException();
								}

								public void DeleteDog(int dogId)
								{
												throw new NotImplementedException();
								}

								public List<Dog> GetAllDogs()
								{
												using (SqlConnection conn = Connection)
												{
																conn.Open();
																using (SqlCommand cmd = conn.CreateCommand())
																{
																				cmd.CommandText = @"
                        SELECT Id, [Name], OwnerId, Breed, Notes, ImageUrl
                        FROM Dog
                    ";

																				SqlDataReader reader = cmd.ExecuteReader();

																				List<Dog> dogs = new List<Dog>();
																				while(reader.Read())
																				{
																								Dog dog = new Dog
																								{
																												Id = reader.GetInt32(reader.GetOrdinal("Id")),
																												Name = reader.GetString(reader.GetOrdinal("Name")),
																												OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
																												Breed = reader.GetString((reader.GetOrdinal("Breed")))
																								};

																								if (reader.GetString(reader.GetOrdinal("Notes")) == null)
																								{
																												dog.Notes = "";
																								};

																								if (reader.GetString(reader.GetOrdinal("ImageUrl")) == null)
																								{
																												dog.ImageUrl = "https://i.pinimg.com/originals/07/93/a0/0793a045622b504943b975dd36c41da6.png";
																								}

																								dogs.Add(dog);
																				}

																				reader.Close();

																				return dogs;
																}
												}
								}

								public Dog GetDogById(int id)
								{
												throw new NotImplementedException();
								}

								public void UpdateDog(Dog dog)
								{
												throw new NotImplementedException();
								}
				}
}
