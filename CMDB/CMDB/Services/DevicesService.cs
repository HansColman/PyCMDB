﻿using CMDB.Infrastructure;
using CMDB.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace CMDB.Services
{
    public class DevicesService : LogService
    {
        public DevicesService(CMDBContext context) : base(context)
        {
        }
        public async Task<List<Device>> ListAll(string category)
        {
            List<Device> devices = new();
            switch (category)
            {
                case "Laptop":
                    var laptops = await _context.Devices
                        .OfType<Laptop>()
                        .Include(x => x.Category)
                        .Include(x => x.Identity)
                        .Include(x => x.Type)
                        .ToListAsync();
                    foreach (var laptop in laptops)
                    {
                        devices.Add(laptop);
                    }
                    break;
                case "Desktop":
                    var dekstops = await _context.Devices
                        .OfType<Desktop>()
                        .Include(x => x.Category)
                        .Include(x => x.Identity)
                        .Include(x => x.Type)
                        .ToListAsync();
                    foreach (var desktop in dekstops)
                    {
                        devices.Add(desktop);
                    }
                    break;
                case "Docking":
                    var dockings = await _context.Devices
                        .OfType<Docking>()
                        .Include(x => x.Category)
                        .Include(x => x.Identity)
                        .Include(x => x.Type)
                        .ToListAsync();
                    foreach (var docking in dockings)
                    {
                        devices.Add(docking);
                    }
                    break;
                case "Token":
                    var tokens = await _context.Devices
                        .OfType<Token>()
                        .Include(x => x.Category)
                        .Include(x => x.Identity)
                        .Include(x => x.Type)
                        .ToListAsync();
                    foreach (var token in tokens)
                    {
                        devices.Add(token);
                    }
                    break;
                case "Monitor":
                    var screens = await _context.Devices
                        .OfType<Screen>()
                        .Include(x => x.Category)
                        .Include(x => x.Identity)
                        .Include(x => x.Type)
                        .ToListAsync();
                    foreach (var screen in screens)
                    {
                        devices.Add(screen);
                    }
                    break;
                default:
                    break;
            }
            return devices;
        }
        public async Task<List<Device>> ListAll(string category, string searchString)
        {
            string searhterm = "%" + searchString + "%";
            List<Device> devices = new();
            switch (category)
            {
                case "Laptop":
                    var laptops = await _context.Devices
                        .OfType<Laptop>()
                        .Include(x => x.Category)
                        .Include(x => x.Identity)
                        .Include(x => x.Type)
                        .Where(x => EF.Functions.Like(x.SerialNumber, searhterm) || EF.Functions.Like(x.AssetTag, searhterm))
                        .ToListAsync();
                    foreach (var laptop in laptops)
                    {
                        devices.Add(laptop);
                    }
                    break;
                case "Desktop":
                    var dekstops = await _context.Devices
                        .OfType<Desktop>()
                       .Include(x => x.Category)
                       .Include(x => x.Identity)
                       .Include(x => x.Type)
                       .Where(x => EF.Functions.Like(x.SerialNumber, searhterm) || EF.Functions.Like(x.AssetTag, searhterm))
                       .ToListAsync();
                    foreach (var desktop in dekstops)
                    {
                        devices.Add(desktop);
                    }
                    break;
                case "Docking":
                    var dockings = await _context.Devices
                        .OfType<Docking>()
                        .Include(x => x.Category)
                        .Include(x => x.Identity)
                        .Where(x => EF.Functions.Like(x.SerialNumber, searhterm) || EF.Functions.Like(x.AssetTag, searhterm))
                        .ToListAsync();
                    foreach (var docking in dockings)
                    {
                        devices.Add(docking);
                    }
                    break;
                case "Token":
                    var tokens = await _context.Devices
                        .OfType<Token>()
                        .Include(x => x.Category)
                        .Include(x => x.Identity)
                        .Include(x => x.Type)
                        .Where(x => EF.Functions.Like(x.SerialNumber, searhterm) || EF.Functions.Like(x.AssetTag, searhterm))
                        .ToListAsync();
                    foreach (var token in tokens)
                    {
                        devices.Add(token);
                    }
                    break;
                case "Monitor":
                    var screens = await _context.Devices
                        .OfType<Screen>()
                        .Include(x => x.Category)
                        .Include(x => x.Identity)
                        .Include(x => x.Type)
                        .Where(x => EF.Functions.Like(x.SerialNumber, searhterm) || EF.Functions.Like(x.AssetTag, searhterm))
                        .ToListAsync();
                    foreach (var screen in screens)
                    {
                        devices.Add(screen);
                    }
                    break;
                default:
                    break;
            }
            return devices;
        }
        public List<SelectListItem> ListRams()
        {
            List<SelectListItem> assettypes = new();
            var rams = _context.RAMs.ToList();
            foreach (var ram in rams)
            {
                assettypes.Add(new(ram.Display, ram.Value.ToString()));
            }
            return assettypes;
        }
        public async Task CreateNewDesktop(Desktop desktop, string table)
        {
            desktop.LastModfiedAdmin = Admin;
            _context.Devices.Add(desktop);
            await _context.SaveChangesAsync();
            string Value = String.Format("{0} with type {1}", desktop.Category.Category, desktop.Type.Vendor + " " + desktop.Type.Type);
            await LogCreate(table, desktop.AssetTag, Value);
        }
        public async Task CreateNewLaptop(Laptop laptop, string table)
        {
            laptop.LastModfiedAdmin = Admin;
            _context.Devices.Add(laptop);
            await _context.SaveChangesAsync();
            string Value = String.Format("{0} with type {1}", laptop.Category.Category, laptop.Type.Vendor + " " + laptop.Type.Type);
            await LogCreate(table, laptop.AssetTag, Value);
        }
        public async Task UpdateDesktop(Desktop desktop, string newRam, string newMAC, AssetType newAssetType, string newSerialNumber, string Table)
        {
            desktop.LastModfiedAdmin = Admin;
            string OldRam, OldMac, OldSerial, OldType;
            OldMac = desktop.MAC;
            OldRam = desktop.RAM;
            OldSerial = desktop.SerialNumber;
            OldType = desktop.Type.Vendor + " " + desktop.Type.Type;
            if (String.Compare(desktop.RAM, newRam) != 0)
            {
                desktop.RAM = newRam;
                await _context.SaveChangesAsync();
                await LogUpdate(Table, desktop.AssetTag, "RAM", OldRam, newRam);
            }
            if (String.Compare(desktop.MAC, newMAC) != 0)
            {
                desktop.MAC = newMAC;
                await _context.SaveChangesAsync();
                await LogUpdate(Table, desktop.AssetTag, "MAC", OldMac, newMAC);
            }
            if (String.Compare(desktop.SerialNumber, newSerialNumber) != 0)
            {
                desktop.SerialNumber = newSerialNumber;
                await _context.SaveChangesAsync();
                await LogUpdate(Table, desktop.AssetTag, "SerialNumber", OldSerial, newSerialNumber);
            }
            if (desktop.Type.TypeID != newAssetType.TypeID)
            {
                desktop.Type = newAssetType;
                await _context.SaveChangesAsync();
                await LogUpdate(Table, desktop.AssetTag, "Type", OldType, newAssetType.Vendor + " " + newAssetType.Type);
            }
        }
        public async Task UpdateLaptop(Laptop laptop, string newRam, string newMAC, AssetType newAssetType, string newSerialNumber, string Table)
        {
            laptop.LastModfiedAdmin = Admin;
            string OldRam, OldMac, OldSerial, OldType;
            OldMac = laptop.MAC;
            OldRam = laptop.RAM;
            OldSerial = laptop.SerialNumber;
            OldType = laptop.Type.Vendor + " " + laptop.Type.Type;
            if (String.Compare(laptop.RAM, newRam) != 0)
            {
                laptop.RAM = newRam;
                await _context.SaveChangesAsync();
                await LogUpdate(Table, laptop.AssetTag, "RAM", OldRam, newRam);
            }
            if (String.Compare(laptop.MAC, newMAC) != 0)
            {
                laptop.MAC = newMAC;
                await _context.SaveChangesAsync();
                await LogUpdate(Table, laptop.AssetTag, "MAC", OldMac, newMAC);
            }
            if (String.Compare(laptop.SerialNumber, newSerialNumber) != 0)
            {
                laptop.SerialNumber = newSerialNumber;
                await _context.SaveChangesAsync();
                await LogUpdate(Table, laptop.AssetTag, "SerialNumber", OldSerial, newSerialNumber);
            }
            if (laptop.Type.TypeID != newAssetType.TypeID)
            {
                laptop.Type = newAssetType;
                await _context.SaveChangesAsync();
                await LogUpdate(Table, laptop.AssetTag, "Type", OldType, newAssetType.Vendor + " " + newAssetType.Type);
            }
        }
        public async Task Deactivate(Device device, string Reason, string table)
        {
            device.LastModfiedAdmin = Admin;
            device.DeactivateReason = Reason;
            device.Active = "Inactive";
            await _context.SaveChangesAsync();
            string Value = String.Format("{0} with type {1}", device.Category.Category, device.Type.Vendor + " " + device.Type.Type);
            await LogDeactivated(table, device.AssetTag, Value, Reason);
        }
        public async Task Activate(Device device, string table)
        {
            device.LastModfiedAdmin = Admin;
            device.DeactivateReason = "";
            device.Active = "Active";
            await _context.SaveChangesAsync();
            string Value = String.Format("{0} with type {1}", device.Category.Category, device.Type.Vendor + " " + device.Type.Type);
            await LogActivate(table, device.AssetTag, Value);
        }
        public async Task<List<Desktop>> ListDekstopByID(string assetTag)
        {
            var desktops = await _context.Devices
                .OfType<Desktop>()
                .Include(x => x.Category)
                .Include(x => x.Identity)
                .Where(x => x.AssetTag == assetTag)
                .ToListAsync();
            return desktops;
        }
        public async Task<List<Laptop>> ListLaptopByID(string assetTag)
        {
            var laptops = await _context.Devices
                .OfType<Laptop>()
                .Include(x => x.Category)
                .Include(x => x.Identity)
                .Where(x => x.AssetTag == assetTag)
                .ToListAsync();
            return laptops;
        }
        public async Task<List<Docking>> ListDockingByID(string assetTag)
        {
            var dockings = await _context.Devices
                .OfType<Docking>()
                .Include(x => x.Category)
                .Include(x => x.Identity)
                .Where(x => x.AssetTag == assetTag)
                .ToListAsync();
            return dockings;
        }
        public async Task<List<Screen>> ListScreensByID(string assetTag)
        {
            var screens = await _context.Devices
                .OfType<Screen>()
                .Include(x => x.Category)
                .Include(x => x.Identity)
                .Where(x => x.AssetTag == assetTag)
                .ToListAsync();
            return screens;
        }
        public async Task<List<Token>> ListTokenByID(string assetTag)
        {
            var tokens = await _context.Devices
                .OfType<Token>()
                .Include(x => x.Category)
                .Include(x => x.Identity)
                .Where(x => x.AssetTag == assetTag)
                .ToListAsync();
            return tokens;
        }
        public bool IsLaptopExisting(Laptop device)
        {
            bool result = false;
            var devices = _context.Devices.OfType<Laptop>().Where(x => x.AssetTag == device.AssetTag).ToList();
            if (devices.Count > 0)
                result = true;
            return result;
        }
        public bool IsDesktopExisting(Desktop device)
        {
            bool result = false;
            var devices = _context.Devices.OfType<Desktop>().Where(x => x.AssetTag == device.AssetTag).ToList();
            if (devices.Count > 0)
                result = true;
            return result;
        }
        public List<SelectListItem> ListAssetTypes(string category)
        {
            List<SelectListItem> assettypes = new();
            var types = _context.AssetTypes.Include(x => x.Category).Where(x => x.Category.Category == category).ToList();
            foreach (var type in types)
            {
                assettypes.Add(new(type.Vendor + " " + type.Type, type.TypeID.ToString()));
            }
            return assettypes;
        }
        public List<AssetType> ListAssetTypeById(int id)
        {
            var devices = _context.AssetTypes
                .Include(x => x.Category)
                .Where(x => x.TypeID == id)
                .ToList();
            return devices;
        }
        public void GetAssignedIdentity(Laptop laptop)
        {
            var Identity = _context.Devices.OfType<Laptop>()
                .Include(x => x.Identity)
                .Where(x => x.AssetTag == laptop.AssetTag)
                .Select(x => x.Identity);
        }
        public void GetAssignedIdentity(Desktop desktop)
        {
            var Identity = _context.Devices.OfType<Desktop>()
                .Include(x => x.Identity)
                .Where(x => x.AssetTag == desktop.AssetTag)
                .Select(x => x.Identity);
        }
    }
}
