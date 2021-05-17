
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using DogGo.Repositories;
using DogGo.Models;
using DogGo.Models.ViewModels;
using System.Security.Claims;

namespace DogGo.Controllers
{
				
				public class WalkersController : Controller
				{

								private readonly IWalkerRepository _walkerRepo;
								private readonly IWalkRepository _walkRepo;
								private readonly IDogRepository _dogRepo;
								private readonly INeighborhoodRepository _neighborRepo;
								private readonly IOwnerRepository _ownerRepo;

								// ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
								public WalkersController(
												IWalkerRepository walkerRepository,
												IWalkRepository walkRepository,
												IDogRepository dogRepository,
												INeighborhoodRepository neighborRepository,
												IOwnerRepository ownerRepository)
								{
												_walkerRepo = walkerRepository;
												_walkRepo = walkRepository;
												_dogRepo = dogRepository;
												_neighborRepo = neighborRepository;
												_ownerRepo = ownerRepository;
								}

								// GET: Walkers
								public ActionResult Index()
								{
												int activeUser = GetCurrentUserId();

												Owner activeOwner = _ownerRepo.GetOwnerById(activeUser);

												if (activeUser != 0)
												{
																List<Walker> walkers = _walkerRepo.GetWalkersInNeighborhood(activeOwner.NeighborhoodId);
																return View(walkers);
												}
												else
												{
																List<Walker> walkers = _walkerRepo.GetAllWalkers();
																return View(walkers);
												}

								}

								// GET: Walkers/Details/5
								public ActionResult Details(int id)
								{
												Walker walker = _walkerRepo.GetWalkerById(id);
												List<Walk> walks = _walkRepo.GetWalksById(id);
												Neighborhood hood = _neighborRepo.GetNeighborhoodById(walker.NeighborhoodId);
												string walkTotal = _walkRepo.WalkTime(walks);
												//int walkId = walk.DogId;
												//Owner owner = _walkRepo.GetOwner(walk.DogId);
												//Owner owner = _ownerRepo.GetOwnerByDog;

												ProfileViewModel vm = new ProfileViewModel()
												{
																Walker = walker,
																Walks = walks,
																Hood = hood,
																WalkTotal = walkTotal
												};


												if (vm.Walker == null)
												{
																return NotFound();
												}

												return View(vm);
								}

								// GET: Walkers/Create
								public ActionResult Create()
								{
												return View();
								}

								// POST: Walkers/Create
								[HttpPost]
								[ValidateAntiForgeryToken]
								public ActionResult Create(Walker walker)
								{
												try
												{
																return RedirectToAction(nameof(Index));
												}
												catch
												{
																return View();
												}
								}

								// GET: Walkers/Edit/5
								public ActionResult Edit(int id)
								{
												return View();
								}

								// POST: Walkers/Edit/5
								[HttpPost]
								[ValidateAntiForgeryToken]
								public ActionResult Edit(int id, Walker walker)
								{
												try
												{
																return RedirectToAction(nameof(Index));
												}
												catch
												{
																return View();
												}
								}

								// GET: Walkers/Delete/5
								public ActionResult Delete(int id)
								{
												return View();
								}

								// POST: Walkers/Delete/5
								[HttpPost]
								[ValidateAntiForgeryToken]
								public ActionResult Delete(int id, Walker walker)
								{
												try
												{
																return RedirectToAction(nameof(Index));
												}
												catch
												{
																return View();
												}
								}
								private int GetCurrentUserId()
								{
												string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
												return int.Parse(id);
								}
				}
}
