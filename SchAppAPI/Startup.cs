using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchAppAPI.Contexts;
using SchAppAPI.Models;
using SchAppAPI.Repository;
using SchAppAPI.Services;
using SchAppAPI.Services.Hubs;
using SchAppAPI.Settings;

namespace SchAppAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            GoogleCredential googleCredential = GoogleCredential.FromFile("ihsapi.json").CreateScoped("https://www.googleapis.com/auth/firebase.messaging");
            FirebaseApp.Create(new AppOptions() { Credential = googleCredential });

            services.AddControllers();

            //entity framework services
            services.AddDbContext<SchoolDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            //inject dependencies
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<IClassRepository, ClassRepository>();
            services.AddTransient<IContentRepository, ContentRepository>();
            services.AddTransient<ILessonRepository, LessonRepository>();
            services.AddTransient<IQuizRepository, QuizRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IQuizReportRepository, QuizReportRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<ILessonReportRepository, LessonReportRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<IGalleryRepository, GalleryRepository>();
            services.AddTransient<IChatRepository, ChatRepository>();

            services.AddTransient<IMobileMessagingClient, MobileMessagingClient>();
            services.AddSingleton<IMediaService, MediaService>();
            services.AddTransient<IEmailService, EmailService>();

            services.Configure<SMTPConfigModel>(Configuration.GetSection("SMTPConfig"));
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinaryConfig"));
            //newtonsoft json

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddAutoMapper(typeof(Startup));

            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<SchoolDbContext>()
               .AddDefaultTokenProviders();

            //Add JWT Auth
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/chathub")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SchAppAPI", Version = "v1" });
            });

            services.AddSingleton<IUserIdProvider, ClaimNameBasedUserIdProvider>();

            services.AddSignalR();


            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //CROSS ORIGIN REQUEST SHARING
            app.UseCors(options =>
                options
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()

            );
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchAppAPI v1"));

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chathub");

            });

        }
    }
}
