using System;
using System.Collections.Generic;
using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DogGo.Repositories
{
				public class NeighborhoodRepository : INeighborhoodRepository
				{
								private readonly IConfiguration _config;

								public NeighborhoodRepository(IConfiguration config)
								{
												_config = config;
								}

								public SqlConnection Connection
								{ 
												get
												{
																return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
												}
								}

								public List<Neighborhood> GetAll()
								{
												using (SqlConnection conn = Connection)
												{
																conn.Open();
																using (SqlCommand cmd = conn.CreateCommand())
																{
																				cmd.CommandText = @"SELECT Id, [Name] FROM Neighborhood";

																				SqlDataReader reader = cmd.ExecuteReader();

																				List<Neighborhood> neighborhoods = new List<Neighborhood>();

																				while (reader.Read())
																				{
																								Neighborhood neighborhood = new Neighborhood()
																								{
																												Id = reader.GetInt32(reader.GetOrdinal("Id")),
																												Name = reader.GetString(reader.GetOrdinal("Name"))
																								};
																								neighborhoods.Add(neighborhood);
																				}
																				reader.Close();

																				return neighborhoods;
																}
												}
								}

								public Neighborhood GetNeighborhoodById(int id)
								{
												using (SqlConnection conn = Connection)
												{
																conn.Open();
																using(SqlCommand cmd = conn.CreateCommand())
																{
																				cmd.CommandText = @"
																								SELECT Id, [Name]
																								FROM Neighborhood
																								WHERE Id = @id
																				";

																				cmd.Parameters.AddWithValue("@id", id);

																				SqlDataReader reader = cmd.ExecuteReader();

																				if (reader.Read())
																				{
																								Neighborhood hood = new Neighborhood()
																								{
																												Id = reader.GetInt32(reader.GetOrdinal("Id")),
																												Name = reader.GetString(reader.GetOrdinal("Name"))
																								};

																								reader.Close();
																								return hood;
																				}

																				reader.Close();
																				return null;
																}
												}
								}
				}
}
