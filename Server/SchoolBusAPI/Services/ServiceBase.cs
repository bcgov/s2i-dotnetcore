﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolBusAPI.Authorization;
using SchoolBusAPI.Models;
using System;
using System.Linq;
using System.Security.Claims;

namespace SchoolBusAPI.Services
{
    public abstract class ServiceBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServiceBase(IHttpContextAccessor httpContextAccessor, DbAppContext context, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            DbContext = context;
            Mapper = mapper;
        }

        protected IDbAppContext DbContext { get; private set;  }
        protected HttpRequest Request
        {
            get { return _httpContextAccessor.HttpContext.Request; }
        }

        protected ClaimsPrincipal User
        {
            get { return _httpContextAccessor.HttpContext.User; }
        }

        protected IMapper Mapper { get; private set; }

        /// <summary>
        /// Returns the current user ID
        /// </summary>
        /// <returns></returns>
        protected int? GetCurrentUserId()
        {
            int? result = null;
            
            try
            {
                string rawuid = User.FindFirst(SchoolBusAPI.Models.User.USERID_CLAIM).Value;
                result = int.Parse(rawuid);
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        /// <summary>
        /// Returns the current Site Minder User ID
        /// </summary>
        /// <returns></returns>
        protected string GetCurrentSmUserId()
        {
            string result = null;
            
            try
            {
                result = User.FindFirst(ClaimTypes.Name).Value;                
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }


        protected OkObjectResult Ok(object value)
        {
            return new OkObjectResult(value);
        }


        protected bool CurrentUserHasAllThePermissions(int roleId)
        {
            var permissions = DbContext.RolePermissions
                .AsNoTracking()
                .Where(rp => rp.RoleId == roleId)
                .Select(rp => rp.Permission.Code)
                .ToArray();

            return User.HasPermissions(permissions);
        }
    }
}
