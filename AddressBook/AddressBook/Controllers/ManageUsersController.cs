using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AddressBook.Controllers
{
    public class ManageUsersController : Controller
    {
        //
        // GET: /ManageUsers/

        public ActionResult ViewUsers()
        {
            List<User> user = new List<User>();

            using (AddressBookEntitiesForAccountModel dc = new AddressBookEntitiesForAccountModel())
            {
                var v = from a in dc.Users
                        select new
                        {
                            Id = a.Id,
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                            Email = a.Email,
                            PasswordHashed = a.PasswordHashed
                        };

                var data = v.ToList().Select(r => new User
                        {
                            Id = r.Id,
                            FirstName = r.FirstName,
                            LastName = r.LastName,
                            Email = r.Email,
                            PasswordHashed = r.PasswordHashed
                        }).ToList();

                user = data;
                ViewBag.users = user;
                return View();
            }
        }

        public ActionResult AddNewUsers()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewUsers(User u)
        {
            if (ModelState.IsValid)
            {
                using (AddressBookEntitiesForAccountModel dc = new AddressBookEntitiesForAccountModel())
                {
                    string hashPass = AddressBook.Cyrpto.Hash(u.PasswordHashed);
                    u.PasswordHashed = hashPass;
                    dc.Users.Add(u);
                    try
                    {
                        dc.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                    }

                }
                return RedirectToAction("ViewUsers");
            }
            else
                return View(u);
        }

        public ActionResult EditUsers(int id)
        {
            List<User> user = new List<User>();

            using (AddressBookEntitiesForAccountModel dc = new AddressBookEntitiesForAccountModel())
            {
                var v = from a in dc.Users
                        where (a.Id == id)
                        select new
                        {
                            Id = a.Id,
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                            Email = a.Email,
                            PasswordHashed = a.PasswordHashed
                        };

                var data = v.ToList().Select(r => new User
                {
                    Id = r.Id,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    Email = r.Email,
                    PasswordHashed = r.PasswordHashed

                }).ToList();

                user = data;
                ViewBag.userdetails = user;
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUsers(User u)
        {
            if (ModelState.IsValid)
            {
                using (AddressBookEntitiesForAccountModel dc = new AddressBookEntitiesForAccountModel())
                {
                    var v = dc.Users.Where(a => a.Id.Equals(u.Id)).FirstOrDefault();
                    if (v != null)
                    {
                        v.Id = u.Id;
                        v.FirstName = u.FirstName;
                        v.LastName = u.LastName;
                        v.Email = u.Email;
                        v.PasswordHashed = u.PasswordHashed;
                    }
                    dc.SaveChanges();
                }
                return RedirectToAction("ViewUsers");
            }
            else
            {
                List<User> user = new List<User>();
                user.Add(u);
                ViewBag.userdetails = user;
                return View();
            }
        }

        
    }
}
