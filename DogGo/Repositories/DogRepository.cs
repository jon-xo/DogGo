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
												using (SqlConnection conn = Connection)
												{
																// Initiate Sql connection
																conn.Open();

																// Create Sql Query
																using (SqlCommand cmd = conn.CreateCommand())
																{
																				cmd.CommandText = @"
                        INSERT INTO Dog ([Name], OwnerId, Breed, Notes, ImageUrl)
																								OUTPUT INSERTED.ID
																								VALUES (@name, @ownerId, @breed, @notes, @imageUrl);
                 ";

																				cmd.Parameters.AddWithValue("@name", dog.Name);
																				cmd.Parameters.AddWithValue("@ownerId",				dog.OwnerId);
																				cmd.Parameters.AddWithValue("@breed", dog.Breed);
																				cmd.Parameters.AddWithValue("@notes", dog.Notes);
																				cmd.Parameters.AddWithValue("@imageUrl", dog.ImageUrl);

																				int id = (int)cmd.ExecuteScalar();

																				dog.Id = id;

																}
												}
								}

								public void DeleteDog(int dogId)
								{
												throw new NotImplementedException();
								}

								public List<Dog> GetAllDogs()
								{
												using (SqlConnection conn = Connection)
												{
																// Initiate Sql connection
																conn.Open();

																// Create Sql Query
																using (SqlCommand cmd = conn.CreateCommand())
																{
																				cmd.CommandText = @"
                        SELECT Id, [Name], OwnerId, Breed, Notes, ImageUrl
                        FROM Dog
                    ";

																				// Declare variable to store reader command
																				SqlDataReader reader = cmd.ExecuteReader();

																				// Instantiate a list of dog objects
																				List<Dog> dogs = new List<Dog>();

																				//Create a while loop to iterate through all database rows
																				while(reader.Read())
																				{
																								// Instantiate a new dog object
																								Dog dog = new Dog
																								{
																												Id = reader.GetInt32(reader.GetOrdinal("Id")),
																												Name = reader.GetString(reader.GetOrdinal("Name")),
																												OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
																												Breed = reader.GetString((reader.GetOrdinal("Breed")))
																								};
																								
																								// -- null database handling --

																								// seperate conditional detects for Null value
																								// and writes string to object's Notes/ImageUrl properties
																								// if null exists, ELSE writes DB value to property
																								
																								if (reader.GetValue(reader.GetOrdinal("Notes")) == DBNull.Value)
																								{
																												dog.Notes = "No Notes";
																								}
																								else
																								{
																												dog.Notes = reader.GetString(reader.GetOrdinal("Notes"));
																								}
;

																								if (reader.GetValue(reader.GetOrdinal("ImageUrl")) == DBNull.Value)
																								{
																												dog.ImageUrl = "https://i.pinimg.com/originals/07/93/a0/0793a045622b504943b975dd36c41da6.png";
																								}
																								else
																								{
																												dog.ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl"));
																								};

																								dogs.Add(dog);
																				}

																				reader.Close();

																				return dogs;
																}
												}
								}

								public Dog GetDogById(int id)
								{
												using (SqlConnection conn = Connection)
												{
																conn.Open();

																using (SqlCommand cmd = conn.CreateCommand())
																{
																				cmd.CommandText = @"
																								SELECT Id, [Name], OwnerId, Breed, Notes, ImageURL
																								FROM Dog
																								WHERE Id = @id
																				";

																				cmd.Parameters.AddWithValue("@id", id);

																				SqlDataReader reader = cmd.ExecuteReader();

																				if (reader.Read())
																				{
																								// Instantiate a new dog object
																								Dog dog = new Dog()
																								{
																												Id = reader.GetInt32(reader.GetOrdinal("Id")),
																												Name = reader.GetString(reader.GetOrdinal("Name")),
																												OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
																												Breed = reader.GetString((reader.GetOrdinal("Breed")))
																								};

																								// -- null database handling --

																								// seperate conditional detects for Null value
																								// and writes string to object's Notes/ImageUrl properties
																								// if null exists, ELSE writes DB value to property

																								if (reader.GetValue(reader.GetOrdinal("Notes")) == DBNull.Value)
																								{
																												dog.Notes = "No Notes";
																								}
																								else
																								{
																												dog.Notes = reader.GetString(reader.GetOrdinal("Notes"));
																								}
;

																								if (reader.GetValue(reader.GetOrdinal("ImageUrl")) == DBNull.Value)
																								{
																												dog.ImageUrl = "https://i.pinimg.com/originals/07/93/a0/0793a045622b504943b975dd36c41da6.png";
																								}
																								else
																								{
																												dog.ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl"));
																								};

																								reader.Close();
																								return dog;
																				}

																				reader.Close();
																				return null;
																}
																
												}
								}

								public void UpdateDog(Dog dog)
								{
												using (SqlConnection conn = Connection)
												{
																conn.Open();

																using (SqlCommand cmd = conn.CreateCommand())
																{
																				cmd.CommandText = @"
																								UPDATE Dog
																								SET
																												[Name] = @name,
																												Breed = @breed,
																												Notes = @notes,
																												ImageUrl = @imageUrl,
																												OwnerId = @ownerId
																								WHERE Id = @id
																				";

																				cmd.Parameters.AddWithValue("@name", dog.Name);
																				cmd.Parameters.AddWithValue("@breed", dog.Breed);
																				cmd.Parameters.AddWithValue("@notes", dog.Notes);
																				cmd.Parameters.AddWithValue("@imageUrl", dog.ImageUrl);
																				cmd.Parameters.AddWithValue("@OwnerId", dog.OwnerId);
																				cmd.Parameters.AddWithValue("@id", dog.Id);

																				cmd.ExecuteNonQuery();
																}
												}
								}
				}
}
