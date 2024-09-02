﻿using Backend.Models;
using Microsoft.EntityFrameworkCore;
namespace PasswordHashingDemo.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
