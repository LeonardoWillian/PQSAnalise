using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubAnalise
{
    public class Usuario
    {
        public string Nome { get; set; }
        public int QtdStars { get; set; }
        public int QtdForks { get; set; }
        public int QtdIssuesResponsavel { get; set; }
        public int QtdPullRequestAceito { get; set; }
    }
}
