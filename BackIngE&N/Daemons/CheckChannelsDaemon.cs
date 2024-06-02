﻿using BackIngE_N.Logic;

namespace BackIngE_N.Daemons {
    public class CheckChannelsDaemon:IHostedService, IDisposable {

        //private readonly ChannelLogic _channelLogic;
        private Timer? _timer;
        private readonly ILogger<CheckChannelsDaemon> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public CheckChannelsDaemon(IServiceScopeFactory scopeFactory, ILogger<CheckChannelsDaemon> logger) {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            // Inicializa el temporizador para que ejecute la tarea periódicamente
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(10)); // Ajusta el intervalo según sea necesario
            return Task.CompletedTask;
        }

        private void DoWork(object? state) {
            _logger.LogInformation("Channel Checker Daemon is working.");
            using(IServiceScope scope = _scopeFactory.CreateScope()) {
                ChannelLogic channelLogic = scope.ServiceProvider.GetRequiredService<ChannelLogic>();
                try {
                    channelLogic.VerifyChannels().GetAwaiter().GetResult();
                } catch(Exception ex) {
                    _logger.LogError(ex, "Error while verifying channels.");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            _logger.LogInformation("Channel Checker Daemon is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose() {
            _timer?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
