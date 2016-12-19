using Microsoft.AspNetCore.Builder;

namespace chumakeveryday.Server
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