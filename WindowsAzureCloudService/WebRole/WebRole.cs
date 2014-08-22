using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace WebRole
{
    public class WebRole : RoleEntryPoint, IWebRole
    {
        public string RunInternalProcess(string stringParams)
        {
            var path = HttpContext.Current.Server.MapPath("..\\TSP_Genetic_Algorithm.exe");
            var result = RunProcessAndGetOutputAsync(stringParams, path).Result;
            return result.Replace(" ", "\n");
        }

        private static async Task<string> RunProcessAndGetOutputAsync(string stringParams, string path)
        {
            return await RunProcessAndGetOutput(stringParams, path);
        }

        private static Task<string> RunProcessAndGetOutput(string stringParams, string path)
        {
            var process = CreateProcess(stringParams, path);
            process.Start();
            var result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            var taskCompletionSource = CreateTaskCompletionSourceAndSet(result);
            return taskCompletionSource.Task;
        }

        private static TaskCompletionSource<string> CreateTaskCompletionSourceAndSet(string result)
        {
            var taskCompletionSource = new TaskCompletionSource<string>();
            taskCompletionSource.SetResult(result);
            return taskCompletionSource;
        }

        private static Process CreateProcess(string stringParams, string path)
        {
            return new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    Arguments = stringParams,
                    FileName = path
                }
            };
        }
    }
}
