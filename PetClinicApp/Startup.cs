using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PetClinicApp.Source.Infra;
using PetClinicApp.Source.Modules.Accounts.Repositories;
using PetClinicApp.Source.Modules.Accounts.Repositories.Implementations;
using PetClinicApp.Source.Modules.Accounts.Services;
using PetClinicApp.Source.Modules.Clinics.Repositories;
using PetClinicApp.Source.Modules.Clinics.Repositories.Implementations;
using PetClinicApp.Source.Modules.Clinics.Services;
using PetClinicApp.Source.Modules.MedicalHistories.Repositories;
using PetClinicApp.Source.Modules.MedicalHistories.Repositories.Implementations;
using PetClinicApp.Source.Modules.MedicalHistories.Services;
using PetClinicApp.Source.Modules.Pets.Repositories;
using PetClinicApp.Source.Modules.Pets.Repositories.Implementations;
using PetClinicApp.Source.Modules.Pets.Services;
using PetClinicApp.Source.Modules.Reminders.Repositories;
using PetClinicApp.Source.Modules.Reminders.Repositories.Implementations;
using PetClinicApp.Source.Modules.Reminders.Services;
using PetClinicApp.Source.Shared.Middlewares;
using PetClinicApp.Source.Shared.Settings;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PetClinicApp
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
            services.AddCors();
            services.AddControllers();
            services.AddApiVersioning();

            services.AddDbContext<AppDbContext>(options => {
                var connectionString = Configuration.GetConnectionString("Default");
                options.UseMySql(connectionString);
            });

            var key = Encoding.ASCII.GetBytes(Setting.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            ConfigureAppRepositories(services);
            ConfigureAppService(services);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });

            //Configuração do swagger 
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                Scheme = "bearer",
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Description = "Insira abaixo apenas o token JWT",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            services.AddSwaggerGen(c =>
            {
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo))
                    {
                        return false;
                    }

                    IEnumerable<ApiVersion> versions = methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(a => a.Versions);

                    return versions.Any(v => $"v{v.ToString()}" == docName);
                });

                c.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Title = "PetClinicApp",
                    Description = "Aplicativo de gerenciamento de Pets",
                    Contact = new OpenApiContact
                    {
                        Email = "210937@aluno.riobrancofac.edu.br",
                        Name = "Felipe Fogaça"
                    },
                    Version = "v1.0"
                });

                var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlfile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    { jwtSecurityScheme, Array.Empty<string>() }
                 });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x =>
            x.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware(typeof(AppErrorMiddleware));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseApiVersioning();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "V1 Docs");
            });
        }

        public void ConfigureAppService(IServiceCollection services)
        {
            //accounts
            services.AddTransient<CreateSessionService>();
            services.AddTransient<GenerateTokenService>();
            services.AddTransient<GenerateRandomTokenService>();
            services.AddTransient<RefreshTokenService>();
            services.AddTransient<RegisterUserService>();
            services.AddTransient<UpdatePasswordService>();
            services.AddTransient<UpdateUserService>();
            services.AddTransient<UploadAccountAvatarService>();
            services.AddTransient<DownloadAccountAvatarService>();
            services.AddTransient<FindLoggedUserService>();

            //Clinics
            services.AddTransient<CreateClinicService>();
            services.AddTransient<CreateClinicServiceSerivice>();
            services.AddTransient<DeleteClinicService>();
            services.AddTransient<DeleteClinicServiceService>();
            services.AddTransient<FindClinicService>();
            services.AddTransient<FindClinicServicesService>();
            services.AddTransient<ListClinicServicesUseCase>();
            services.AddTransient<ListClinicsService>();
            services.AddTransient<UpdateClinicService>();
            services.AddTransient<UpdateClinicServiceService>();
            services.AddTransient<UploadClinicAvatarService>();
            services.AddTransient<DownloadClinicAvatarService>();


            //pets
            services.AddTransient<CreateAnimalService>();
            services.AddTransient<CreatePetService>();
            services.AddTransient<DeleteAnimalService>();
            services.AddTransient<DeletePetService>();
            services.AddTransient<FindAnimalService>();
            services.AddTransient<FindPetService>();
            services.AddTransient<ListAnimalsService>();
            services.AddTransient<ListPetsService>();
            services.AddTransient<UpdateAnimalService>();
            services.AddTransient<UpdatePetService>();
            services.AddTransient<UploadAvatarService>();
            services.AddTransient<DownloadAvatarService>();
            //reminders
            services.AddTransient<CreateReminderService>();
            services.AddTransient<DeleteReminderService>();
            services.AddTransient<FindReminderService>();
            services.AddTransient<FinishReminderService>();
            services.AddTransient<ListRemindersService>();
            services.AddTransient<UpdateReminderService>();
            services.AddTransient<ListReminderByUserService>();

            //reminder types
            services.AddTransient<CreateReminderTypeService>();
            services.AddTransient<DeleteReminderTypeService>();
            services.AddTransient<FindReminderTypeService>();
            services.AddTransient<ListReminderTypesService>();
            services.AddTransient<UpdateReminderTypeService>();

            //medical history
            services.AddTransient<CreateMedicalHistoryService>();
            services.AddTransient<UpdateMedicalHistoryService>();
            services.AddTransient<FindMedicalHistoryService>();
            services.AddTransient<DeleteMedicalHistoryService>();
            services.AddTransient<ListMedicalHistoriesService>();
            services.AddTransient<UploadHistoryAttachmentService>();
            services.AddTransient<DownloadHistoryAttachmentService>();
            services.AddTransient<DeleteHistoryAttachmentService>();

            //medical history type
            services.AddTransient<ListMedicalHistoryTypeService>();
            services.AddTransient<FindMedicalHistoryTypeService>();
        }

        public void ConfigureAppRepositories(IServiceCollection services ) {
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();
            services.AddScoped<IClinicsRepository, ClinicsRepository>();
            services.AddScoped<IClinicServicesRepository, ClinicServicesRepository>();
            services.AddScoped<IAnimalsRepository, AnimalsRepository>();
            services.AddScoped<IPetsRepository, PetsRepository>();
            services.AddScoped<IRemindersRepository, RemindersRepository>();
            services.AddScoped<IReminderTypesRepository, ReminderTypesRepository>();
            services.AddScoped<IMedicalHistoryTypesRepository, MedicalHistoryTypesRepository>();
            services.AddScoped<IMedicalHistoriesRepository, MedicalHistoriesRepository>();
        }

        
    }
}
