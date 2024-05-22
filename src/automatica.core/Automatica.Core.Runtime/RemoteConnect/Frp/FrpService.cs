﻿using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Automatica.Core.Base.Common;
using Automatica.Core.Base.Tunneling;
using Automatica.Core.Internals.Cloud;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Automatica.Core.Runtime.RemoteConnect.Frp
{
    internal class FrpService : IFrpService
    {
        private readonly IOptionsMonitor<FrpcOptions> _options;
        private readonly IFrpProcess _process;
        private readonly ICloudApi _cloudApi;
        private readonly ILogger<FrpService> _logger;

        private bool _isInitialized;

        private readonly string _frpcIniFile = "frpc.ini";
        private readonly string _frpcWebIni = "frpc_automatica-web.ini";
        private readonly string _frpcSshIni = "frpc_ssh.ini";

        public FrpService(IOptionsMonitor<FrpcOptions> options, IFrpProcess process, ICloudApi cloudApi, ILogger<FrpService> logger)
        {
            _options = options;
            _process = process;
            _cloudApi = cloudApi;
            _logger = logger;
        }

        public async Task<bool> InitConfigurationsAsync(CancellationToken cancellationToken = default)
        {
            var currentDir = ServerInfo.GetBasePath();
            try
            {
                if (!Directory.Exists(Path.Combine(currentDir, "frp")))
                {
                    throw new ArgumentException($"Could not find configuration directory in {currentDir}");
                }

                if (!File.Exists(Path.Combine(currentDir, "frp", _frpcIniFile)))
                {
                    throw new ArgumentException(
                        $"Could not find config file '{_frpcIniFile}' in {Directory.GetCurrentDirectory()}");
                }

                if (!Directory.Exists(Path.Combine(currentDir, "frp", "enabled")))
                {
                    Directory.CreateDirectory(Path.Combine(currentDir, "frp", "enabled"));
                }
                else
                {
                    var files = Directory.GetFiles(Path.Combine(currentDir, "frp", "enabled"));

                    foreach (var file in files)
                    {
                        File.Delete(file);
                    }
                }

                if (_options.CurrentValue.UseSsh)
                {
                    try
                    {
                        var remoteSshPort =
                            await _cloudApi.GetRemoteConnectPort(ServerInfo.ServerUid, "ssh", TunnelingProtocol.Tcp);

                        Environment.SetEnvironmentVariable("REMOTE_SSH_PORT", remoteSshPort.Port.ToString());
                        InitConfigurationFile(Path.Combine(currentDir, "frp", _frpcSshIni));
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "Could not get remote ssh port");
                    }
                }
                if (_options.CurrentValue.UseWeb)
                {
                    InitConfigurationFile(Path.Combine(currentDir, "frp", _frpcWebIni));
                }

                _isInitialized = true;
            }
            catch
            {
                throw;
            }

            return true;
        }

        private void InitConfigurationFile(string file)
        {
            if (!File.Exists(file))
            {
                _logger.LogError($"Cannot enable service, '{file}' could not be found!");
            }
            else
            {
                var fileInfo = new FileInfo(file);
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    File.Copy(file, Path.Combine(fileInfo.DirectoryName!, "enabled", fileInfo.Name));
                }
                else
                {
                    File.CreateSymbolicLink(Path.Combine(fileInfo.DirectoryName!, "enabled", fileInfo.Name), file);
                }
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            if (!_isInitialized)
            {
                if (!await InitConfigurationsAsync(cancellationToken))
                {
                    _logger.LogError($"Could not initialize RemoteControl service...");
                    return;
                }
            }
            await _process.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _process.StopAsync(cancellationToken);
        }
    }
}
