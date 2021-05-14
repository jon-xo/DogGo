﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using DogGo.Repositories;
using DogGo.Models;


namespace DogGo.Controllers
{
				public class DogsController : Controller
				{

								private readonly IDogRepository _dogRepo;

								public DogsController(IDogRepository dogRepository)
								{
												_dogRepo = dogRepository;
								}
								
								// GET: Dogs
								public ActionResult Index()
								{
												List<Dog> dogs = _dogRepo.GetAllDogs();

												return View(dogs);
								}

								// GET: Dogs/Details/5
								public ActionResult Details(int id)
								{
												return View();
								}

								// GET: Dogs/Create
								public ActionResult Create()
								{
												return View();
								}

								// POST: Dogs/Create
								[HttpPost]
								[ValidateAntiForgeryToken]
								public ActionResult Create(Dog dog)
								{
												try
												{
																_dogRepo.AddDog(dog);
																
																return RedirectToAction(nameof(Index));
												}
												catch(Exception ex)
												{
																return View(dog);
												}
								}

								// GET: Dogs/Edit/5
								public ActionResult Edit(int id)
								{
												return View();
								}

								// POST: Dogs/Edit/5
								[HttpPost]
								[ValidateAntiForgeryToken]
								public ActionResult Edit(int id, IFormCollection collection)
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

								// GET: Dogs/Delete/5
								public ActionResult Delete(int id)
								{
												return View();
								}

								// POST: Dogs/Delete/5
								[HttpPost]
								[ValidateAntiForgeryToken]
								public ActionResult Delete(int id, IFormCollection collection)
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