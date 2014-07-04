using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpUtils
{
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
    }

}
