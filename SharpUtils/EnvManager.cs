using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SharpUtils
{

    /** Simple property file are far easier then complex XML stuff
     * Taken from http://stackoverflow.com/a/16972767/75540
     * but adapted to support Java/Unix comment convention
     * Environment expansion is still unsupported.
     * 
     */ 
   public class IniReader
    {
        Dictionary<string, Dictionary<string, string>> ini = new Dictionary<string, Dictionary<string, string>>(StringComparer.InvariantCultureIgnoreCase);

        public IniReader(string file)
        {
            var txt = File.ReadAllText(file);

            Dictionary<string, string> currentSection = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            ini[""] = currentSection;
            // .NET 3.5 does not have string.IsNullOrWhiteSpace(t)
            foreach (var line in txt.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)
                                   .Where(t => !string.IsNullOrEmpty(t))
                                   .Select(t => t.Trim()))
            {
                if (line.StartsWith(";") || line.StartsWith("#"))
                    continue;

                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    currentSection = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
                    ini[line.Substring(1, line.LastIndexOf("]") - 1)] = currentSection;
                    continue;
                }

                var idx = line.IndexOf("=");
                if (idx == -1)
                    currentSection[line] = "";
                else
                    currentSection[line.Substring(0, idx)] = line.Substring(idx + 1);
            }
        }

        public string GetValue(string key)
        {
            return GetValue(key, "", "");
        }

        public string GetValue(string key, string section)
        {
            return GetValue(key, section, "");
        }

        public string GetValue(string key, string section, string @default)
        {
            if (!ini.ContainsKey(section))
                return @default;

            if (!ini[section].ContainsKey(key))
                return @default;

            return ini[section][key];
        }

        public string[] GetKeys(string section)
        {
            if (!ini.ContainsKey(section))
                return new string[0];

            return ini[section].Keys.ToArray();
        }

        public string[] GetSections()
        {
            return ini.Keys.Where(t => t != "").ToArray();
        }

        /// <summary>
        /// Return false if the parameter does not exist!
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool GetBoolean(string p)
        {
            return GetValue(p, "", "false").Equals("true", StringComparison.CurrentCultureIgnoreCase);
        }
    }
    public enum Config
    {
        MsgShare,
        AuditLog
    }

    /// <summary>
    /// It is good practice to have an application to be convigured via environment variable.
    /// Cloud Services like Heroku are heavly based on this pattern
    /// Use 
    ///  SETX var val 
    /// to permanently set a value under windows server/7/ etc.
    /// 
    /// </summary>
    public class Env
    {
        private string prefix;
        private Env(String ns)
        {
            this.prefix = ns;
        }

        public static  Env getInstance(String appName)
        {
            return new Env("APP_" + appName.ToUpper());
        }

        public Boolean isDebugEnabled()
        {

            var v= Environment.GetEnvironmentVariable(prefix + "_DEBUG_MODE");
            if(v==null) { return false; }
            return v.ToLower().Equals("true");
        }

        public IniReader getConfig()
        {
            var i= new IniReader(Environment.GetEnvironmentVariable(prefix + "_CONFIG"));
            return i;

        }
    }

}
