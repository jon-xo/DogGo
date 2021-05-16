
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using DogGo.Repositories;
using DogGo.Models;
using DogGo.Models.ViewModels;

namespace DogGo.Controllers
{
				
				public class WalkersController : Controller
				{

								private readonly IWalkerRepository _walkerRepo;
								private readonly IWalkRepository _walkRepo;
								private readonly IDogRepository _dogRepo;
								private readonly INeighborhoodRepository _neighborRepo;

								// ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
								public WalkersController(
												IWalkerRepository walkerRepository,
												IWalkRepository walkRepository,
												IDogRepository dogRepository,
												INeighborhoodRepository neighborRepository)
								{
												_walkerRepo = walkerRepository;
												_walkRepo = walkRepository;
												_dogRepo = dogRepository;
												_neighborRepo = neighborRepository;
								}

								// GET: Walkers
								public ActionResult Index()
								{
												List<Walker> walkers = _walkerRepo.GetAllWalkers();

												return View(walkers);
								}

								// GET: Walkers/Details/5
				public ActionResult Details(int id)
								{
												Walker walker = _walkerRepo.GetWalkerById(id);
												List<Walk> walks = _walkRepo.GetAllWalks();
												Neighborhood hood = _neighborRepo.GetNeighborhoodById(walker.NeighborhoodId);

												ProfileViewModel vm = new ProfileViewModel()
												{
																Walker = walker,
																Walks = walks,
																Hood = hood
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
				}
}
