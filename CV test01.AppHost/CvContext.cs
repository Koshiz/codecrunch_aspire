using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_test01.AppHost
{
    public class CvContext : DbContext
    {
        public CvContext(DbContextOptions<CvContext> options) : base(options) { }

        public DbSet<CVModel> CVs { get; set; }
    }
}
