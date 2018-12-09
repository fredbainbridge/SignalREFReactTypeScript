using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalRWebPack.Models;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SignalRWebPack.Data {
    public class DbInitializer {
        public static void Initialize(SignalRContext context) {
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}