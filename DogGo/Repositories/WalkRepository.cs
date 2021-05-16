using DogGo.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DogGo.Repositories
{
				public class WalkRepository : IWalkRepository
				{
								private readonly IConfiguration _config;

								public WalkRepository(IConfiguration config)
								{
												_config = config;
								}

								public SqlConnection Connection
								{
												get
												{
																return new SqlConnection
																(_config.GetConnectionString("DefaultConnection"));
												}
								}
								
								public List<Walk> GetAllWalks()
								{
												using (SqlConnection conn = Connection)
												{
																conn.Open();
																using (SqlCommand cmd = conn.CreateCommand())
																{
																				cmd.CommandText = @"
																								SELECT		Id, 
																																[Date], 
																																Duration,
																																WalkerId,
																																DogId
																								FROM Walks
																				";

																				SqlDataReader reader = cmd.ExecuteReader();

																				List<Walk> walks = new List<Walk>();

																				while (reader.Read())
																				{
																								Walk walk = new Walk
																								{
																												Id = reader.GetInt32(reader.GetOrdinal("Id")),
																												Date = reader.GetDateTime(reader.GetOrdinal("Date")),
																												Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
																												WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
																												DogId = reader.GetInt32(reader.GetOrdinal("DogId"))
																								};

																								walks.Add(walk);
																				}

																				reader.Close();

																				return walks;
																}
												}
								}
				}
}
