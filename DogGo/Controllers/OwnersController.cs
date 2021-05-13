using Microsoft.AspNetCore.Mvc;
using System;
using DogGo.Repositories;
using DogGo.Models;
using System.Collections.Generic;


namespace DogGo.Controllers
{
				public class OwnersController : Controller
				{
								private readonly IOwnerRepository _ownerRepo;

								public OwnersController(IOwnerRepository ownerRepository)
								{
												_ownerRepo = ownerRepository;
								}

								// GET: Owners
								public ActionResult Index()
								{
												List<Owner> owners = _ownerRepo.GetAllOwners();

												return View(owners);
								}

								// GET: Owners/Details/5
								public ActionResult Details(int id)
								{
												return View();
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
