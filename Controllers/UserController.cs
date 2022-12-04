using ChristmasLottery.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ChristmasLottery.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public UserController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<UserData> Get()
        {
            var result = _context.Users.Select(u => new UserData { 
                Id = u.Id,
                GiftForUserId = u.GiftForUserId,
                GiftForUser = new UserData
                {
                    Wants = u.GiftForUser.Wants,
                    UserName = u.GiftForUser.UserName
                },
                Picture = u.Picture,
                UserName = u.UserName,
                Wants = u.Wants
            }).ToList();
            return result;
        }

        [HttpGet]
        [Route("{id}")]
        public UserData GetSingle(int id)
        {
            var result = _context.Users.Include(u => u.GiftForUser).FirstOrDefault(u => u.Id == id);
            result.GiftForUser.GiftForUser = null;
            return result;
        }

        [HttpPost]
        public void Post(UserData user)
        {
            var userFromDb = _context.Users.Find(user.Id);
            userFromDb.Wants = user.Wants;
            _context.SaveChanges();
        }

        [HttpPost]
        public void FillDb([FromBody] FillDbCommandModel fillDbCommand)
        {
            if (fillDbCommand.Password != "saicxs")
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }

            foreach (var user in _context.Users.ToList())
            {
                user.GiftForUserId = null;
            }

            _context.SaveChanges();

            foreach (var user in _context.Users.ToList())
            {
                _context.Users.Remove(user);
            }

            var usersNames = fillDbCommand.Users; 
            if (usersNames == null || usersNames.Count == 0) {
                usersNames = new List<string>(){ "Agata Z", "Jarek Z", "Kamil Z", "Basia Z", "Piotr B", "Małgorzata B", "Maciej B", "Bartek B", "Julek B", "Barbara B", "Jerzy B", "Marcin Z", "Małgorzata Z", "Teresa Z", "Zofia Z", "Szymon Z", "Filip Z", "Helena Z" };
            }
            var users = new List<UserData>();
            foreach (var user in usersNames)
            {
                var newUser = new UserData
                {
                    UserName = user
                };
                users.Add(newUser);
            }
            _context.Users.AddRange(users);
            _context.SaveChanges();

            while (true)
            {
                Random rng = new Random();
                var usersCopy = users.OrderBy(a => rng.Next()).ToList();
                foreach (var user in users)
                {
                    user.GiftForUser = usersCopy[0];
                    if (user.UserName == user.GiftForUser.UserName)
                    {
                        break;
                    }
                    usersCopy.RemoveAt(0);
                }
                if (usersCopy.Count == 0) break;
            }

            _context.SaveChanges();
        }
    }
}

public class FillDbCommandModel
{
    public string Password { get; set; }
    public List<string> Users { get; set; }
}
