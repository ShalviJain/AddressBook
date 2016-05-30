using AddressBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AddressBook.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddContacts()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddContacts(Contact c)
        {
            if (ModelState.IsValid)
            {
                using (AddressBookEntities dc = new AddressBookEntities())
                {
                    dc.Contacts.Add(c);
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
                return RedirectToAction("AddressBook");
            }
            else
                return RedirectToAction("AddressBook");
            //return View(c);
        }

        public ActionResult AddressBook()
        {
           List<Contact> contacts = new List<Contact>();

           using( AddressBookEntities dc = new AddressBookEntities())
           {

            var v =  from a in dc.Contacts
                        select new
                        {
                            Id = a.Id,
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                            PhoneNumber = a.PhoneNumber,
                            StreetName = a.StreetName,
                            City = a.City,
                            Province = a.Province,
                            PostalCode = a.PostalCode,
                            Country = a.Country
                         };
               
                var data = v.ToList().Select( r => new Contact
                        {
                            Id = r.Id,
                            FirstName = r.FirstName,
                            LastName = r.LastName,
                            PhoneNumber = r.PhoneNumber,
                            StreetName = r.StreetName,
                            City = r.City,
                            Province = r.Province,
                            PostalCode = r.PostalCode,
                            Country = r.Country
                }).ToList();
                
            contacts = data;
            ViewBag.Data = contacts;
            return View();
           }
        }

        //public ActionResult GetSessionIdContacts()
        //{
        //    bool pid = true;

        //    return RedirectToAction("GetSessionIdContacts", "Account", new { pid = true });
        //}

        //public Contact GetContactId(int contactId)
        //{
        //    Contact contact = null;

        //    using (AddressBookEntities dc = new AddressBookEntities())
        //    {
        //        var v = (from a in dc.Contacts
        //                 where a.Id.Equals(contactId)
        //                 select new
        //                 {
        //                     a
        //                 }).FirstOrDefault();
        //        if (v != null)
        //        {
        //            contact = v.a;
        //        }
        //        return contact;
        //    }

        //}

        public ActionResult EditContact(int id)
        {

            List<Contact> contact = new List<Contact>();

            using (AddressBookEntities dc = new AddressBookEntities())
            {
                var v = from a in dc.Contacts
                        where (a.Id == id)
                        select new
                        {
                            Id = a.Id,
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                            PhoneNumber = a.PhoneNumber,
                            StreetName = a.StreetName,
                            City = a.City,
                            Province = a.Province,
                            PostalCode = a.PostalCode,
                            Country = a.Country
                        };

                var data = v.ToList().Select(r => new Contact
                {
                    Id = r.Id,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    PhoneNumber = r.PhoneNumber,
                    StreetName = r.StreetName,
                    City = r.City,
                    Province = r.Province,
                    PostalCode = r.PostalCode,
                    Country = r.Country

                }).ToList();

                contact = data;
                ViewBag.userdetails = contact;
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditContact(Contact c)
        {
            if (ModelState.IsValid)
            {
                using (AddressBookEntities dc = new AddressBookEntities())
                {
                    var v = dc.Contacts.Where(a => a.Id.Equals(c.Id)).FirstOrDefault(); 
                    if (v != null)
                    {
                            v.Id = c.Id;
                            v.FirstName = c.FirstName;
                            v.LastName = c.LastName;
                            v.PhoneNumber = c.PhoneNumber;
                            v.StreetName = c.StreetName;
                            v.City = c.City;
                            v.Province = c.Province;
                            v.PostalCode = c.PostalCode;
                            v.Country = c.Country;
                    }
                    dc.SaveChanges();
                }
                return RedirectToAction("AddressBook");
            }
            else
            {
                List<Contact> contact = new List<Contact>();
                contact.Add(c);
                ViewBag.userdetails = contact;
                return View();
            }
        }

        public ActionResult DeleteContact(int id)
        {
            List<Contact> contact = new List<Contact>();

            using (AddressBookEntities dc = new AddressBookEntities())
            {
                var v = from a in dc.Contacts
                        where (a.Id == id)
                        select new
                        {
                            Id = a.Id,
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                            PhoneNumber = a.PhoneNumber,
                            StreetName = a.StreetName,
                            City = a.City,
                            Province = a.Province,
                            PostalCode = a.PostalCode,
                            Country = a.Country
                        };

                var data = v.ToList().Select(r => new Contact
                {
                    Id = r.Id,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    PhoneNumber = r.PhoneNumber,
                    StreetName = r.StreetName,
                    City = r.City,
                    Province = r.Province,
                    PostalCode = r.PostalCode,
                    Country = r.Country

                }).ToList();

                contact = data;
                ViewBag.userdetails = contact;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteContact(Contact c)
        {
            using (AddressBookEntities dc = new AddressBookEntities())
            {
                var v = dc.Contacts.Where(a => a.Id.Equals(c.Id)).FirstOrDefault();
                if (v != null)
                {
                    dc.Contacts.Remove(v);
                    dc.SaveChanges();
                    return RedirectToAction("AddressBook");
                }
                else
                {
                    return HttpNotFound("contact not found");
                }
            }
        }

        public ActionResult GetSessionId()
        {
            bool pid = true;

            return RedirectToAction("GetSessionId", "Account", new { pid = true });
        }

        public ActionResult MyProfile(int id)
        {
            List<User> user = new List<User>();

            using (AddressBookEntitiesForAccountModel dc = new AddressBookEntitiesForAccountModel())
            {
                var v = from a in dc.Users
                        where(a.Id == id)
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


        public ActionResult EditMyProfile(int id)
        {
            List<UserValidation> user = new List<UserValidation>();

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

                var data = v.ToList().Select(r => new UserValidation
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
        public ActionResult EditMyProfile(UserValidation u)
        {
            if (ModelState.IsValid)
            {
                using (AddressBookEntitiesForAccountModel dc = new AddressBookEntitiesForAccountModel())
                {
                    string username = User.Identity.Name;
                    var v = dc.Users.Where(a => a.Id.Equals(u.Id)).FirstOrDefault(); 
                    if (v != null)
                    {
                        Session["LogedUserFirstName"] = u.FirstName.ToString();
                        v.FirstName = u.FirstName;
                        v.LastName = u.LastName;
                        v.Email = u.Email;
                        v.PasswordHashed = u.PasswordHashed;
                    }
                    dc.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                List<UserValidation> user = new List<UserValidation>();
                user.Add(u);
                ViewBag.userdetails = user;
                return View();
            }
        }
                    
     }
}

