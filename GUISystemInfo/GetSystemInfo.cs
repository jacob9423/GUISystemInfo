using System;
using System.IO;
using System.Management;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Drawing;
// Differnt Qurys for differnt system information.
// https://docs.microsoft.com/en-us/windows/desktop/CIMWin32Prov/win32-provider
// information on what you can qury and what fields are in those files
namespace GUISystemInfo
{
    class GetSystemInfo
    {
        public string year = DateTime.Now.Year.ToString();
        public SystemInfo SysInfo = new SystemInfo();
        public void StartProgram()
        {
            GetInfo();
        }
        public void GetInfo()
        {
            PCName();
            OSBits();
            ProcessorName();
            PCModelAndRam();
            OSVersion();
            BiosInfo();
            ConvertBytesToGB();
        }
        public void PCName()
        {
            try
            {
                SysInfo.Name = System.Environment.MachineName;
            }
            catch (Exception)
            {
                SysInfo.Name = "Not Found";
            }
        }
        public void OSBits()
        {
            try
            {
                if (System.Environment.Is64BitOperatingSystem == false)
                {
                    SysInfo.Bit = "32";
                }
                else
                {
                    SysInfo.Bit = "64";
                }
            }
            catch (Exception)
            {
                SysInfo.Bit = "Not Found";
            }
        }
        public void ProcessorName()
        {
            try
            {
                SelectQuery ProcessorInfo = new SelectQuery(@"Select Name from Win32_Processor");
                using (ManagementObjectSearcher InfoSearch = new ManagementObjectSearcher(ProcessorInfo))
                {
                    foreach (ManagementObject process in InfoSearch.Get())
                    {
                        SysInfo.Processor = process["Name"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                SysInfo.Processor = "Not Found";
            }  
        }
        public void PCModelAndRam()
        {
            try
            {
                SelectQuery ComputerInfo = new SelectQuery(@"Select * from Win32_ComputerSystem");
                using (ManagementObjectSearcher InfoSearch = new ManagementObjectSearcher(ComputerInfo))
                {
                    foreach (ManagementObject process in InfoSearch.Get())
                    {
                        SysInfo.Model = process["Model"].ToString();
                        SysInfo.Ram = process["TotalPhysicalMemory"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                SysInfo.Model = "Not Found";
                SysInfo.Ram = "Not Found";
            }   
        }
        public void OSVersion()
        {
            SelectQuery OSInfo = new SelectQuery(@"Select Caption from Win32_OperatingSystem");
            // geting the OS version in HUMAN Friendly readable version not the number version of Windows.
            using (ManagementObjectSearcher InfoSearch = new ManagementObjectSearcher(OSInfo))
            {
                foreach (ManagementObject process in InfoSearch.Get())
                {
                    SysInfo.OS = process["Caption"].ToString();
                }
            }
        }
        public void BiosInfo()
        {
            //On Dell systems the Serial Number is called the service tag
            SelectQuery ServiceTagQury = new SelectQuery(@"Select Serialnumber from Win32_BIOS");
            SelectQuery ServiceTagTry2 = new SelectQuery(@"Select Serialnumber from Win32_BaseBoard");
            // Code doesn't work on my computer so having error checking will save me later. 
            // Code returns "Default string" on my computer
            // trying two methods because I don't know what one will return the correct value
            // The first one will return the correct value on my laptop (Emulated BIOS 2012 Macbook Pro Windows 10) 
            // and the secound will return a value on my EFUI Desktop.
            try
            {
                using (ManagementObjectSearcher InfoSearch = new ManagementObjectSearcher(ServiceTagQury))
                {
                    foreach (ManagementObject process in InfoSearch.Get())
                    {
                        SysInfo.ServiceTag = process["Serialnumber"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error While Trying to get Dell service Tag. Trying Again:");
            }
            // Another try catch for the BIOS Serial number. Just a differnt way of getting it.
            if (SysInfo.ServiceTag == "Default string")
            {
                SysInfo.ServiceTag = "N/A";
                try
                {
                    using (ManagementObjectSearcher InfoSearch = new ManagementObjectSearcher(ServiceTagTry2))
                    {
                        foreach (ManagementObject process in InfoSearch.Get())
                        {
                            SysInfo.ServiceTag = process["Serialnumber"].ToString();
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error on secound attempt.Press Ok.");
                    SysInfo.ServiceTag = Microsoft.VisualBasic.Interaction.InputBox("Enter service tag manually");
                }
            }
        }
        public void ConvertBytesToGB()
        {
            double convert = System.Convert.ToDouble(SysInfo.Ram) / 1073741824;
            convert = Math.Round(convert);
            SysInfo.Ram = convert.ToString() + "GB";
        }
        public void MoveDataToFile()
        {
            try
            {
                using (StreamWriter invintory = File.AppendText("..\\Inventory.txt"))
                {
                    invintory.WriteLine("{0},{1},\"{2}\",{3},\"{4}\",{5},{6},\"{7}\",\"{8}\",\"{9}\",", SysInfo.RoomNum, SysInfo.Name, SysInfo.Model, SysInfo.ServiceTag, SysInfo.OS, SysInfo.Bit, SysInfo.Ram, SysInfo.Processor, SysInfo.People, SysInfo.Comments);
                    invintory.Close();
                }
                MessageBox.Show("Done!");
            }
            catch (Exception)
            {
                MessageBox.Show("Could not write to file. Try running the program again");
            } 
        }
    }  
}
