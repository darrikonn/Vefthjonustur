using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Services.Exceptions {
	public class AppObjectNotFoundException : Exception {
	    public AppObjectNotFoundException(){}
	    public AppObjectNotFoundException(string msg) : base(msg) {}
    }
}
