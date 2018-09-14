using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DELTA_Service.CNC
{

    class Service_Controll
    {
        protected string service_name = "SDI_CID";

        public void Service_Shutdown()//關閉Service
        {
            ServiceController[] scServices;
            scServices = ServiceController.GetServices();
            foreach (ServiceController scTemp in scServices)
            {
                if (scTemp.ServiceName.Equals(service_name))
                {
                    ServiceController sc = new ServiceController(service_name);
                    if (sc.Status == ServiceControllerStatus.Running)
                    {
                        sc.Stop();
                        sc.Close();
                    }
                }
            }
        }

        public void Service_Start ()//啟動Service
        {
            ServiceController[] scServices;
            scServices = ServiceController.GetServices();
            foreach (ServiceController scTemp in scServices)
            {
                if (scTemp.ServiceName.Equals(service_name))
                {
                    ServiceController sc = new ServiceController(service_name);
                    if (sc.Status == ServiceControllerStatus.Stopped)
                    {
                        sc.Start();
                    }
                    else
                    {
                        throw new Exception("Can't get Service state.");
                    }
                }
                else
                {
                    throw new Exception("Don't find service name,please check service name is correct or not!");
                }
            }
        }

        public void Service_Restart()//重新啟動Service，也就是Shutdown後Start
        {
            Service_Shutdown();
            Service_Start();
        }
    }
}
