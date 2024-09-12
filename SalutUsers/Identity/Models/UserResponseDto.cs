using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class UserResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Invitation? UserInvitation { get; set; }
    }

}
