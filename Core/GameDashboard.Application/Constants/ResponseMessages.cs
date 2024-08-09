using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDashboard.Application.Constants
{
    public static class ResponseMessages
    {
        public static readonly string UserNotFound = "User not found.";
        public static readonly string UsernameOrPasswordInvalid = "Username or password is invalid.";
        public static readonly string TokenNotFound = "Token not found.";
        public static readonly string UserExist = "User already exists.";
        public static readonly string UserCreated = "User created.";
        public static readonly string BuildingTypeNotFound = "Building type not found.";

        public static readonly string BuildingAdded = "Building added.";
        public static readonly string BuildingTypeExist = "Building type already exists.";
    }
}
