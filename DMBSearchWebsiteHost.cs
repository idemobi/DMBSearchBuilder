using System.Diagnostics;
using System.Text.Json;

namespace DMBSearchBuilder
{
    internal sealed class DMBSearchWebsiteHost : IDisposable
    {
        private readonly Process? _process;

        private DMBSearchWebsiteHost(Uri baseUri, Process? process)
        {
            BaseUri = baseUri;
            _process = process;
        }

        internal Uri BaseUri { get; }

        internal static async Task<DMBSearchWebsiteHost> EnsureAvailableAsync(
            string projectPath,
            string launchSettingsPath,
            string preferredLaunchProfile,
            string? fallbackLaunchProfile,
            TimeSpan startupTimeout,
            bool useNoBuild)
        {
            DMBSearchLaunchProfile launchProfile = LoadLaunchProfile(
                launchSettingsPath,
                preferredLaunchProfile,
                fallbackLaunchProfile);

            if (await IsAvailableAsync(launchProfile.BaseUri).ConfigureAwait(false))
            {
                Console.WriteLine($"[DMBSearchBuilder] Website already available at {launchProfile.BaseUri}");
                return new DMBSearchWebsiteHost(launchProfile.BaseUri, null);
            }

            Console.WriteLine($"[DMBSearchBuilder] Website unavailable at {launchProfile.BaseUri}");
            string buildMode = useNoBuild ? " and --no-build" : string.Empty;
            Console.WriteLine($"[DMBSearchBuilder] Starting {Path.GetFileName(projectPath)} with launch profile '{launchProfile.Name}'{buildMode}.");

            Process process = StartWebsite(projectPath, launchProfile.Name, useNoBuild);
            try
            {
                await WaitUntilAvailableAsync(launchProfile.BaseUri, startupTimeout, process).ConfigureAwait(false);
                Console.WriteLine($"[DMBSearchBuilder] Website ready at {launchProfile.BaseUri}");
                return new DMBSearchWebsiteHost(launchProfile.BaseUri, process);
            }
            catch
            {
                StopProcess(process);
                process.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Stops the temporary website process when this host started one.
        /// </summary>
        public void Dispose()
        {
            if (_process == null)
            {
                return;
            }

            Console.WriteLine("[DMBSearchBuilder] Stopping temporary website process.");
            StopProcess(_process);
            _process.Dispose();
        }

        private static Process StartWebsite(string projectPath, string launchProfile, bool useNoBuild)
        {
            ProcessStartInfo processStartInfo = new()
            {
                FileName = "dotnet",
                WorkingDirectory = Path.GetDirectoryName(projectPath) ?? AppContext.BaseDirectory,
                UseShellExecute = false
            };

            processStartInfo.ArgumentList.Add("run");
            processStartInfo.ArgumentList.Add("--project");
            processStartInfo.ArgumentList.Add(projectPath);
            processStartInfo.ArgumentList.Add("--launch-profile");
            processStartInfo.ArgumentList.Add(launchProfile);
            if (useNoBuild)
            {
                processStartInfo.ArgumentList.Add("--no-build");
            }

            Process? process = Process.Start(processStartInfo);
            if (process == null)
            {
                throw new InvalidOperationException("Unable to start the website process.");
            }

            return process;
        }

        private static async Task WaitUntilAvailableAsync(Uri baseUri, TimeSpan timeout, Process process)
        {
            DateTimeOffset deadline = DateTimeOffset.UtcNow.Add(timeout);
            while (DateTimeOffset.UtcNow < deadline)
            {
                if (process.HasExited)
                {
                    throw new InvalidOperationException($"The website process exited before {baseUri} became available.");
                }

                if (await IsAvailableAsync(baseUri).ConfigureAwait(false))
                {
                    return;
                }

                await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
            }

            throw new TimeoutException($"The website did not become available at {baseUri} before the timeout.");
        }

        private static async Task<bool> IsAvailableAsync(Uri baseUri)
        {
            try
            {
                using HttpClientHandler handler = new();
                if (baseUri.IsLoopback && string.Equals(baseUri.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
                {
                    handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                }

                using HttpClient client = new(handler)
                {
                    Timeout = TimeSpan.FromSeconds(5)
                };

                using HttpResponseMessage response = await client.GetAsync(baseUri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                return true;
            }
            catch (HttpRequestException)
            {
                return false;
            }
            catch (TaskCanceledException)
            {
                return false;
            }
        }

        private static DMBSearchLaunchProfile LoadLaunchProfile(
            string launchSettingsPath,
            string preferredLaunchProfile,
            string? fallbackLaunchProfile)
        {
            using FileStream stream = File.OpenRead(launchSettingsPath);
            using JsonDocument document = JsonDocument.Parse(stream);

            JsonElement profiles = document.RootElement.GetProperty("profiles");
            string profileName = profiles.TryGetProperty(preferredLaunchProfile, out JsonElement profile)
                ? preferredLaunchProfile
                : fallbackLaunchProfile ?? string.Empty;

            if (string.IsNullOrWhiteSpace(profileName) || !profiles.TryGetProperty(profileName, out profile))
            {
                throw new InvalidOperationException($"The launch profile '{preferredLaunchProfile}' was not found in {launchSettingsPath}.");
            }

            if (!profile.TryGetProperty("applicationUrl", out JsonElement applicationUrlElement))
            {
                throw new InvalidOperationException($"The launch profile '{profileName}' does not define an applicationUrl.");
            }

            Uri baseUri = ResolveBaseUri(applicationUrlElement.GetString());

            return new DMBSearchLaunchProfile(profileName, baseUri);
        }

        private static Uri ResolveBaseUri(string? applicationUrl)
        {
            string[] urls = (applicationUrl ?? string.Empty)
                .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            string? selectedUrl = urls.FirstOrDefault(url => url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                ?? urls.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(selectedUrl))
            {
                throw new InvalidOperationException("The selected launch profile does not define an applicationUrl.");
            }

            return new Uri(selectedUrl.EndsWith("/", StringComparison.Ordinal) ? selectedUrl : selectedUrl + "/");
        }

        private static void StopProcess(Process process)
        {
            if (process.HasExited)
            {
                return;
            }

            process.Kill(entireProcessTree: true);
            process.WaitForExit(TimeSpan.FromSeconds(10));
        }
    }

    internal sealed record DMBSearchLaunchProfile(string Name, Uri BaseUri);
}
