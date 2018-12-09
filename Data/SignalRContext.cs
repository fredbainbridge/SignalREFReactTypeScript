using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalRWebPack.Models;
using Microsoft.EntityFrameworkCore;

namespace SignalRWebPack.Data {
    public class SignalRContext : DbContext {
        public SignalRContext(DbContextOptions<SignalRContext> options) : base(options) {}
        public DbSet<SomeData> SomeData {get; set;}
    }  
}