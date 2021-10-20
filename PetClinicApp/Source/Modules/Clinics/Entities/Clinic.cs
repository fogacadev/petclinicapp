﻿using PetClinicApp.Source.Modules.Clinics.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Modules.Clinics.Entities
{
    public class Clinic
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Descripton { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }

        public List<ClinicService> Services { get; set; }
    }

    public static class ClinicExtensions
    {
        public static Clinic ToEntity(this ClinicDTO clinic)
        {
            return new Clinic
            {
                Id = clinic.Id,
                Name = clinic.Name,
                Descripton = clinic.Descripton,
                PhoneNumber = clinic.PhoneNumber,
                Address = clinic.Address,
                City = clinic.City,
                ZipCode = clinic.ZipCode,
                State = clinic.State,
            };
        }

        public static ClinicDTO ToDTO(this Clinic clinic)
        {
            return new ClinicDTO
            {
                Id = clinic.Id,
                Name = clinic.Name,
                Descripton = clinic.Descripton,
                PhoneNumber = clinic.PhoneNumber,
                Address = clinic.Address,
                City = clinic.City,
                ZipCode = clinic.ZipCode,
                State = clinic.State,
            };
        }

        public static Clinic ToEntity(this CreateClinicDTO clinic)
        {
            return new Clinic
            {
                Name = clinic.Name,
                Descripton = clinic.Descripton,
                PhoneNumber = clinic.PhoneNumber,
                Address = clinic.Address,
                City = clinic.City,
                ZipCode = clinic.ZipCode,
                State = clinic.State,
            };
        }

        public static Clinic ToEntity(this UpdateClinicDTO clinic)
        {
            return new Clinic
            {
                Id = clinic.Id,
                Name = clinic.Name,
                Descripton = clinic.Descripton,
                PhoneNumber = clinic.PhoneNumber,
                Address = clinic.Address,
                City = clinic.City,
                ZipCode = clinic.ZipCode,
                State = clinic.State,
            };
        }

        public static IQueryable<Clinic> Filter(this IQueryable<Clinic> query, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToUpper();

                query = query.Where(c => c.Name.ToUpper().Contains(search) || c.Descripton.ToUpper().Contains(search));
            }

            return query;
        }
    }
}
