using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubAnalise
{
    class Program
    {
        static void Main(string[] args)
        {

            var github = new GitHubClient(new ProductHeaderValue("Analise"));
            Console.WriteLine("Digite seu user no GitHub(para ter mais requisições):");
            var user = Console.ReadLine();
            Console.WriteLine("Digite sua senha do GitHub:");
            var password = Console.ReadLine();
            var basicAuth = new Credentials(user, password);
            github.Credentials = basicAuth;
            
            List<string> users = new List<string>
            {
                "Tyriar",
                "isidorn",
                "rebornix",
                "sandy081",
                "bpasero",
                "aeschli",
                "jrieken",
                "ramya-rao-a",
                "joaomoreno",
                "egamma",
            };
            
            GetRepositoriesStatistics(github, users);
            
            Console.ReadKey();
        }

        public static async void GetRepositoriesStatistics(GitHubClient github, List<string> users)
        {
            List<Usuario> usuarios = new List<Usuario>();
            var vscode = github.Repository.Get("Microsoft", "vscode");
            var commitsVSCode = await github.Repository.Commit.GetAll(vscode.Result.Id);
            var issuesVSCode = await github.Issue.GetAllForRepository(vscode.Result.Id);
            foreach (var user in users)
            {
                var usuarioClass = await github.User.Get(user);
                var repositorios = await github.Repository.GetAllForUser(user);
                int somaStars = 0;
                int somaForks = 0;
                int somaResponsavelIssuesVSCode = 0;
                int somaPullRequestsAceitosVSCode = 0;
                foreach(var issue in issuesVSCode)
                {
                    if(issue.Assignee != null)
                        somaResponsavelIssuesVSCode += issue.Assignee.Login == user ? 1 : 0;
                }
                foreach(var commit in commitsVSCode)
                {
                    if(commit.Author != null)
                        somaPullRequestsAceitosVSCode += commit.Author.Login == user ? 1 : 0;
                }
                foreach(var repo in repositorios)
                {
                    somaStars += repo.StargazersCount;
                    somaForks += repo.ForksCount;
                }
                usuarios.Add(new Usuario { Nome = user,
                    QtdForks = somaForks,
                    QtdStars = somaStars,
                    QtdIssuesResponsavel = somaResponsavelIssuesVSCode,
                    QtdPullRequestAceito = somaPullRequestsAceitosVSCode
                });

            }
            foreach(var usuario in usuarios)
            {
                Console.WriteLine("Usuário: {0}\n" +
                    "Soma de forks: {1}\n" +
                    "Soma de stars: {2}\n" +
                    "Responsável por {3} issues no VSCode\n" +
                    "Teve {4} pull requests aceitos no VSCode\n\n", 
                    usuario.Nome, 
                    usuario.QtdForks, 
                    usuario.QtdStars, 
                    usuario.QtdIssuesResponsavel,
                    usuario.QtdPullRequestAceito);
            }
            
        }
    }
}
