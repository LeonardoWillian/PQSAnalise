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
            Dictionary<string, string> repositories = new Dictionary<string, string>()
            {
                { "Tyriar", "vscode-terminal-api-example" },
                { "isidorn", "aspnet" },
                { "rebornix", "monaco-vue" },
                { "sandy081", "vscode-todotasks" },
                { "bpasero", "fsevents-prebuilt" },
                { "aeschli", "sample-languages" },
                { "jrieken", "md-navigate" },
                { "ramya-rao-a", "electron-crash-server-go" },
                { "joaomoreno", "large-scale-typescript" },
                { "egamma", "mobiletry" }
            };

            GetRepositories(github, repositories);
            
            Console.ReadKey();
        }

        public static async void GetRepositories(GitHubClient github, Dictionary<string, string> repos)
        {
            List<Repository> repositorios = new List<Repository>();

            foreach (var repo in repos)
            {
                repositorios.Add(await github.Repository.Get(repo.Key, repo.Value));

            }
            foreach(var repo in repositorios)
            {
                Console.WriteLine("Repositorio: {0}\n\n" +
                    "Contagem de forks: {1}\n" +
                    "Contagem de stars: {2}\n" +
                    "", repo.FullName, repo.ForksCount, repo.StargazersCount);
            }
            
        }
    }
}
