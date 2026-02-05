using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Core.Model
{
    public class Class
    {
        public int Id { get; set; }
        public List<Student> students { get; set; }
        public List<Teacher>teachers { get; set; }
    }
}
