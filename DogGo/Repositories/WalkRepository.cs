using DogGo.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

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

								public List<Walk> GetWalksById(int walkerId)
								{
												using (SqlConnection conn = Connection)
												{
																conn.Open();
																using (SqlCommand cmd = conn.CreateCommand())
																{
																				cmd.CommandText = @"
																								SELECT		wk.Id, 
																																wk.[Date], 
																																wk.Duration,
																																wk.WalkerId,
																																wk.DogId,
																																ow.Name as Owner
																								FROM Walks wk
																								JOIN Dog d on wk.DogId = d.id
																								JOIN Owner ow on d.OwnerId = ow.Id
																								WHERE wk.WalkerId = @id;
																				";
																				cmd.Parameters.AddWithValue("@id", walkerId);

																				SqlDataReader reader = cmd.ExecuteReader();

																				List<Walk> walks = new List<Walk>();

																				while (reader.Read())
																				{
																								DateTime orginalDate = reader.GetDateTime(reader.GetOrdinal("Date"));
																								string shortDate = orginalDate.ToShortDateString();

																								int shortDuration = reader.GetInt32(reader.GetOrdinal("Duration"));
																								//TimeSpan ts = TimeSpan.FromSeconds(shortDuration);
																								//double finalDuration = ts.TotalMinutes;
																								double shortSpan = shortDuration / 60;
																								int finalDuration = Convert.ToInt32(Math.Round(shortSpan, 0));

																								Walk walk = new Walk
																								{
																												Id = reader.GetInt32(reader.GetOrdinal("Id")),
																												Date = shortDate,
																												Duration = finalDuration,
																												WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
																												DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
																												Owner = reader.GetString(reader.GetOrdinal("Owner"))
																								};

																								walks.Add(walk);
																				}


																				reader.Close();

																				return walks.OrderByDescending(w => w.Owner).ToList();
																}
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
																								SELECT		wk.Id, 
																																wk.[Date], 
																																wk.Duration,
																																wk.WalkerId,
																																wk.DogId,
																																ow.Name as Owner
																								FROM Walks wk
																								JOIN Dog d on wk.DogId = d.id
																								JOIN Owner ow on d.OwnerId = ow.Id
																				";

																				SqlDataReader reader = cmd.ExecuteReader();

																				List<Walk> walks = new List<Walk>();

																				while (reader.Read())
																				{
																								DateTime orginalDate = reader.GetDateTime(reader.GetOrdinal("Date"));
																								string shortDate = orginalDate.ToShortDateString();

																								int shortDuration = reader.GetInt32(reader.GetOrdinal("Duration"));
																								//TimeSpan ts = TimeSpan.FromSeconds(shortDuration);
																								//double finalDuration = ts.TotalMinutes;
																								double shortSpan = shortDuration / 60;
																								int finalDuration = Convert.ToInt32(Math.Round(shortSpan, 0));

																								Walk walk = new Walk
																								{
																												Id = reader.GetInt32(reader.GetOrdinal("Id")),
																												Date = shortDate,
																												Duration = finalDuration,
																												WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
																												DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
																												Owner = reader.GetString(reader.GetOrdinal("Owner"))
																								};

																								walks.Add(walk);
																				}


																				reader.Close();																				

																				return walks.OrderByDescending(w => w.Owner).ToList();
																}
												}
								}
								
								public string WalkTime(List<Walk> walkList)
								{
												int TotalTime = walkList.Sum(w => w.Duration);
												TimeSpan t = TimeSpan.FromMinutes(TotalTime);

												string AllWalks = string.Format("{0}hr {1}min",
												t.Hours,
												t.Minutes);

												return AllWalks;
								}
				}
}
