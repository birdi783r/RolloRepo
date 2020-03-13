using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RolloSteuerung.Web.Application.Data
{
    public class RolloSteuerungDbContext : IdentityDbContext
    {
        public RolloSteuerungDbContext(DbContextOptions<RolloSteuerungDbContext> options)
            : base(options)
        {
        }
    }
}
