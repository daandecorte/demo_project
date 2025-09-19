using Demo.Application.Interfaces;
using Demo.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.UoW
{
    public class UnitofWork : IUnitofWork
    {
        private readonly DemoContext ctxt;

        public UnitofWork(DemoContext ctxt)
        {
            this.ctxt = ctxt;
        }

        public async Task Commit()
        {
            await ctxt.SaveChangesAsync();
        }
    }
}
