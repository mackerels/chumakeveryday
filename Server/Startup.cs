using System;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using CoreSandbox.Factory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CoreSandbox.Server
{
    public class Startup
    {
        private readonly CustomRouter _router;

        public Startup()
        {
            _router = new CustomRouter();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.Run(_router.Handle);
        }
    }
}