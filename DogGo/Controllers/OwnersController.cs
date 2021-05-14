
using Microsoft.AspNetCore.Mvc;
using System;
using DogGo.Repositories;
using DogGo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using DogGo.Models.ViewModels;

namespace DogGo.Controllers
{
				public class OwnersController : Controller
				{
								private readonly IOwnerRepository _ownerRepo;
								private readonly IDogRepository _dogRepo;
								private readonly IWalkerRepository _walkerRepo;

								//public OwnersController(IOwnerRepository ownerRepository)
								//{
								//				_ownerRepo = ownerRepository;
								//}

								public OwnersController(
												IOwnerRepository ownerRepository,
												IDogRepository dogRepository,
												IWalkerRepository walkerRepository)
								{
												_ownerRepo = ownerRepository;
												_dogRepo = dogRepository;
												_walkerRepo = walkerRepository;
								}

								// GET: Login
								//public ActionResult Login()
								//{
								//				return View();
								//}

								//[HttpPost]
								//public async Task<ActionResult> Login(LoginViewModel viewModel)
								//{
								//				Owner owner = _ownerRepo.GetOwnerByEmail(viewModel.Email);

								//				if (owner == null)
								//				{
								//								return Unauthorized();
								//				}

								//				var claims = new List<Claim>
								//				{
								//								new Claim(ClaimTypes.NameIdentifier, owner.Id.ToString()),
								//								new Claim(ClaimTypes.Email, owner.Email),
								//								new Claim(ClaimTypes.Role, "DogOwner"),
								//				};

								//				var claimsIdentity = new ClaimsIdentity(
								//								claims, CookieAuthenticationDefaults.AuthenticationScheme);

								//				await HttpContext.SignInAsync(
								//								CookieAuthenticationDefaults.AuthenticationScheme,
								//								new ClaimsPrincipal(claimsIdentity));

								//				return RedirectToAction("Index", "Dogs");
								//}

								// GET: Owners
								public ActionResult Index()
								{
												List<Owner> owners = _ownerRepo.GetAllOwners();

												return View(owners);
								}

								// GET: Owners/Details/5
								public ActionResult Details(int id)
								{
												Owner owner = _ownerRepo.GetOwnerById(id);
												List<Dog> dogs = _dogRepo.GetDogsByOwnerId(owner.Id);
												List<Walker> walkers = _walkerRepo.GetWalkersInNeighborhood(owner.NeighborhoodId);

												ProfileViewModel vm = new ProfileViewModel()
												{
																Owner = owner,
																Dogs = dogs,
																Walkers = walkers
												};

												return View(vm);
								}

								// GET: Owners/Create
								public ActionResult Create()
								{
												return View();
								}

								// POST: Owners/Create
								[HttpPost]
								[ValidateAntiForgeryToken]
								public ActionResult Create(Owner owner)
								{
												try
												{
																//owner.NeighborhoodId = 1;
																_ownerRepo.AddOwner(owner);

																return RedirectToAction(nameof(Index));
												}
												catch(Exception ex)
												{
																return View(owner);
												}
								}

								// GET: Owners/Edit/5
								public ActionResult Edit(int id)
								{
												Owner owner = _ownerRepo.GetOwnerById(id);

												if (owner == null)
												{
																return NotFound();
												}

												return View(owner);
								}

								// POST: Owners/Edit/5
								[HttpPost]
								[ValidateAntiForgeryToken]
								public ActionResult Edit(int id, Owner owner)
								{
												try
												{
																_ownerRepo.UpdateOwner(owner);

																return RedirectToAction("Index");
												}
												catch (Exception ex)
												{
																return View(owner);
												}
								}

								// GET: Owners/Delete/5
								public ActionResult Delete(int id)
								{
												Owner owner = _ownerRepo.GetOwnerById(id);

												return View(owner);
								}

								// POST: Owners/Delete/5
								[HttpPost]
								[ValidateAntiForgeryToken]
								public ActionResult Delete(int id, Owner owner)
								{
												try
												{
																_ownerRepo.DeleteOwner(id);
																
																return RedirectToAction("Index");
												}
												catch (Exception ex)
												{
																Console.WriteLine(ex);
																return View(owner);
												}
								}
				}
}
