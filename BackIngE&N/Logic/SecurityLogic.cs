using BackIngE_N.BD;
using BackIngE_N.Context;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BackIngE_N.Logic
{
    public class SecurityLogic {

        private readonly IngenieriaeynContext _context;

        public SecurityLogic(IngenieriaeynContext context) {
            _context = context;
        }

        /// <summary>
        /// Verify if an IP is blocked.
        /// </summary>
        /// <param name="ip">The ip to verify</param>
        /// <returns>A boolean if is blocked or not</returns>
        public async Task<bool> isBlockedIP(IPAddress ip) {
            if (ip == null) return false;
            return await _context.BlockedIps.Where(b => b.Ip == ip.ToString()).FirstOrDefaultAsync() != null;
        }

        /// <summary>
        /// Unblock an IP.
        /// </summary>
        /// <param name="ip">The ip to unblock</param>
        public void UnblockIP(IPAddress ip) {
            if (ip == null) return;

            BlockedIp b = _context.BlockedIps.Where(b => b.Ip == ip.ToString()).FirstOrDefault();

            if (b != null) {
                _context.BlockedIps.Remove(b);
                _ = _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Block an IP.
        /// </summary>
        /// <param name="ip">The ip to block</param>
        public async Task BlockIP(IPAddress ip) {
            await _context.BlockedIps.AddAsync(new BlockedIp() {
                Ip = ip.ToString(),
                BlockTime = DateTime.Now
            });

            Console.WriteLine($"IP {ip} blocked");

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Validate an IP.
        /// </summary>
        /// <param name="ip">Validates if an IP has many requests and blocks them</param>
        public async Task ValidateIP(IPAddress ip) {

            if (ip == null) return;

            var threshold = 1; // Número de intentos fallidos necesarios para bloquear la IP
            var blockDurationMinutes = 5; // Duración del bloqueo en minutos
            var blockStartTime = DateTime.Now.AddMinutes(-blockDurationMinutes);

            var count = await _context.Securities
                .Where(s => s.Ip == ip.ToString() && s.LoginTime > blockStartTime && s.StatusLogin == false)
                .CountAsync();

            if (count >= threshold) {
                await BlockIP(ip);
            }
        }

        /// <summary>
        /// Save an IP.
        /// </summary>
        /// <param name="ip">The ip to save on bd</param>
        /// <returns>A boolean if the ip was saved</returns>
        public async Task<bool> SaveIP(IPAddress ip, bool status) {
            await _context.Securities.AddAsync(new Security() {
                Ip = ip.ToString(),
                LoginTime = DateTime.Now,
                StatusLogin = status
            });

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
